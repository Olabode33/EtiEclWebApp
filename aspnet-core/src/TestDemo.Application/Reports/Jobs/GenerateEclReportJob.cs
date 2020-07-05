using Abp.BackgroundJobs;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using TestDemo.EclShared;
using TestDemo.Storage;
using TestDemo.Notifications;
using Abp.Configuration;
using OfficeOpenXml;
using TestDemo.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Threading;
using Abp.Domain.Uow;
using TestDemo.EclShared.Emailer;
using Abp.Domain.Repositories;
using TestDemo.Authorization.Users;
using Abp.Organizations;
using TestDemo.Wholesale;
using TestDemo.Investment;
using TestDemo.OBE;
using TestDemo.Retail;
using System.IO;

namespace TestDemo.Reports.Jobs
{
    public class GenerateEclReportJob : BackgroundJob<GenerateReportJobArgs>, ITransientDependency
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
        private readonly IExcelReportGenerator _reportGenerator;

        public GenerateEclReportJob(
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
            IExcelReportGenerator reportGenerator,
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
            _reportGenerator = reportGenerator;
        }

        [UnitOfWork]
        public override void Execute(GenerateReportJobArgs args)
        {
            
            var excelPackage = _reportGenerator.GenerateExcelReport(args);

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Template\", "ETI_template.xlsx");
            var fi = new FileInfo(path);
            Logger.Debug("ReportTemplateFileLocation: " + fi);

            var binaryObjectId = SaveAsBinary(excelPackage);

            SendEmailAlert(args, binaryObjectId);
        }


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

        private void SendEmailAlert(GenerateReportJobArgs args, Guid binaryObjectId)
        {
            var user = _userRepository.FirstOrDefault(args.userIdentifier.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var type = args.eclType.ToString() + " ECL";
            long ouId = 0;
            string fileName = "";

            switch (args.eclType)
            {
                case EclType.Retail:
                    var retailEcl = _retailEclRepository.FirstOrDefault(args.eclId);
                    fileName = args.eclType.ToString() + "-ECL-Report-" + retailEcl.ReportingDate.ToString("dd-MMM-yyyy") + ".xlsx";
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case EclType.Wholesale:
                    var wEcl = _wholesaleRepository.FirstOrDefault(args.eclId);
                    fileName = args.eclType.ToString() + "-ECL-Report-" + wEcl.ReportingDate.ToString("dd-MMM-yyyy") + ".xlsx";
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case EclType.Obe:
                    var oEcl = _obeEclRepository.FirstOrDefault(args.eclId);
                    fileName = args.eclType.ToString() + "-ECL-Report-" + oEcl.ReportingDate.ToString("dd-MMM-yyyy") + ".xlsx";
                    ouId = oEcl.OrganizationUnitId;
                    break;

                case EclType.Investment:
                    var iEcl = _investmentEclRepository.FirstOrDefault(args.eclId);
                    fileName = args.eclType.ToString() + "-ECL-Report-" + iEcl.ReportingDate.ToString("dd-MMM-yyyy") + ".xlsx";
                    ouId = iEcl.OrganizationUnitId;
                    break;
            }

            var link = baseUrl + "file/DownloadBinaryFile?contentType=" + MimeTypeNames.ApplicationVndOpenxmlformatsOfficedocumentSpreadsheetmlSheet + "&id=" + binaryObjectId + "&fileName=" + fileName;


            var ou = _ouRepository.FirstOrDefault(ouId);
            _emailer.SendEmailReportGeneratedAsync(user, type, ou.DisplayName, link);
        }
    }
}
