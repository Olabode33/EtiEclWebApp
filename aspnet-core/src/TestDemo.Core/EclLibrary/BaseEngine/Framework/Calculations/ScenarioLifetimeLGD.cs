using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;
using EclEngine.BaseEclEngine.PdInput;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class ScenarioLifetimeLGD
    {
        protected string _scenario;
        protected PdInputResult _pdInputResult;
        protected SicrWorkings _sicrWorkings;
        protected LifetimeEadWorkings _lifetimeEadWorkings;
        protected ScenarioLifetimeCollateral _scenarioLifetimeCollateral;

        public void Run()
        {
            DataTable dataTable = ComputeLifetimeLGD();
            string stop = "Ma te";
        }
        public ScenarioLifetimeLGD(string scenario)
        {
            _scenario = scenario;
            _pdInputResult = new PdInputResult();
            _sicrWorkings = new SicrWorkings();
            _lifetimeEadWorkings = new LifetimeEadWorkings();
            _scenarioLifetimeCollateral = new ScenarioLifetimeCollateral(_scenario);
        }

        public DataTable ComputeLifetimeLGD()
        {
            DataTable lifetimeLGD = new DataTable();
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.ContractId, typeof(string));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.PdIndex, typeof(string));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.LgdIndex, typeof(string));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.RedefaultLifetimePD, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.CureRate, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.UrBest, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.URDownturn, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.Cor, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.GPd, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.GuarantorLgd, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.GuaranteeValue, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.GuaranteeLevel, typeof(double));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.Stage, typeof(int));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.Month, typeof(int));
            lifetimeLGD.Columns.Add(LifetimeLgdColumns.Value, typeof(double));

            DataTable contractData = GetContractData();
            DataTable pdMapping = GetPdIndexMappingResult();
            DataTable lgdAssumptions = GetLgdAssumptionsData();
            DataTable sicrInput = GetSicrResult();
            DataTable stageClassification = GetStagingClassificationResult();
            DataTable impairmentAssumptions = GetImpairmentAssumptions();
            DataTable lifetimePd = GetScenarioLifetimePdResult();
            DataTable redefaultPd = GetScenarioRedfaultLifetimePdResult();
            DataTable lifetimeEAD = GetLifetimeEadResult();
            DataTable creditIndex = GetCreditRiskResult();
            DataTable lifetimeCollateral = GetScenarioLifetimeCollateralResult();

            int stage2to3Forward = Convert.ToInt32(impairmentAssumptions.AsEnumerable()
                                                               .FirstOrDefault(x => x.Field<string>(ImpairmentRowKeys.ColumnAssumption) == ImpairmentRowKeys.ForwardTransitionStage2to3)
                                                               .Field<string>(ImpairmentRowKeys.ColumnValue));
            //double creditIndexHurdle = Convert.ToDouble(GetImpairmentAssumptionValue(impairmentAssumptions, ImpairmentRowKeys.CreditIndexThreshold));

            foreach (DataRow row in contractData.Rows)
            {
                string contractId = row.Field<string>(TempLgdContractDataColumns.ContractNo);
                double costOfRecovery = row.Field<double>(TempLgdContractDataColumns.CostOfRecovery);
                double guarantorPd = row.Field<double>(TempLgdContractDataColumns.GuaranteePd);
                double guarantorLgd = row.Field<double>(TempLgdContractDataColumns.GuaranteeLgd);
                double guaranteeValue = row.Field<double>(TempLgdContractDataColumns.GuaranteeValue);
                double guaranteeLevel = row.Field<double>(TempLgdContractDataColumns.GuaranteeLevel);

                int loanStage = stageClassification.AsEnumerable()
                                                   .FirstOrDefault(x => x.Field<string>(StageClassificationColumns.ContractId) == contractId)
                                                   .Field<int>(StageClassificationColumns.Stage);

                DataRow pdMappingRow = pdMapping.AsEnumerable().FirstOrDefault(x => x.Field<string>(LoanBookColumns.ContractID) == contractId);
                string pdGroup = pdMappingRow.Field<string>(PdMappingColumns.PdGroup);
                string segment = pdMappingRow.Field<string>(LoanBookColumns.Segment);
                string productType = pdMappingRow.Field<string>(LoanBookColumns.ProductType);

                DataRow sicrInputRow = sicrInput.AsEnumerable().FirstOrDefault(x => x.Field<string>(SicrInputsColumns.ContractId) == contractId);
                double redefaultLifetimePd = sicrInputRow.Field<double>(SicrInputsColumns.RedefaultLifetimePd);
                long daysPastDue = sicrInputRow.Field<long>(SicrInputsColumns.DaysPastDue);

                DataRow bestAssumption = FindDataRowInTable(lgdAssumptions, LgdInputAssumptionColumns.SegementProductType, segment + "_" + productType, 
                                                                            LgdInputAssumptionColumns.Scenario, TempEclData.ScenarioBest);
                DataRow downturnAssumption = FindDataRowInTable(lgdAssumptions, LgdInputAssumptionColumns.SegementProductType, segment + "_" + productType, 
                                                                                LgdInputAssumptionColumns.Scenario, TempEclData.ScenarioDownturn);

                double cureRates = bestAssumption.Field<double>(LgdInputAssumptionColumns.CureRate);

                long lgdAssumptionColumn = Math.Max(daysPastDue - stage2to3Forward, 0);
                double unsecuredRecoveriesBest = bestAssumption.Field<double>(lgdAssumptionColumn.ToString());
                double unsecuredRecoveriesDownturn = downturnAssumption.Field<double>(lgdAssumptionColumn.ToString());


                for (int month = 0; month < TempEclData.TempExcelVariable_LIM_MONTH; month++)
                {

                    if(month == 10)
                    {
                        string stop = "stop";
                    }

                    double monthLifetimeEAD = GetLifetimeEADPerMonth(lifetimeEAD, contractId, month);  //Excel lifetimeEAD!F
                    double monthCreditIndex = GetCreditIndexPerMonth(creditIndex, month);           // Excel $O$3
                    double sumLifetimePds = ComputeLifetimeRedefaultPdValuePerMonth(lifetimePd, pdGroup, month);   //Excel Sum(OFFSET(PD_BE, $C8-1, 1, 1, O$7))
                    double sumRedefaultPds = ComputeLifetimeRedefaultPdValuePerMonth(redefaultPd, pdGroup, month); //Excel SUM(OFFSET(RD_PD_BE, $C8-1, 1, 1, O$7)))
                    double lifetimeCollateralValue = ComputeLifetimeCollateralValuePerMonth(lifetimeCollateral, contractId, month);  // Excel 'Lifetime Collateral (BE)'!E4



                    DataRow newRow = lifetimeLGD.NewRow();

                    double month1pdValue = ComputeLifetimeRedefaultPdValuePerMonth(lifetimePd, pdGroup, 1);  // Excel INDEX(PD_BE,$C8, 2)
                    double resultUsingMonth1pdValue = month1pdValue == 1.0 ? 1.0 : 0.0; // IF(INDEX(PD_BE,$C8, 2) = 1,1,0),
                    double redefaultCalculation = (redefaultLifetimePd - sumRedefaultPds) / (1 - sumLifetimePds);
                    double maxRedefaultPdValue = Math.Max(redefaultCalculation, 0.0);
                    double ifSumLifetimePd = sumLifetimePds == 1.0 ? resultUsingMonth1pdValue : maxRedefaultPdValue;
                    double checkForMonth0 = month == 0.0 ? redefaultLifetimePd : ifSumLifetimePd;
                    double checkForStage1 = loanStage != 1.0 ? cureRates * checkForMonth0 : 0.0;
                    double maxCurerateResult = Math.Max((1.0 - cureRates) + checkForStage1, 0.0);  //result not in double
                    double minMaxCureRateResult = Math.Min(maxCurerateResult, 1.0);  
                    ///
                    double lifetimeCollateralForMonthCor = lifetimeCollateralValue * (1 - costOfRecovery);
                    double min_gvalue_glevel = Math.Min(guaranteeValue, guaranteeLevel * monthLifetimeEAD);
                    double gLgd_gPd = (1 - guarantorLgd * guarantorPd);
                    double multiplerMinColl = (gLgd_gPd * min_gvalue_glevel) + lifetimeCollateralForMonthCor;
                    ///

                    double creditIndexHurdle = Convert.ToDouble(GetImpairmentAssumptionValue(impairmentAssumptions, ImpairmentRowKeys.CreditIndexThreshold));

                    double ifCreditIndexHurdle;
                    if (monthCreditIndex > creditIndexHurdle)
                    {
                        ifCreditIndexHurdle = ((1 - unsecuredRecoveriesDownturn) * multiplerMinColl) + (unsecuredRecoveriesDownturn * monthLifetimeEAD);
                    }
                    else
                    {
                        ifCreditIndexHurdle = ((1 - unsecuredRecoveriesBest) * multiplerMinColl) + (unsecuredRecoveriesBest * monthLifetimeEAD);
                    }

                    double maxCreditIndexHurdle = Math.Max(1 - (ifCreditIndexHurdle) / monthLifetimeEAD, 0);
                    double minMaxCreditIndexHurdle = Math.Min(maxCreditIndexHurdle, 1);
                    double lifetimeLgdValue = monthLifetimeEAD == 0 ? 0 : minMaxCureRateResult * minMaxCreditIndexHurdle;


                    newRow[LifetimeLgdColumns.ContractId] = contractId;
                    newRow[LifetimeLgdColumns.PdIndex] = pdGroup;
                    newRow[LifetimeLgdColumns.LgdIndex] = segment + "_" + productType;
                    newRow[LifetimeLgdColumns.RedefaultLifetimePD] = redefaultLifetimePd;
                    newRow[LifetimeLgdColumns.CureRate] = cureRates;
                    newRow[LifetimeLgdColumns.UrBest] = unsecuredRecoveriesBest;
                    newRow[LifetimeLgdColumns.URDownturn] = unsecuredRecoveriesDownturn;
                    newRow[LifetimeLgdColumns.Cor] = costOfRecovery;
                    newRow[LifetimeLgdColumns.GPd] = guarantorPd;
                    newRow[LifetimeLgdColumns.GuarantorLgd] = guarantorLgd;
                    newRow[LifetimeLgdColumns.GuaranteeValue] = guaranteeValue;
                    newRow[LifetimeLgdColumns.GuaranteeLevel] = guaranteeLevel;
                    newRow[LifetimeLgdColumns.Stage] = loanStage;
                    newRow[LifetimeLgdColumns.Month] = month;
                    newRow[LifetimeLgdColumns.Value] = lifetimeLgdValue;
                    lifetimeLGD.Rows.Add(newRow);
                }
            }


            return lifetimeLGD;
        }

        protected DataRow FindDataRowInTable(DataTable dataTable, string searchColumn, string searchValue, string searchColumn2, string searchValue2)
        {
            DataRow row = dataTable.AsEnumerable().FirstOrDefault(x => x.Field<string>(searchColumn) == searchValue
                                                                    && x.Field<string>(searchColumn2) == searchValue2);
            return row;
        }
        protected DataRow FindDataRowInTable(DataTable dataTable, string searchColumn, string searchValue, string searchColumn2, int searchValue2)
        {
            DataRow row = dataTable.AsEnumerable().FirstOrDefault(x => x.Field<string>(searchColumn) == searchValue
                                                                    && x.Field<int>(searchColumn2) == searchValue2);
            return row;
        }
        protected double GetLifetimeEADPerMonth(DataTable lifetimeEAD, string contractId, int month)
        {
            return lifetimeEAD.AsEnumerable().FirstOrDefault(x => x.Field<string>(LifetimeEadColumns.ContractId) == contractId
                                                                                                      && x.Field<int>(LifetimeEadColumns.ProjectionMonth) == month)
                                                                                    .Field<double>(LifetimeEadColumns.ProjectionValue);
        }

        protected double GetCreditIndexPerMonth(DataTable creditIndex, int month)
        {
            string creditIndexColumn = CreditIndexColumns.BestEstimate;

            switch (_scenario)
            {
                case TempEclData.ScenarioBest:
                    creditIndexColumn = CreditIndexColumns.BestEstimate;
                    break;
                case TempEclData.ScenarioOptimistic:
                    creditIndexColumn = CreditIndexColumns.Optimistic;
                    break;
                case TempEclData.ScenarioDownturn:
                    creditIndexColumn = CreditIndexColumns.Downturn;
                    break;
                default:
                    creditIndexColumn = CreditIndexColumns.BestEstimate;
                    break;
            }

            return creditIndex.AsEnumerable().FirstOrDefault(x => x.Field<string>(CreditIndexColumns.ProjectionMonth) == (month > 60 ? "60" : month.ToString()))
                                                                                    .Field<double>(creditIndexColumn);
        }

        protected double ComputeLifetimeRedefaultPdValuePerMonth(DataTable lifetimePd, string pdGroup, int month)
        {
            double[] pds = lifetimePd.AsEnumerable().Where(x => x.Field<string>(MarginalLifetimeRedefaultPdColumns.PdGroup) == pdGroup
                                                                          && x.Field<long>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) >= 1
                                                                          && x.Field<long>(MarginalLifetimeRedefaultPdColumns.ProjectionMonth) <= month)
                                                                   .Select(x =>
                                                                   {
                                                                       return x.Field<double>(MarginalLifetimeRedefaultPdColumns.Value);
                                                                   }).ToArray();
            return pds.Aggregate(0.0, (acc, x) => acc + x);
        }

        protected double ComputeLifetimeCollateralValuePerMonth(DataTable lifetimeCollateral, string contractId, int month)
        {
            double lifetimeCollateralValue = lifetimeCollateral.AsEnumerable().FirstOrDefault(x => x.Field<string>(LifetimeCollateralColumns.ContractId) == contractId
                                                                                                                    && x.Field<int>(LifetimeCollateralColumns.ProjectionMonth) == month)
                                                                                                  .Field<double>(LifetimeCollateralColumns.ProjectionValue);
            return lifetimeCollateralValue;
        }
        protected string GetImpairmentAssumptionValue(DataTable assumptions, string assumptionKey)
        {
            return assumptions.AsEnumerable()
                              .FirstOrDefault(x => x.Field<string>(ImpairmentRowKeys.ColumnAssumption) == assumptionKey)
                              .Field<string>(ImpairmentRowKeys.ColumnValue);
        }

        protected DataTable GetImpairmentAssumptions()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetImpairmentAssumptionsData());
        }
        protected DataTable GetContractData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdContractDataData());
        }
        protected DataTable GetPdIndexMappingResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInputResult.GetPdMapping());
        }
        protected DataTable GetLgdAssumptionsData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetTempLgdInputAssumptionsData());
        }

        protected DataTable GetSicrResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInputResult.GetSicrInputs());
        }

        protected DataTable GetStagingClassificationResult()
        {
            return _sicrWorkings.ComputeStageClassification();
        }
        protected DataTable GetLifetimeEadResult()
        {
            return _lifetimeEadWorkings.ComputeLifetimeEad();
        }
        protected DataTable GetScenarioLifetimePdResult()
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
        protected DataTable GetScenarioRedfaultLifetimePdResult()
        {
            switch (_scenario)
            {
                case TempEclData.ScenarioBest:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetRedefaultLifetimePdBest());
                case TempEclData.ScenarioOptimistic:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetRedefaultLifetimePdOptimistic());
                case TempEclData.ScenarioDownturn:
                    return JsonUtil.DeserializeToDatatable(_pdInputResult.GetRedefaultLifetimePdDownturn());
                default:
                    return null;
            }
        }
        protected DataTable GetCreditRiskResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInputResult.GetCreditIndex());
        }
        protected DataTable GetScenarioLifetimeCollateralResult()
        {
            return _scenarioLifetimeCollateral.ComputeLifetimeCollateral();
        }
    }
}
