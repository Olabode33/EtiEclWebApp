using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclShared;
using TestDemo.EclShared.Emailer;
using TestDemo.Investment;
using TestDemo.Notifications;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Storage;
using TestDemo.Wholesale;

namespace TestDemo.Reports
{
    public class ExcelReportGenerator : ITransientDependency, IExcelReportGenerator
    {
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IAppNotifier _appNotifier;
        private readonly IEclEngineEmailer _emailer;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleRepository;

        public ExcelReportGenerator(
            IHostingEnvironment env,
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager,
            IEclEngineEmailer emailer,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<WholesaleEcl, Guid> wholesaleRepository,
            IRepository<InvestmentEcl, Guid> investmentEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IAppNotifier appNotifier)
        {
            _appConfiguration = env.GetAppConfiguration();
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
            _appNotifier = appNotifier;
            _emailer = emailer;
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _wholesaleRepository = wholesaleRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentEclRepository = investmentEclRepository;
        }


        public FileDto DownloadExcelReport(GenerateReportJobArgs args)
        {
            var reportProperties = GetReportProperties(args); 
            var excelPackage = GenerateExcelReport(args);
            var file = Save(excelPackage, reportProperties.FileName() + ".xlsx");
            return file;
        }

        public ExcelPackage GenerateExcelReport(GenerateReportJobArgs args)
        {
            ResultDetail rd = new ResultDetail();
            var reportProps = GetReportProperties(args);

            if (args.eclType == EclType.Investment)
                rd = GetInvestmentResultDetail(args.eclType, args.eclId);
            else
                rd = GetResultDetail(args.eclType, args.eclId);

            var rc = new ReportComputation();
            var excelPackage = rc.GenerateEclReport(args.eclType, args.eclId, rd, reportProps);

            return excelPackage;
        }

