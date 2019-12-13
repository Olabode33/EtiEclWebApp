using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;
using EclEngine.BaseEclEngine.PdInput;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class ScenarioEclWorkings
    {
        protected string _scenario;
        protected ScenarioLifetimeLGD _lifetimeLgd;
        protected LifetimeEadWorkings _lifetimeEad;
        protected PdInputResult _pdInputResult;
        protected IrFactorWorkings _irFactorWorkings;
        protected SicrWorkings _sicrWorkings;

        public ScenarioEclWorkings(string scenario)
        {
            _scenario = scenario;
            _lifetimeEad = new LifetimeEadWorkings();
            _lifetimeLgd = new ScenarioLifetimeLGD(_scenario);
            _pdInputResult = new PdInputResult();
            _irFactorWorkings = new IrFactorWorkings();
            _sicrWorkings = new SicrWorkings();
        }

        public void Run()
        {
            DataTable dataTable = ComputeFinalEcl();
            DataTable dataTable2 = ComputeMonthlyEcl();
            string stop = "Ma te";
        }

        public DataTable ComputeFinalEcl()
        {
            DataTable finalEcl = new DataTable();
            finalEcl.Columns.Add(EclColumns.ContractId, typeof(string));
            finalEcl.Columns.Add(EclColumns.Stage, typeof(int));
            finalEcl.Columns.Add(EclColumns.FinalEclValue, typeof(double));

            DataTable monthlyEcl = ComputeMonthlyEcl();
            DataTable cummulativeDiscountFactor = GetCummulativeDiscountFactor();
            DataTable eadInput = GetTempEadInputData();
            DataTable lifetimeEad = GetLifetimeEadResult();
            DataTable lifetimeLGD = GetLifetimeLgdResult();
            DataTable stageClassifcation = GetStageClassification();

            foreach (DataRow row in stageClassifcation.Rows)
            {
                string contractId = row.Field<string>(StageClassificationColumns.ContractId);
                int stage = row.Field<int>(StageClassificationColumns.Stage);
                string eirGroup = eadInput.AsEnumerable().FirstOrDefault(x => x.Field<string>(EadInputsColumns.ContractId) == contractId).Field<string>(EadInputsColumns.EirGroup);
                double finalEclValue = ComputeFinalEclValue(monthlyEcl, cummulativeDiscountFactor, lifetimeEad, lifetimeLGD, contractId, stage, eirGroup);

                DataRow newRow = finalEcl.NewRow();
                newRow[EclColumns.ContractId] = contractId;
                newRow[EclColumns.Stage] = stage;
                newRow[EclColumns.FinalEclValue] = finalEclValue;

                finalEcl.Rows.Add(newRow);
            }

            return finalEcl;
        }

        protected double ComputeFinalEclValue(DataTable monthlyEcl, DataTable cummulativeDiscountFactor, DataTable lifetimeEad, DataTable lifetimeLGD, string contractId, int stage, string eirGroup)
        {
            double lifetimeLgdMonth0Value = GetMonth0Value(lifetimeLGD, LifetimeLgdColumns.ContractId, LifetimeLgdColumns.Month, LifetimeLgdColumns.Value, contractId);
            double lifetimeEadMonth0Value = GetMonth0Value(lifetimeEad, LifetimeEadColumns.ContractId, LifetimeEadColumns.ProjectionMonth, LifetimeEadColumns.ProjectionValue, contractId);

            double finalEclValue = 0;

            switch (stage)
            {
                case 1:
                    double[] monthEclArray = ComputeMonthArray(monthlyEcl, EclColumns.ContractId, EclColumns.EclMonth, EclColumns.MonthlyEclValue, contractId, 12);
                    double[] monthCdfArray = ComputeMonthArray(cummulativeDiscountFactor, IrFactorColumns.EirGroup, IrFactorColumns.ProjectionMonth, IrFactorColumns.ProjectionValue, eirGroup, 12);
                    finalEclValue = ExcelFormulaUtil.SumProduct(monthEclArray, monthCdfArray);
                    break;
                case 2:
                    double[] monthEclArray2 = ComputeMonthArray(monthlyEcl, EclColumns.ContractId, EclColumns.EclMonth, EclColumns.MonthlyEclValue, contractId, TempEclData.ProjectionNoMonth);
                    double[] monthCdfArray2 = ComputeMonthArray(cummulativeDiscountFactor, IrFactorColumns.EirGroup, IrFactorColumns.ProjectionMonth, IrFactorColumns.ProjectionValue, eirGroup, TempEclData.ProjectionNoMonth);
                    finalEclValue = ExcelFormulaUtil.SumProduct(monthEclArray2, monthCdfArray2);
                    break;
                default:
                    finalEclValue = lifetimeEadMonth0Value * lifetimeLgdMonth0Value;
                    break;

            }

            return finalEclValue;
        }

        protected double GetMonth0Value(DataTable dataTable, string contractColumn, string monthColumn, string valueColumn, string contractId)
        {
            double month0Value = dataTable.AsEnumerable().FirstOrDefault(x => x.Field<string>(contractColumn) == contractId
                                                                             && x.Field<int>(monthColumn) == 0)
                                                           .Field<double>(valueColumn);

            return month0Value;
        }

        public DataTable ComputeMonthlyEcl()
        {
            DataTable monthlyEcl = new DataTable();
            monthlyEcl.Columns.Add(EclColumns.ContractId, typeof(string));
            monthlyEcl.Columns.Add(EclColumns.EclMonth, typeof(int));
            monthlyEcl.Columns.Add(EclColumns.MonthlyEclValue, typeof(double));

            DataTable lifetimePds = GetLifetimePdResult();
            DataTable lifetimeEads = GetLifetimeEadResult();
            DataTable lifetimeLgds = GetLifetimeLgdResult().AsEnumerable().Where(x => x.Field<int>(LifetimeLgdColumns.Month) != 0).CopyToDataTable();

            foreach (DataRow row in lifetimeLgds.Rows)
            {
                string contractId = row.Field<string>(LifetimeLgdColumns.ContractId);
                string pdGroup = row.Field<string>(LifetimeLgdColumns.PdIndex);
                int month = row.Field<int>(LifetimeLgdColumns.Month);
                double monthlyEclValue = ComputeMonthlyEclValue(lifetimePds, lifetimeEads, row, contractId, pdGroup, month);

                DataRow newRow = monthlyEcl.NewRow();
                newRow[EclColumns.ContractId] = contractId;
                newRow[EclColumns.EclMonth] = month;
                newRow[EclColumns.MonthlyEclValue] = monthlyEclValue;

                monthlyEcl.Rows.Add(newRow);
            }

            return monthlyEcl;
        }

        public double[] ComputeMonthArray(DataTable dataTable, string contractColumnName, string monthColumnName, string valueColumnName, string contractId, int maxMonth)
        {
            double[] monthlyArray = dataTable.AsEnumerable()
                                                     .Where(x => x.Field<string>(contractColumnName) == contractId
                                                              && x.Field<int>(monthColumnName) >= 1
                                                              && x.Field<int>(monthColumnName) <= maxMonth)
                                                     .Select(x =>
                                                     {
                                                         return x.Field<double>(valueColumnName);
                                                     }).ToArray();

            return monthlyArray;
        }

        protected double ComputeMonthlyEclValue(DataTable lifetimePds, DataTable lifetimeEads, DataRow row, string contractId, string pdGroup, int month)
        {
            double lgdValue = row.Field<double>(LifetimeLgdColumns.Value);
            double pdValue = GetLifetimePdValueFromTable(lifetimePds, pdGroup, month);
            double eadValue = GetLifetimeEadValueFromTable(lifetimeEads, contractId, month);
            double monthlyEclValue = pdValue * lgdValue * eadValue;
            return monthlyEclValue;
        }
        protected double GetLifetimeEadValueFromTable(DataTable lifetimeEads, string contractId, int month)
        {
            return lifetimeEads.AsEnumerable()
                                          .FirstOrDefault(x => x.Field<string>(LifetimeEadColumns.ContractId) == contractId
                                                            && x.Field<int>(LifetimeEadColumns.ProjectionMonth) == month)
                                          .Field<double>(LifetimeEadColumns.ProjectionValue);
        }
        protected double GetLifetimePdValueFromTable(DataTable lifetimePds, string pdGroup, int month)
        {
            return lifetimePds.AsEnumerable()
                                        .FirstOrDefault(x => x.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == pdGroup
                                                          && x.Field<long>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) == (month > 120 ? 120 : month))
                                        .Field<double>(MarginalLifetimeRedefaultPdColumns.Value);
        }
        protected DataTable GetLifetimeLgdResult()
        {
            return _lifetimeLgd.ComputeLifetimeLGD();
        }
        protected DataTable GetLifetimeEadResult()
        {
            return _lifetimeEad.ComputeLifetimeEad();
        }
        protected DataTable GetLifetimePdResult()
        {
            switch (_scenario)
            {
                case TempEclData.ScenarioBest:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetLifetimePdBest());
                case TempEclData.ScenarioOptimistic:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetLifetimePdOptimistic());
                case TempEclData.ScenarioDownturn:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetLifetimePdDownturn());
                default:
                    return null;
            }
        }
        protected DataTable GetTempEadInputData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempEadInputsData());
        }
        protected DataTable GetCummulativeDiscountFactor()
        {
            return _irFactorWorkings.ComputeCummulativeDiscountFactor();
        }
        protected DataTable GetStageClassification()
        {
            return _sicrWorkings.ComputeStageClassification();
        }
    }
}
