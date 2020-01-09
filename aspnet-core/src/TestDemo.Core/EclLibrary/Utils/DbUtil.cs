using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EclEngine.Utils
{
    public static class DbUtil
    {
        private const string testAccountNo = "152007783";
        private const string _serverName = "localhost";
        private const string _username = "sa";
        private const string _password = "";
        private const string _databaseName = "ETI_IFRS9_DB";
        private const string _dumpDatabaseName = "eti_ifrs9_dump_db";
        private const string _loanBookQuery = @"SELECT top 100 ContractId,CustomerNo,AccountNo,ContractNo,CustomerName,SnapshotDate
                                                  ,Segment,Sector,Currency,ProductType,ProductMapping,SpecialisedLending,RatingModel
                                                  ,OriginalRating,CurrentRating,LifetimePD,Month12PD,DaysPastDue,WatchlistIndicator
                                                  ,Classification,ImpairedDate,DefaultDate,CreditLimit,OriginalBalanceLCY,OutstandingBalanceLCY,OutstandingBalanceACY
                                                  ,ContractStartDate,ContractEndDate,RestructureIndicator,RestructureRisk,RestructureType,RestructureStartDate
                                                  ,RestructureEndDate,PrincipalPaymentTermsOrigination,PPTOPeriod,InterestPaymentTermsOrigination
                                                  ,IPTOPeriod,PrincipalPaymentStructure,InterestPaymentStructure,InterestRateType,BaseRate,OriginationContractualInterestRate
                                                  ,IntroductoryPeriod,PostIPContractualInterestRate,CurrentContractualInterestRate,EIR,DebentureOMV,DebentureFSV
                                                  ,CashOMV,CashFSV,InventoryOMV,InventoryFSV,PlantEquipmentOMV,PlantEquipmentFSV,ResidentialPropertyOMV,ResidentialPropertyFSV
                                                  ,CommercialPropertyOMV,CommercialProperty,ReceivablesOMV,ReceivablesFSV,SharesOMV,SharesFSV,VehicleOMV
                                                  ,VehicleFSV,CureRate,GuaranteeIndicator,GuarantorPD,GuarantorLGD,GuaranteeValue,GuaranteeLevel
                                              FROM WholesaleEclDataLoanBooks ";
                                              //Where ContractNo = '" + testAccountNo + "'";
        private const string _12MonthsPdQuery = @"SELECT [Credit Rating]
                                                  ,[PD]
                                                  ,[S&P Mapping (ETI Credit Policy)]
                                                  ,[S&P Mapping (Best Fit)]
                                                FROM [PDI_12MonthPds]";
        private const string _etiNplQuery = @"SELECT [Date],[Series]
                                                 FROM [PDI_EtiNpl]";
        private const string _historicIndexQuery = @"SELECT [Date]
                                                        ,[Actual]
                                                        ,[Standardised]
                                                    FROM [PDI_HistoricIndex]";
        private const string _macroEcoBestQuery = @"SELECT [Date]
                                                      ,[Prime Lending Rate (%)]
                                                      ,[Oil Exports (USD'm)]
                                                      ,[Real GDP Growth Rate (%)]
                                                      ,[Differenced Real GDP Growth Rate (%)]
                                                  FROM [PDI_MacroEcoBest]";
        private const string _macroEcoDownturnQuery = @"SELECT [Date]
                                                          ,[Prime Lending Rate (%)]
                                                          ,[Oil Exports (USD'm)]
                                                          ,[Real GDP Growth Rate (%)]
                                                          ,[Differenced Real GDP Growth Rate (%)]
                                                      FROM [PDI_MacroEcoDownturn]";
        private const string _macroEcoOptimisticQuery = @"SELECT [Date]
                                                            ,[Prime Lending Rate (%)]
                                                            ,[Oil Exports (USD'm)]
                                                            ,[Real GDP Growth Rate (%)]
                                                            ,[Differenced Real GDP Growth Rate (%)]
                                                        FROM [PDI_MacroEcoOptimisit]";
        private const string _nonInternalmodelInputQuery = @"SELECT [Month]
                                                            ,[CONS_STAGE_1]
                                                            ,[CONS_STAGE_2]
                                                            ,[COMM_STAGE_1]
                                                            ,[COMM_STAGE_2]
                                                        FROM [PDI_NonInternalModelInputs]";
        private const string _snpCummulativeDefaultRateQuery = @"SELECT [Rating]
                                                              ,[1]
                                                              ,[2]
                                                              ,[3]
                                                              ,[4]
                                                              ,[5]
                                                              ,[6]
                                                              ,[7]
                                                              ,[8]
                                                              ,[9]
                                                              ,[10]
                                                              ,[11]
                                                              ,[12]
                                                              ,[13]
                                                              ,[14]
                                                              ,[15]
                                                              ,[12 Month PD]
                                                          FROM [PDI_SnPCummlativeDefaultRate]";
        private const string _statisticalInputsQuery = @"SELECT [Mode]
                                                            ,[Prime Lending Rate (%)]
                                                            ,[Oil Exports (USD'm)]
                                                            ,[Real GDP Growth Rate (%)]
                                                        FROM [PDI_StatisticalInputs]";
        private const string _pdInputAssumptionsQuery = @"SELECT [Assumptions]
                                                                ,[Value]
                                                            FROM [PDI_Assumptions]";
        private const string _impairmentAssumptionsQuery = @"SELECT [Assumption]
                                                                  ,[Value]
                                                              FROM [ImpairmentAssumptions]";
        private const string _tempLgdContractDataQuery = @"SELECT [CONTRACT_NO]
                                                              ,[ACCOUNT_NO]
                                                              ,[CUSTOMER_NO]
                                                              ,[PRODUCT_TYPE]
                                                              ,[TTR_YEARS]
                                                              ,[COST_OF_RECOVERY_%]
                                                              ,[GUARANTOR_PD]
                                                              ,[GUARANTOR_LGD]
                                                              ,[GUARANTEE_VALUE]
                                                              ,[GUARANTEE_LEVEL]
                                                          FROM [TempLGDContractData]
                                                          Where [CONTRACT_NO] = '"+ testAccountNo +"'";
        private const string _tempLgdCollateralProjectionOptimisticQuery = @"SELECT [MONTH]
                                                                                ,[CASH]
                                                                                ,[COMMERCIAL_PROPERTY]
                                                                                ,[DEBENTURE]
                                                                                ,[INVENTORY]
                                                                                ,[PLANT_AND_EQUIPMENT]
                                                                                ,[RECEIVABLES]
                                                                                ,[RESIDENTIAL_PROPERTY]
                                                                                ,[SHARES]
                                                                                ,[VEHICLE]
                                                                            FROM [TempLGDCollateralProjectionOptimistic]";
        private const string _tempLgdCollateralProjectionDownturnQuery = @"SELECT [MONTH]
                                                                                  ,[CASH]
                                                                                  ,[COMMERCIAL_PROPERTY]
                                                                                  ,[DEBENTURE]
                                                                                  ,[INVENTORY]
                                                                                  ,[PLANT_AND_EQUIPMENT]
                                                                                  ,[RECEIVABLES]
                                                                                  ,[RESIDENTIAL_PROPERTY]
                                                                                  ,[SHARES]
                                                                                  ,[VEHICLE]
                                                                              FROM [TempLGDCollateralProjectionDownturn]";
        private const string _tempLgdCollateralProjectionBestQuery = @"SELECT [MONTH]
                                                                              ,[CASH]
                                                                              ,[COMMERCIAL_PROPERTY]
                                                                              ,[DEBENTURE]
                                                                              ,[INVENTORY]
                                                                              ,[PLANT_AND_EQUIPMENT]
                                                                              ,[RECEIVABLES]
                                                                              ,[RESIDENTIAL_PROPERTY]
                                                                              ,[SHARES]
                                                                              ,[VEHICLE]
                                                                          FROM [TempLGDCollateralProjectionBest]";
        private const string _tempLgdCollateralTypeOmvQuery = @"SELECT [CONTRACT_NO]
                                                                    ,[CASH]
                                                                    ,[COMMERCIAL_PROPERTY]
                                                                    ,[DEBENTURE]
                                                                    ,[INVENTORY]
                                                                    ,[PLANT_AND_EQUIPMENT]
                                                                    ,[RECEIVABLES]
                                                                    ,[RESIDENTIAL_PROPERTY]
                                                                    ,[SHARES]
                                                                    ,[VEHICLE]
                                                                FROM [TempLGDCollateralTypeOMV]";
        private const string _tempLgdCollateralTypeFsvQuery = @"SELECT [CONTRACT_NO]
                                                                    ,[CASH]
                                                                    ,[COMMERCIAL_PROPERTY]
                                                                    ,[DEBENTURE]
                                                                    ,[INVENTORY]
                                                                    ,[PLANT_AND_EQUIPMENT]
                                                                    ,[RECEIVABLES]
                                                                    ,[RESIDENTIAL_PROPERTY]
                                                                    ,[SHARES]
                                                                    ,[VEHICLE]
                                                                FROM [TempLGDCollateralTypeFSV]";
        private const string _tempEadInputQuery = @"SELECT top 100 [CONTRACT_ID]
      ,[EIR_GROUP]
      ,[CIR_GROUP]
      ,[0]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[32]
      ,[33]
      ,[34]
      ,[35]
      ,[36]
      ,[37]
      ,[38]
      ,[39]
      ,[40]
      ,[41]
      ,[42]
      ,[43]
      ,[44]
      ,[45]
      ,[46]
      ,[47]
      ,[48]
      ,[49]
      ,[50]
      ,[51]
      ,[52]
      ,[53]
      ,[54]
      ,[55]
      ,[56]
      ,[57]
      ,[58]
      ,[59]
      ,[60]
      ,[61]
      ,[62]
      ,[63]
      ,[64]
      ,[65]
      ,[66]
      ,[67]
      ,[68]
      ,[69]
      ,[70]
      ,[71]
      ,[72]
      ,[73]
      ,[74]
      ,[75]
      ,[76]
      ,[77]
      ,[78]
      ,[79]
      ,[80]
      ,[81]
      ,[82]
      ,[83]
      ,[84]
      ,[85]
      ,[86]
      ,[87]
      ,[88]
      ,[89]
      ,[90]
      ,[91]
      ,[92]
      ,[93]
      ,[94]
      ,[95]
      ,[96]
      ,[97]
      ,[98]
      ,[99]
      ,[100]
      ,[101]
      ,[102]
      ,[103]
      ,[104]
      ,[105]
      ,[106]
      ,[107]
      ,[108]
      ,[109]
      ,[110]
      ,[111]
      ,[112]
      ,[113]
      ,[114]
      ,[115]
      ,[116]
      ,[117]
      ,[118]
      ,[119]
      ,[120]
      ,[121]
      ,[122]
      ,[123]
      ,[124]
      ,[125]
      ,[126]
      ,[127]
      ,[128]
      ,[129]
      ,[130]
      ,[131]
      ,[132]
      ,[133]
      ,[134]
      ,[135]
      ,[136]
      ,[137]
      ,[138]
      ,[139]
      ,[140]
      ,[141]
      ,[142]
      ,[143]
      ,[144]
      ,[145]
      ,[146]
      ,[147]
      ,[148]
      ,[149]
      ,[150]
      ,[151]
      ,[152]
      ,[153]
      ,[154]
      ,[155]
      ,[156]
      ,[157]
      ,[158]
      ,[159]
      ,[160]
      ,[161]
      ,[162]
      ,[163]
      ,[164]
      ,[165]
      ,[166]
      ,[167]
      ,[168]
      ,[169]
      ,[170]
      ,[171]
      ,[172]
      ,[173]
      ,[174]
      ,[175]
      ,[176]
      ,[177]
      ,[178]
      ,[179]
      ,[180]
      ,[181]
      ,[182]
      ,[183]
      ,[184]
      ,[185]
      ,[186]
      ,[187]
      ,[188]
      ,[189]
      ,[190]
      ,[191]
      ,[192]
      ,[193]
      ,[194]
      ,[195]
      ,[196]
      ,[197]
      ,[198]
      ,[199]
      ,[200]
      ,[201]
      ,[202]
      ,[203]
      ,[204]
  FROM [TempEADInputs]
  Where [CONTRACT_ID] = '" + testAccountNo + "'";
        private const string _tempEirProjectionsQuery = @"/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [EIR_GROUPS]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[32]
      ,[33]
      ,[34]
      ,[35]
      ,[36]
      ,[37]
      ,[38]
      ,[39]
      ,[40]
      ,[41]
      ,[42]
      ,[43]
      ,[44]
      ,[45]
      ,[46]
      ,[47]
      ,[48]
      ,[49]
      ,[50]
      ,[51]
      ,[52]
      ,[53]
      ,[54]
      ,[55]
      ,[56]
      ,[57]
      ,[58]
      ,[59]
      ,[60]
      ,[61]
      ,[62]
      ,[63]
      ,[64]
      ,[65]
      ,[66]
      ,[67]
      ,[68]
      ,[69]
      ,[70]
      ,[71]
      ,[72]
      ,[73]
      ,[74]
      ,[75]
      ,[76]
      ,[77]
      ,[78]
      ,[79]
      ,[80]
      ,[81]
      ,[82]
      ,[83]
      ,[84]
      ,[85]
      ,[86]
      ,[87]
      ,[88]
      ,[89]
      ,[90]
      ,[91]
      ,[92]
      ,[93]
      ,[94]
      ,[95]
      ,[96]
      ,[97]
      ,[98]
      ,[99]
      ,[100]
      ,[101]
      ,[102]
      ,[103]
      ,[104]
      ,[105]
      ,[106]
      ,[107]
      ,[108]
      ,[109]
      ,[110]
      ,[111]
      ,[112]
      ,[113]
      ,[114]
      ,[115]
      ,[116]
      ,[117]
      ,[118]
      ,[119]
      ,[120]
      ,[121]
      ,[122]
      ,[123]
      ,[124]
      ,[125]
      ,[126]
      ,[127]
      ,[128]
      ,[129]
      ,[130]
      ,[131]
      ,[132]
      ,[133]
      ,[134]
      ,[135]
      ,[136]
      ,[137]
      ,[138]
      ,[139]
      ,[140]
      ,[141]
      ,[142]
      ,[143]
      ,[144]
      ,[145]
      ,[146]
      ,[147]
      ,[148]
      ,[149]
      ,[150]
      ,[151]
      ,[152]
      ,[153]
      ,[154]
      ,[155]
      ,[156]
      ,[157]
      ,[158]
      ,[159]
      ,[160]
      ,[161]
      ,[162]
      ,[163]
      ,[164]
      ,[165]
      ,[166]
      ,[167]
      ,[168]
      ,[169]
      ,[170]
      ,[171]
      ,[172]
      ,[173]
      ,[174]
      ,[175]
      ,[176]
      ,[177]
      ,[178]
      ,[179]
      ,[180]
      ,[181]
      ,[182]
      ,[183]
      ,[184]
      ,[185]
      ,[186]
      ,[187]
      ,[188]
      ,[189]
      ,[190]
      ,[191]
      ,[192]
      ,[193]
      ,[194]
      ,[195]
      ,[196]
      ,[197]
      ,[198]
      ,[199]
      ,[200]
      ,[201]
      ,[202]
      ,[203]
      ,[204]
  FROM [TempEADEirProjections]";
        private const string _tempCirProjectionQuery = @"SELECT [CIR_GROUPS]
      ,[1]
      ,[2]
      ,[3]
      ,[4]
      ,[5]
      ,[6]
      ,[7]
      ,[8]
      ,[9]
      ,[10]
      ,[11]
      ,[12]
      ,[13]
      ,[14]
      ,[15]
      ,[16]
      ,[17]
      ,[18]
      ,[19]
      ,[20]
      ,[21]
      ,[22]
      ,[23]
      ,[24]
      ,[25]
      ,[26]
      ,[27]
      ,[28]
      ,[29]
      ,[30]
      ,[31]
      ,[32]
      ,[33]
      ,[34]
      ,[35]
      ,[36]
      ,[37]
      ,[38]
      ,[39]
      ,[40]
      ,[41]
      ,[42]
      ,[43]
      ,[44]
      ,[45]
      ,[46]
      ,[47]
      ,[48]
      ,[49]
      ,[50]
      ,[51]
      ,[52]
      ,[53]
      ,[54]
      ,[55]
      ,[56]
      ,[57]
      ,[58]
      ,[59]
      ,[60]
      ,[61]
      ,[62]
      ,[63]
      ,[64]
      ,[65]
      ,[66]
      ,[67]
      ,[68]
      ,[69]
      ,[70]
      ,[71]
      ,[72]
      ,[73]
      ,[74]
      ,[75]
      ,[76]
      ,[77]
      ,[78]
      ,[79]
      ,[80]
      ,[81]
      ,[82]
      ,[83]
      ,[84]
      ,[85]
      ,[86]
      ,[87]
      ,[88]
      ,[89]
      ,[90]
      ,[91]
      ,[92]
      ,[93]
      ,[94]
      ,[95]
      ,[96]
      ,[97]
      ,[98]
      ,[99]
      ,[100]
      ,[101]
      ,[102]
      ,[103]
      ,[104]
      ,[105]
      ,[106]
      ,[107]
      ,[108]
      ,[109]
      ,[110]
      ,[111]
      ,[112]
      ,[113]
      ,[114]
      ,[115]
      ,[116]
      ,[117]
      ,[118]
      ,[119]
      ,[120]
      ,[121]
      ,[122]
      ,[123]
      ,[124]
      ,[125]
      ,[126]
      ,[127]
      ,[128]
      ,[129]
      ,[130]
      ,[131]
      ,[132]
      ,[133]
      ,[134]
      ,[135]
      ,[136]
      ,[137]
      ,[138]
      ,[139]
      ,[140]
      ,[141]
      ,[142]
      ,[143]
      ,[144]
      ,[145]
      ,[146]
      ,[147]
      ,[148]
      ,[149]
      ,[150]
      ,[151]
      ,[152]
      ,[153]
      ,[154]
      ,[155]
      ,[156]
      ,[157]
      ,[158]
      ,[159]
      ,[160]
      ,[161]
      ,[162]
      ,[163]
      ,[164]
      ,[165]
      ,[166]
      ,[167]
      ,[168]
      ,[169]
      ,[170]
      ,[171]
      ,[172]
      ,[173]
      ,[174]
      ,[175]
      ,[176]
      ,[177]
      ,[178]
      ,[179]
      ,[180]
      ,[181]
      ,[182]
      ,[183]
      ,[184]
      ,[185]
      ,[186]
      ,[187]
      ,[188]
      ,[189]
      ,[190]
      ,[191]
      ,[192]
      ,[193]
      ,[194]
      ,[195]
      ,[196]
      ,[197]
      ,[198]
      ,[199]
      ,[200]
      ,[201]
      ,[202]
      ,[203]
      ,[204]
  FROM[TempEADCirProjections]";
        private const string _tempLgdInputAssumptions = @"SELECT [SEGMENT_PRODUCT_TYPE]
      ,[CURE_RATE]
      ,[Scenario]
      ,[0]
      ,[90]
      ,[180]
      ,[270]
      ,[360]
  FROM [LgdInputAssumptions]";

        private const double _indexWeightW1 = 0.58;
        private const double _indexWeightW2 = 0.42;
        private const double _statisticsStandardDeviation = 0.84;
        private const double _statisticsAverage = 0.00;

        private static Guid _fakeEclId = Guid.Parse("4698AF52-31AB-4C10-80EE-29B8CB409113");

        public static string _tempResulPdMappings = "TestResultPdMappings";
        public static string _tempResulPdLifetimeBest = "TestResultPdLifetimeBests";
        public static string _tempResulPdLifetimeOptimistics = "TestResultPdLifetimeOptimistics";
        public static string _tempResulPdLifetimeDownturn = "TestResultPdLifetimeDownturns";
        public static string _tempResultPdRedefaultLifetimeBest = "TestResultPdRedefaultLifetimeBests";
        public static string _tempResultPdRedefaultLifetimeOptimistics = "TestResultPdRedefaultLifetimeOptimistics";
        public static string _tempResultPdRedefaultLifetimeDownturn = "TestResultPdRedefaultLifetimeDownturns";
        public static string _eclIdColumn = "WholesaleEclId";

        public static string GetImpairmentAssumptionsData()
        {
            DataTable datable = RunQuery(_impairmentAssumptionsQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdContractDataData()
        {
            DataTable datable = RunQuery(_tempLgdContractDataQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdCollateralProjectionDownturnData()
        {
            DataTable datable = RunQuery(_tempLgdCollateralProjectionDownturnQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdCollateralProjectionOptimisticData()
        {
            DataTable datable = RunQuery(_tempLgdCollateralProjectionOptimisticQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdCollateralProjectionBestData()
        {
            DataTable datable = RunQuery(_tempLgdCollateralProjectionBestQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdCollateralTypeFSVData()
        {
            DataTable datable = RunQuery(_tempLgdCollateralTypeFsvQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdCollateralTypeOMVData()
        {
            DataTable datable = RunQuery(_tempLgdCollateralTypeOmvQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempEadInputsData()
        {
            DataTable datable = RunQuery(_tempEadInputQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempEirProjectionsData()
        {
            DataTable datable = RunQuery(_tempEirProjectionsQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempCirProjectionData()
        {
            DataTable datable = RunQuery(_tempCirProjectionQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static double GetIndexWeightW1()
        {
            return _indexWeightW1;
        }
        public static double GetIndexWeight2()
        {
            return _indexWeightW2;
        }
        public static double GetIndexWeightStandardDeviation()
        {
            return _statisticsStandardDeviation;
        }
        public static double GetIndexWeightAverage()
        {
            return _statisticsAverage;
        }
        public static string GetLoanbookData()
        {
            DataTable datable = RunQuery(_loanBookQuery);
            DataTable loanbook = datable.AsEnumerable()
                                    .Select(row => {
                                        row[LoanBookColumns.ContractID] = GenerateContractId(row);
                                        return row;
                                    }).CopyToDataTable();

            return JsonConvert.SerializeObject(loanbook);
        }
        public static string Get12MonthsPDData()
        {
            DataTable datable = RunQuery(_12MonthsPdQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetEtiNplData()
        {
            DataTable datable = RunQuery(_etiNplQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GethistoricIndexData()
        {
            DataTable datable = RunQuery(_historicIndexQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetMacroEcoBestData()
        {
            DataTable datable = RunQuery(_macroEcoBestQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetMacroEcoOptimisticData()
        {
            DataTable datable = RunQuery(_macroEcoOptimisticQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetMacroEcoDownturnData()
        {
            DataTable datable = RunQuery(_macroEcoDownturnQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetnonInternalModelInputData()
        {
            DataTable datable = RunQuery(_nonInternalmodelInputQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetSnPCummulativeDefaultData()
        {
            DataTable datable = RunQuery(_snpCummulativeDefaultRateQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetStatisticalInputsData()
        {
            DataTable datable = RunQuery(_statisticalInputsQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetPdInputAssumptionsData()
        {
            DataTable datable = RunQuery(_pdInputAssumptionsQuery, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        public static string GetTempLgdInputAssumptionsData()
        {
            DataTable datable = RunQuery(_tempLgdInputAssumptions, _dumpDatabaseName);

            return JsonConvert.SerializeObject(datable);
        }
        private static DataTable RunQuery(string query, string db = _databaseName)
        {
            try
            {
                //Logger.Debug("DBConnDetails: Server = " + serverName + " dbname = " + dbName + " username = " + username + " password = " + password);
                // Connect to client database
                SqlConnection connection = new SqlConnection("Data Source=" + _serverName + ";Initial Catalog=" + db + ";User ID=" + _username + ";Password=" + _password);
                connection.Open();

                //Get data
                SqlCommand command;
                SqlDataReader dataReader;
                command = new SqlCommand(query, connection);
                dataReader = command.ExecuteReader();


                if (!dataReader.HasRows)
                {
                    throw new Exception("No new data found!");
                }

                //Loop through data to datatable
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);

                connection.Close();
                //return JsonConvert.SerializeObject(dataTable);
                return dataTable;
            }

            catch (Exception e)
            {
                //Task.Factory.StartNew(() => { Logger.Info("Connection not created: " + e.Message); });
                throw new Exception(e.Message);
            }

        }
        private static string GenerateContractId(DataRow loanbookRow)
        {
            if (loanbookRow.Field<DateTime?>(LoanBookColumns.ContractStartDate) == null &&
               loanbookRow.Field<DateTime?>(LoanBookColumns.ContractEndDate) == null &&
               loanbookRow.Field<double>(LoanBookColumns.OriginalBalanceLCY) == 0 &&
               loanbookRow.Field<double>(LoanBookColumns.CreditLimit) == 0)
            {
                var contractId = "EXP " + loanbookRow[LoanBookColumns.ProductType].ToString() + "|";
                if (Convert.ToDouble(loanbookRow[LoanBookColumns.DebentureOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.CashOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.InventoryOMV]) +
                    Convert.ToDouble(loanbookRow[LoanBookColumns.PlantEquipmentOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.ResidentialPropertyOMV]) +
                    Convert.ToDouble(loanbookRow[LoanBookColumns.CommercialPropertyOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.ReceivablesOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.SharesOMV]) +
                    Convert.ToDouble(loanbookRow[LoanBookColumns.VehicleOMV]) + Convert.ToDouble(loanbookRow[LoanBookColumns.GuaranteeIndicator])
                    == 0)
                {
                    contractId += loanbookRow[LoanBookColumns.Segment].ToString();
                }
                else
                {
                    contractId += loanbookRow[LoanBookColumns.ContractNo].ToString();
                }

                return contractId;
            }
            else
            {
                return loanbookRow[LoanBookColumns.ContractNo].ToString();
            }
        }

        public static string InsertDataTable(string TableName, DataTable dt)
        {
            try
            {
                string connString = "Data Source=" + _serverName + ";Initial Catalog=" + _dumpDatabaseName + ";User ID=" + _username + ";Password=" + _password;

                TruncateTable(TableName, connString);

                DataColumn eclId = new DataColumn(_eclIdColumn, typeof(Guid));
                eclId.DefaultValue = _fakeEclId;
                dt.Columns.Add(eclId);

                //DataColumn id = new DataColumn("Id", typeof(Guid));
                //id.DefaultValue = Guid.NewGuid();
                //dt.Columns.Add(id);

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = TableName;

                    foreach (DataColumn column in dt.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                    }

                    bulkCopy.WriteToServer(dt);
                }

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static void TruncateTable(string TableName, string connString)
        {
            string qry = "Truncate table " + TableName;

            SqlConnection _conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = qry,
                Connection = _conn
            };
            _conn.Open();
            cmd.ExecuteNonQuery();
            _conn.Close();
        }

    }
}