        #region DataAccess
        private string sqlConnection
        {
            get
            {
                return _appConfiguration["ConnectionStrings:Default"];
            }
        }
        public int ExecuteQuery(string qry)
        {
            try
            {

                var con = new SqlConnection(sqlConnection);
                var com = new SqlCommand(qry, con);

                con.Open();
                var i = com.ExecuteNonQuery();
                con.Close();
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public int ExecuteBulkCopy(DataTable dt, string tablename)
        {

            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                // make sure to enable triggers
                // more on triggers in next post
                SqlBulkCopy bulkCopy = new SqlBulkCopy(
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                // set the destination table name
                bulkCopy.DestinationTableName = tablename;
                bulkCopy.BatchSize = 10000;

                foreach (DataColumn dc in dt.Columns)
                {
                    bulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }

                connection.Open();

                // write the data in the "dataTable"
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
            // reset
            dt.Clear();

            return 1;
        }

        public int getCount(string qry)
        {
            try
            {

                var con = new SqlConnection(sqlConnection);
                var com = new SqlCommand(qry, con);

                con.Open();
                var i = com.ExecuteScalar();
                con.Close();
                try
                {
                    return int.Parse(i.ToString());
                }
                catch { return 0; }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public DataTable GetData(string qry)
        {
            var dt = new DataTable();
            try
            {

                var con = new SqlConnection(sqlConnection);
                var da = new SqlDataAdapter(qry, con);

                con.Open();
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                //Logger.Debug("GenerateEclReportJob.GetData()-Error: " + ex.Message);
            }
            return dt;
        }

        public T ParseDataToObject<T>(T t, DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
        #endregion DataAccess

        #region GetData
        private ResultDetail GetResultDetail(EclType eclType, Guid eclId)
        {
            var rd = new ResultDetail();
            var _eclId = eclId.ToString();
            var _eclType = eclType.ToString();
            var _eclTypeTable = eclType.ToString();

            var qry = $"select [Status] from {_eclType}Ecls where Id='{_eclId}'";
            var dt = GetData(qry);

            if (dt.Rows.Count > 0)
            {
                var eclStatus = int.Parse(dt.Rows[0][0].ToString());
                if (eclStatus == 10)
                {
                    _eclTypeTable = $"IFRS9_DB_Archive.dbo.{_eclTypeTable}";
                }
            }

            //qry = $"select " +
            //    $" NumberOfContracts=0, " +
            //    $"  SumOutStandingBalance=0," +
            //    $"   Pre_EclBestEstimate=0," +
            //    $"   Pre_Optimistic=0," +
            //    $"   Pre_Downturn=0," +

            //    $"   Post_EclBestEstimate=0," +
            //    $"   Post_Optimistic=0," +
            //    $"   Post_Downturn=0," +

            //    $"   try_convert(float, isnull((select Value UserInputValue from {_eclType}EclAssumptions where {_eclType}EclId = '{_eclId}' and [Key] = 'BestEstimateScenarioLikelihood'), 0)) UserInput_EclBE," +
            //    $"   try_convert(float, isnull((select Value UserInputValue from {_eclType}EclAssumptions where {_eclType}EclId = '{_eclId}' and [Key] = 'OptimisticScenarioLikelihood'), 0)) UserInput_EclO," +
            //    $"   try_convert(float, isnull((select Value UserInputValue from {_eclType}EclAssumptions where {_eclType}EclId = '{_eclId}' and [Key] = 'DownturnScenarioLikelihood'), 0)) UserInput_EclD";

            //dt = GetData(qry);

            //var rde = new ReportDetailExtractor();
            //var temp_header = ParseDataToObject(rde, dt.Rows[0]);

            //var overrides_overlay = 0;

            //qry = $"select f.Stage, f.FinalEclValue, f.Scenario, f.ContractId, fo.Stage StageOverride, fo.FinalEclValue FinalEclValueOverride, fo.Scenario ScenarioOverride, fo.ContractId ContractIOverride from {_eclTypeTable}ECLFrameworkFinal f left join {_eclTypeTable}ECLFrameworkFinalOverride fo on (f.contractId=fo.contractId and f.EclMonth=fo.EclMonth and f.Scenario=fo.Scenario) where f.{_eclType}EclId = '{_eclId}' and f.EclMonth=1";
            //dt = GetData(qry);

            //var lstTfer = new List<TempFinalEclResult>();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    var tfer = new TempFinalEclResult();
            //    lstTfer.Add(ParseDataToObject(tfer, dr));
            //}


            //qry = $"select ContractId, [Value] from {_eclTypeTable}EadInputs where {_eclType}EclId='{_eclId}' and Months=1";
            //dt = GetData(qry);

            //var lstTWEI = new List<TempEadInput>();

            //foreach (DataRow dr in dt.Rows)
            //{
            //    var twei = new TempEadInput();
            //    lstTWEI.Add(ParseDataToObject(twei, dr));
            //}

            rd.ResultDetailDataMore = new List<ResultDetailDataMore>();

            //qry = $"select distinct ContractNo, AccountNo, CustomerNo, Segment, ProductType, Sector from {_eclTypeTable}EclDataLoanBooks where {_eclType}EclUploadId='{_eclId}'";
            //dt = GetData(qry);

            qry = $"Select ContractNo, AccountNo,CustomerNo, Segment, ProductType, Sector, " +
                  $"r.Stage, Outstanding_Balance, ECL_Best_Estimate, ECL_Optimistic, ECL_Downturn, Impairment_ModelOutput, " +
                  $"Overrides_Stage, Overrides_ECL_Best_Estimate, Overrides_ECL_Optimistic, Overrides_ECL_Downturn,Overrides_Impairment_Manual, " +
                  $"o.Reason, o.OverrideType " +
                  $"from {_eclTypeTable}EclFramworkReportDetail r " +
                  $"left join {_eclTypeTable}EclOverrides o on r.ContractNo = o.ContractId and r.{_eclType}EclId = o.{_eclType}EclDataLoanBookId " +
                  $"where {_eclType}EclId = '{_eclId}'";

            dt = GetData(qry);
            foreach (DataRow dr in dt.Rows)
            {
                var rddm = new ResultDetailDataMore();
                var itm = ParseDataToObject(rddm, dr);
                rd.ResultDetailDataMore.Add(itm);
            }


            //foreach (DataRow dr in dt.Rows)
            //{
            //    var rdd = new ResultDetailData();
            //    var itm = ParseDataToObject(rdd, dr);

            //    var _lstTfer = lstTfer.Where(o => o.ContractId == itm.ContractNo).ToList();

            //    var stage = 1;
            //    try { stage = _lstTfer.FirstOrDefault(o => o.Scenario == 1).Stage; } catch { }

            //    var BE_Value = 0.0;
            //    try { BE_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 1).FinalEclValue; } catch { }

            //    var O_Value = 0.0;
            //    try { O_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 2).FinalEclValue; } catch { }

            //    var D_Value = 0.0;
            //    try { D_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 3).FinalEclValue; } catch { }

            //    var stage_Override = 1;
            //    try { stage_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 1).StageOverride; } catch { }

            //    var BE_Value_Override = 0.0;
            //    try { BE_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 1).FinalEclValueOverride; } catch { BE_Value_Override = BE_Value; }

            //    var O_Value_Override = 0.0;
            //    try { O_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 2).FinalEclValueOverride; } catch { O_Value_Override = O_Value; }

            //    var D_Value_Override = 0.0;
            //    try { D_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 3).FinalEclValueOverride; } catch { D_Value_Override = D_Value; }

            //    var outStandingBal = 0.0;
            //    try { outStandingBal = lstTWEI.FirstOrDefault(o => o.ContractId == itm.ContractNo).Value; } catch { }

            //    var rddm = new ResultDetailDataMore
            //    {
            //        AccountNo = itm.AccountNo,
            //        ContractNo = itm.ContractNo,
            //        CustomerNo = itm.CustomerNo,
            //        ProductType = itm.ProductType,
            //        Sector = itm.Sector,
            //        Stage = stage,
            //        Overrides_Stage = stage_Override,
            //        ECL_Best_Estimate = BE_Value,
            //        ECL_Downturn = D_Value,
            //        ECL_Optimistic = O_Value,
            //        Overrides_ECL_Best_Estimate = BE_Value_Override * (1 + overrides_overlay),
            //        Overrides_ECL_Downturn = D_Value_Override * (1 + overrides_overlay),
            //        Overrides_ECL_Optimistic = O_Value_Override * (1 + overrides_overlay),
            //        Segment = itm.Segment,
            //        Overrides_FSV = 0,
            //        Outstanding_Balance = outStandingBal,
            //        Overrides_TTR_Years = 0,
            //        Overrides_Overlay = 0,
            //        Impairment_ModelOutput = 0,
            //        Overrides_Impairment_Manual = 0
            //    };

            //    rddm.Impairment_ModelOutput = (rddm.ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rddm.ECL_Optimistic + temp_header.UserInput_EclO) + (rddm.ECL_Downturn * temp_header.UserInput_EclD);
            //    rddm.Overrides_Impairment_Manual = (rddm.Overrides_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rddm.Overrides_ECL_Optimistic + temp_header.UserInput_EclO) + (rddm.Overrides_ECL_Downturn * temp_header.UserInput_EclD);

            //    rd.ResultDetailDataMore.Add(rddm);
            //}

            rd.NumberOfContracts = rd.ResultDetailDataMore.Count();
            rd.OutStandingBalance = rd.ResultDetailDataMore.Sum(o => o.Outstanding_Balance);
            rd.Pre_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.ECL_Best_Estimate);
            rd.Pre_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.ECL_Optimistic);
            rd.Pre_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.ECL_Downturn);
            rd.Pre_Impairment_ModelOutput = rd.ResultDetailDataMore.Sum(o => o.Impairment_ModelOutput);
            //rd.Pre_Impairment_ModelOutput = (rd.Pre_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rd.Pre_ECL_Optimistic + temp_header.UserInput_EclO) + (rd.Pre_ECL_Downturn * temp_header.UserInput_EclD);

            rd.Post_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Best_Estimate);
            rd.Post_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Optimistic);
            rd.Post_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Downturn);

            rd.Post_Impairment_ModelOutput = rd.ResultDetailDataMore.Sum(o => o.Overrides_Impairment_Manual);
            //rd.Post_Impairment_ModelOutput = (rd.Pre_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rd.Pre_ECL_Optimistic + temp_header.UserInput_EclO) + (rd.Pre_ECL_Downturn * temp_header.UserInput_EclD);

            return rd;
        }


