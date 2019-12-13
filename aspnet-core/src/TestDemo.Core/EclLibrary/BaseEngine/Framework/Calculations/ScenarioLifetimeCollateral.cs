using EclEngine.BaseEclEngine.PdInput;
using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class ScenarioLifetimeCollateral
    {
        protected string _scenario;
        protected IrFactorWorkings _irFactorWorkings;
        protected UpdatedFSVsWorkings _updatedFSVsWorkings;

        public ScenarioLifetimeCollateral(string scenario)
        {
            _scenario = scenario;
            _irFactorWorkings = new IrFactorWorkings();
            _updatedFSVsWorkings = new UpdatedFSVsWorkings();
        }
        public void Run()
        {
            DataTable dataTable = ComputeLifetimeCollateral();
            string stop = "Ma te";
        }
        public DataTable ComputeLifetimeCollateral()
        {
            DataTable lifetimeCollateral = new DataTable();
            lifetimeCollateral.Columns.Add(LifetimeCollateralColumns.ContractId, typeof(string));
            lifetimeCollateral.Columns.Add(LifetimeCollateralColumns.EirIndex, typeof(int));
            lifetimeCollateral.Columns.Add(LifetimeCollateralColumns.TtrMonths, typeof(int));
            lifetimeCollateral.Columns.Add(LifetimeCollateralColumns.ProjectionMonth, typeof(int));
            lifetimeCollateral.Columns.Add(LifetimeCollateralColumns.ProjectionValue, typeof(double));

            DataTable contractData = GetContractData();
            DataTable marginalDiscountFactor = GetMarginalDiscountFactor();
            DataTable eadInputs = GetTempEadInputData();
            DataTable collateralProjections = GetScenarioCollateralProjection();
            DataTable updatedFsv = GetUpdatedFsvResult();

            foreach (DataRow row in contractData.Rows)
            {
                string contractId = row.Field<string>(TempLgdContractDataColumns.ContractNo);
                string eirGroup = eadInputs.AsEnumerable().FirstOrDefault(x => x.Field<string>(EadInputsColumns.ContractId) == contractId).Field<string>(EadInputsColumns.EirGroup);
                int eirIndex = marginalDiscountFactor.AsEnumerable()
                                                         .FirstOrDefault(x => x.Field<string>(IrFactorColumns.EirGroup) == eirGroup)
                                                         .Field<int>(IrFactorColumns.GroupRank);
                int ttrMonth = Convert.ToInt32(Math.Round(row.Field<double>(TempLgdContractDataColumns.TtrYears) * 12, 0));
                DataRow tempFsv = updatedFsv.AsEnumerable().FirstOrDefault(x => x.Field<string>(TypeCollateralTypeColumns.ContractNo) == contractId);
                double[] fsvArray = new double[9];
                fsvArray[0] = tempFsv.Field<double>(TypeCollateralTypeColumns.Cash);
                fsvArray[1] = tempFsv.Field<double>(TypeCollateralTypeColumns.CommercialProperty);
                fsvArray[2] = tempFsv.Field<double>(TypeCollateralTypeColumns.Debenture);
                fsvArray[3] = tempFsv.Field<double>(TypeCollateralTypeColumns.Inventory);
                fsvArray[4] = tempFsv.Field<double>(TypeCollateralTypeColumns.PlantAndEquipment);
                fsvArray[5] = tempFsv.Field<double>(TypeCollateralTypeColumns.Receivables);
                fsvArray[6] = tempFsv.Field<double>(TypeCollateralTypeColumns.ResidentialProperty);
                fsvArray[7] = tempFsv.Field<double>(TypeCollateralTypeColumns.Shares);
                fsvArray[8] = tempFsv.Field<double>(TypeCollateralTypeColumns.Vehicle);

                for (int month = 0; month < TempEclData.MaxIrFactorProjectionMonths; month++)
                {
                    double product = GetProductValue(marginalDiscountFactor, eirIndex, ttrMonth, month);
                    double sumProduct = GetSumProductValue(collateralProjections, ttrMonth, fsvArray, month);
                    double value = product * sumProduct;

                    DataRow newRow = lifetimeCollateral.NewRow();
                    newRow[LifetimeCollateralColumns.ContractId] = contractId;
                    newRow[LifetimeCollateralColumns.EirIndex] = eirIndex;
                    newRow[LifetimeCollateralColumns.TtrMonths] = ttrMonth;
                    newRow[LifetimeCollateralColumns.ProjectionMonth] = month;
                    newRow[LifetimeCollateralColumns.ProjectionValue] = value;

                    lifetimeCollateral.Rows.Add(newRow);
                }
            }


            return lifetimeCollateral;
        }

        private static double GetSumProductValue(DataTable collateralProjections, int ttrMonth, double[] fsvArray, int month)
        {
            int minMonth = Math.Min(1 + month + ttrMonth, TempEclData.TempExcelVariable_LIM_CM);
            DataRow projectionsDr = collateralProjections.AsEnumerable().FirstOrDefault(x => x.Field<double>(TypeCollateralTypeColumns.ProjectionMonth) == minMonth);
            double[] projections = new double[9];
            projections[0] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Cash);
            projections[1] = projectionsDr.Field<double>(TypeCollateralTypeColumns.CommercialProperty);
            projections[2] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Debenture);
            projections[3] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Inventory);
            projections[4] = projectionsDr.Field<double>(TypeCollateralTypeColumns.PlantAndEquipment);
            projections[5] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Receivables);
            projections[6] = projectionsDr.Field<double>(TypeCollateralTypeColumns.ResidentialProperty);
            projections[7] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Shares);
            projections[8] = projectionsDr.Field<double>(TypeCollateralTypeColumns.Vehicle);

            double sumProduct = ExcelFormulaUtil.SumProduct(fsvArray, projections);
            return sumProduct;
        }

        protected static double GetProductValue(DataTable marginalDiscountFactor, int eirIndex, int ttrMonth, int month)
        {
            double[] temp = marginalDiscountFactor.AsEnumerable()
                                                             .Where(x => x.Field<int>(IrFactorColumns.GroupRank) == eirIndex
                                                                                && (x.Field<int>(IrFactorColumns.ProjectionMonth) >= 2 + month) && x.Field<int>(IrFactorColumns.ProjectionMonth) <= ttrMonth)
                                                             .Select(x =>
                                                             {
                                                                 return x.Field<double>(IrFactorColumns.ProjectionValue);
                                                             }).ToArray();
            double product = temp.Aggregate(1.0, (acc, x) => acc * x);
            return product;
        }

        protected DataTable GetContractData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdContractDataData());
        }
        protected DataTable GetMarginalDiscountFactor()
        {
            return _irFactorWorkings.ComputeMarginalDiscountFactor();
        }
        protected DataTable GetTempEadInputData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempEadInputsData());
        }
        protected DataTable GetScenarioCollateralProjection()
        {
            switch (_scenario)
            {
                case TempEclData.ScenarioBest:
                    return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdCollateralProjectionBestData());
                case TempEclData.ScenarioOptimistic:
                    return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdCollateralProjectionOptimisticData());
                case TempEclData.ScenarioDownturn:
                    return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdCollateralProjectionDownturnData());
                default:
                    return null;
            }
        }
        protected DataTable GetUpdatedFsvResult()
        {
            return _updatedFSVsWorkings.ComputeUpdatedFSV();
        }
    }
}
