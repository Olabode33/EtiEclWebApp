using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.Threading;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.BatchEcls;
using TestDemo.BatchEcls.BatchEclInput;
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;
using TestDemo.OBE;
using TestDemo.ObeInputs;
using TestDemo.Retail;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class TrackLoanbookUploadJob : BackgroundJob<ImportEclDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly ILoanbookExcelDataReader _loanbookExcelDataReader;
        private readonly IInvalidLoanbookExporter _invalidLoanbookExporter;
        private readonly IRepository<RetailEclDataLoanBook, Guid> _retailEclDataLoanbookRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _wholesaleEclDataLoanbookRepository;
        private readonly IRepository<ObeEclDataLoanBook, Guid> _obeEclDataLoanbookRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadSummaryRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadSummaryRepository;
        private readonly IRepository<ObeEclUpload, Guid> _obeUploadSummaryRepository;
        private readonly IRepository<BatchEclUpload, Guid> _batchUploadSummaryRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<BatchEcl, Guid> _batchEclRepository;
        private readonly IRepository<TrackEclDataLoanBookException> _loanbookExceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IEclCustomRepository _customRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public TrackLoanbookUploadJob(
            ILoanbookExcelDataReader loanbookExcelDataReader, 
            IInvalidLoanbookExporter invalidLoanbookExporter, 
            IRepository<RetailEclDataLoanBook, Guid> retailEclDataLoanbookRepository, 
            IRepository<WholesaleEclDataLoanBook, Guid> wholesaleEclDataLoanbookRepository, 
            IRepository<ObeEclDataLoanBook, Guid> obeEclDataPaymentScheduleRepository, 
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository, 
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository, 
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository,
            IRepository<BatchEclUpload, Guid> batchUploadSummaryRepository,
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository,
            IRepository<BatchEcl, Guid> batchEclRepository,
            IRepository<TrackEclDataLoanBookException> loanbookExceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
            IObjectMapper objectMapper)
        {
            _loanbookExcelDataReader = loanbookExcelDataReader;
            _invalidLoanbookExporter = invalidLoanbookExporter;
            _retailEclDataLoanbookRepository = retailEclDataLoanbookRepository;
            _wholesaleEclDataLoanbookRepository = wholesaleEclDataLoanbookRepository;
            _obeEclDataLoanbookRepository = obeEclDataPaymentScheduleRepository;
            _retailUploadSummaryRepository = retailUploadSummaryRepository;
            _wholesaleUploadSummaryRepository = wholesaleUploadSummaryRepository;
            _obeUploadSummaryRepository = obeUploadSummaryRepository;
            _batchUploadSummaryRepository = batchUploadSummaryRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _wholesaleEclRepository = wholesaleEclRepository;
            _batchEclRepository = batchEclRepository;
            _objectMapper = objectMapper;
            _customRepository = customRepository;
            _loanbookExceptionTrackerRepository = loanbookExceptionTrackerRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            var allJobs = 0;
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        allJobs = retailSummary.AllJobs;
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        allJobs = wholesaleSummary.AllJobs;
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        allJobs = obeSummary.AllJobs;
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (bSummary != null)
                    {
                        allJobs = bSummary.AllJobs;
                    }
                    break;
            }

            var completedJobs = _uploadJobsTrackerRepository.Count(e => e.RegisterId == args.UploadSummaryId);
            if (allJobs <= completedJobs)
            {
                AsyncHelper.RunSync(() => ProcessResult(args));
            }
            else
            {
                UpdateSummaryTableToProgress(args, completedJobs);
                _backgroundJobManager.Enqueue<TrackLoanbookUploadJob, ImportEclDataFromExcelJobArgs>(args, delay: TimeSpan.FromSeconds(30));
            }

        }

        private async Task ProcessResult(ImportEclDataFromExcelJobArgs args)
        {
            Guid? eclId = null;
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        eclId = retailSummary.RetailEclId;
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        eclId = wholesaleSummary.WholesaleEclId;
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        eclId = (Guid)obeSummary.ObeEclId;
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (bSummary != null)
                    {
                        eclId = (Guid)bSummary.BatchId;
                    }
                    break;
            }
            
            if(eclId != null)
            {
                var invalids = _loanbookExceptionTrackerRepository.GetAll()
                                                                  .Where(e => e.EclId == eclId)
                                                                  .Select(e => _objectMapper.Map<ImportLoanbookDto>(e))
                                                                  .ToList();
                if (invalids.Count > 0)
                {
                    var file = _invalidLoanbookExporter.ExportToFile(invalids);
                    await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                    SendInvalidEmailAlert(args, file);
                    DeleteExistingExceptions(args, (Guid)eclId);

                    var baseUrl = _appConfiguration["App:ServerRootAddress"];
                    var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

                    UpdateSummaryTableToFailed(args, link);
                } 
                else
                {
                    UpdateSummaryTableToCompletedAsync(args);
                    SendEmailAlert(args);
                    _uploadJobsTrackerRepository.Delete(e => e.RegisterId == args.UploadSummaryId);
                }
            } 
        }

        [UnitOfWork]
        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.CompletedJobs = retailSummary.AllJobs;
                        retailSummary.Status = GeneralStatusEnum.Completed;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.CompletedJobs = wholesaleSummary.AllJobs;
                        wholesaleSummary.Status = GeneralStatusEnum.Completed;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.CompletedJobs = obeSummary.AllJobs;
                        obeSummary.Status = GeneralStatusEnum.Completed;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (bSummary != null)
                    {
                        int obeCount = 0; int retailCount = 0; int wholesCount = 0;
                        var obe = _obeEclRepository.FirstOrDefault(e => e.BatchId == bSummary.BatchId);
                        var retail = _retailEclRepository.FirstOrDefault(e => e.BatchId == bSummary.BatchId);
                        var wholes = _wholesaleEclRepository.FirstOrDefault(e => e.BatchId == bSummary.BatchId);

                        if (obe != null)
                        {
                            obeCount = _obeEclDataLoanbookRepository.Count(e => e.ObeEclUploadId == obe.Id);
                        }
                        if (retail != null)
                        {
                            retailCount = _retailEclDataLoanbookRepository.Count(e => e.RetailEclUploadId == retail.Id);
                        }
                        if (wholes != null)
                        {
                            wholesCount = _wholesaleEclDataLoanbookRepository.Count(e => e.WholesaleEclUploadId == wholes.Id);
                        }

                        bSummary.CompletedJobs = bSummary.AllJobs;
                        bSummary.Status = GeneralStatusEnum.Completed;
                        bSummary.CountWholesaleData = wholesCount;
                        bSummary.CountRetailData = retailCount;
                        bSummary.CountObeData = obeCount;
                        bSummary.CountTotalData = wholesCount + retailCount + obeCount;
                        _batchUploadSummaryRepository.Update(bSummary);
                    }
                    break;
            }
        }

        [UnitOfWork]
        private void UpdateSummaryTableToFailed(ImportEclDataFromExcelJobArgs args, string link)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.FileUploaded = false;
                        retailSummary.Status = GeneralStatusEnum.Failed;
                        retailSummary.UploadComment = _localizationSource.GetString("CompletedWithErrorsCheckEmail") + " &nbsp;<a href='" + link + "'> Download</a>";
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.FileUploaded = false;
                        wholesaleSummary.Status = GeneralStatusEnum.Failed;
                        wholesaleSummary.UploadComment = _localizationSource.GetString("CompletedWithErrorsCheckEmail") + " &nbsp;<a href='" + link + "'> Download</a>";
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.FileUploaded = false;
                        obeSummary.Status = GeneralStatusEnum.Failed;
                        obeSummary.UploadComment = _localizationSource.GetString("CompletedWithErrorsCheckEmail") + " &nbsp;<a href='" + link + "'> Download</a>";
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (bSummary != null)
                    {
                        bSummary.FileUploaded = false;
                        bSummary.Status = GeneralStatusEnum.Failed;
                        bSummary.UploadComment = _localizationSource.GetString("CompletedWithErrorsCheckEmail") + " &nbsp;<a href='" + link + "'> Download</a>";
                        _batchUploadSummaryRepository.Update(bSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }


        [UnitOfWork]
        private void UpdateSummaryTableToProgress(ImportEclDataFromExcelJobArgs args, int completedJobs)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.CompletedJobs = completedJobs;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.CompletedJobs = completedJobs;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.CompletedJobs = completedJobs;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (bSummary != null)
                    {
                        bSummary.CompletedJobs = completedJobs;
                        _batchUploadSummaryRepository.Update(bSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }


        [UnitOfWork]
        private void DeleteExistingExceptions(ImportEclDataFromExcelJobArgs args, Guid EclId)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_TrackEclDataLoanBookException, DbHelperConst.COL_EclId, EclId.ToString()));
        }

        private void SendEmailAlert(ImportEclDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var frameworkId = (int)args.Framework;
            string link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/";
            long ouId = 0;

            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var retailEcl = _retailEclRepository.FirstOrDefault(retailSummary.RetailEclId);
                    link += retailEcl.Id;
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wEcl = _wholesaleEclRepository.FirstOrDefault(wholesaleSummary.WholesaleEclId);
                    link += wEcl.Id;
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var oEcl = _obeEclRepository.FirstOrDefault((Guid)obeSummary.ObeEclId);
                    link += oEcl.Id;
                    ouId = oEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var bEcl = _batchEclRepository.FirstOrDefault((Guid)bSummary.BatchId);
                    link = baseUrl + "/app/main/ecl/view/batch/" + bEcl.Id;
                    ouId = bEcl.OrganizationUnitId;
                    break;
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " loanbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportEclDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId); 
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            long ouId = 0;

            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var retailEcl = _retailEclRepository.FirstOrDefault(retailSummary.RetailEclId);
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wEcl = _wholesaleEclRepository.FirstOrDefault(wholesaleSummary.WholesaleEclId);
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var oEcl = _obeEclRepository.FirstOrDefault((Guid)obeSummary.ObeEclId);
                    ouId = oEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Batch:
                    var bSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var bEcl = _batchEclRepository.FirstOrDefault((Guid)bSummary.BatchId);
                    link = baseUrl + "/app/main/ecl/view/batch/" + bEcl.Id;
                    ouId = bEcl.OrganizationUnitId;
                    break;
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " loanbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