        private ResultDetail GetInvestmentResultDetail(EclType eclType, Guid eclId)
        {
            var rd = new ResultDetail();
            var _eclId = eclId.ToString();
            var _eclType = eclType.ToString();
            var _eclTypeTable = eclType.ToString();

            var qry = $"select [Status] from {_eclType}Ecls where Id='{_eclId}'";
            var dt = GetData(qry);

            if (dt.Rows.Count > 0)
            {
                var eclStatus = int.Parse(dt.Rows[0][0].ToString());
                if (eclStatus == 10)
                {
                    _eclTypeTable = $"IFRS9_DB_Archive.dbo.{_eclTypeTable}";
                }
            }

            rd.ResultDetailDataMore = new List<ResultDetailDataMore>();

            //qry = $"select distinct ContractNo, AccountNo, CustomerNo, Segment, ProductType, Sector from {_eclTypeTable}EclDataLoanBooks where {_eclType}EclUploadId='{_eclId}'";
            //dt = GetData(qry);


            qry = $"Select asset.AssetDescription ContractNo, asset.AssetDescription AccountNo, asset.AssetDescription CustomerNo, asset.AssetDescription Segment, asset.AssetType ProductType, asset.CounterParty Sector, " +
                  $" pre.Stage, pre.Exposure Outstanding_Balance, pre.BestValue ECL_Best_Estimate, pre.OptimisticValue ECL_Optimistic, pre.DownturnValue ECL_Downturn, pre.Impairment Impairment_ModelOutput, " +
                  $" post.Stage Overrides_Stage, ISNULL(post.BestValue, pre.BestValue) Overrides_ECL_Best_Estimate, isnull(post.OptimisticValue, pre.OptimisticValue) Overrides_ECL_Optimistic, isnull(post.DownturnValue, pre.DownturnValue) Overrides_ECL_Downturn, isnull(post.Impairment, pre.Impairment) Overrides_Impairment_Manual " +
                  $" ,o.OverrideComment Reason, o.OverrideType " +
                  $"from {_eclTypeTable}EclFinalResult pre " +
                  $"left join {_eclTypeTable}EclFinalPostOverrideResults post on pre.RecordId = post.RecordId " +
                  $"left join {_eclTypeTable}AssetBooks asset on pre.RecordId = asset.Id " +
                  $"left join {_eclTypeTable}EclSicr sicr on pre.RecordId = sicr.RecordId " +
                  $"left join {_eclTypeTable}EclOverrides o on sicr.id = o.InvestmentEclSicrId " +
                  $"where pre.EclId = '{_eclId}'";
            dt = GetData(qry);

            foreach (DataRow dr in dt.Rows)
            {
                var rddm = new ResultDetailDataMore();
                var itm = ParseDataToObject(rddm, dr);
                rd.ResultDetailDataMore.Add(itm);
            }

            rd.NumberOfContracts = rd.ResultDetailDataMore.Count();
            rd.OutStandingBalance = rd.ResultDetailDataMore.Sum(o => o.Outstanding_Balance);
            rd.Pre_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.ECL_Best_Estimate);
            rd.Pre_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.ECL_Optimistic);
            rd.Pre_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.ECL_Downturn);
            rd.Pre_Impairment_ModelOutput = rd.ResultDetailDataMore.Sum(o => o.Impairment_ModelOutput);

            rd.Post_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Best_Estimate);
            rd.Post_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Optimistic);
            rd.Post_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Downturn);

            rd.Post_Impairment_ModelOutput = rd.ResultDetailDataMore.Sum(o => o.Overrides_Impairment_Manual);

            return rd;
        }
        #endregion GetData

        #region ExcelSaver

        protected FileDto Save(ExcelPackage excelPackage, string fileName)
        {
            var file = new FileDto(fileName, MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet);
            _tempFileCacheManager.SetFile(file.FileToken, excelPackage.GetAsByteArray());
            return file;
        }
        protected Guid SaveAsBinary(ExcelPackage excelPackage)
        {
            var fileObject = new BinaryObject(1, excelPackage.GetAsByteArray());
            AsyncHelper.RunSync(() => _binaryObjectManager.SaveAsync(fileObject));
            return fileObject.Id;
        }
        #endregion ExcelSaver

        private ReportProperties GetReportProperties(GenerateReportJobArgs args)
        {
            var reportProperties = new ReportProperties();
            long ouId = -1;
            switch (args.eclType)
            {
                case EclType.Retail:
                    var retailEcl = _retailEclRepository.FirstOrDefault(args.eclId);
                    reportProperties.ReportDate = retailEcl.ReportingDate;
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case EclType.Wholesale:
                    var wEcl = _wholesaleRepository.FirstOrDefault(args.eclId);
                    reportProperties.ReportDate = wEcl.ReportingDate;
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case EclType.Obe:
                    var oEcl = _obeEclRepository.FirstOrDefault(args.eclId);
                    reportProperties.ReportDate = oEcl.ReportingDate;
                    ouId = oEcl.OrganizationUnitId;
                    break;

                case EclType.Investment:
                    var iEcl = _investmentEclRepository.FirstOrDefault(args.eclId);
                    reportProperties.ReportDate = iEcl.ReportingDate;
                    ouId = iEcl.OrganizationUnitId;
                    break;

                default:
                    reportProperties.ReportDate = new DateTime(1, 1, 1);
                    reportProperties.OuName = "";
                    break;
            }

            if (ouId != -1)
            {
                var ou = _ouRepository.FirstOrDefault(ouId);
                reportProperties.OuName = ou == null ? "" : ou.DisplayName;
            }

            return reportProperties;
        }
    }


}
