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
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
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
    public class ImportPaymentScheduleFromExcelJob : BackgroundJob<ImportEclDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IPaymentScheduleExcelDataReader _paymentScheduleExcelDataReader;
        private readonly IInvalidPaymentScheduleExporter _invalidPaymentScheduleExporter;
        private readonly IRepository<RetailEclDataPaymentSchedule, Guid> _retailEclDataPaymentScheduleRepository;
        private readonly IRepository<WholesaleEclDataPaymentSchedule, Guid> _wholesaleEclDataPaymentScheduleRepository;
        private readonly IRepository<ObeEclDataPaymentSchedule, Guid> _obeEclDataPaymentScheduleRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadSummaryRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadSummaryRepository;
        private readonly IRepository<ObeEclUpload, Guid> _obeUploadSummaryRepository;
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
        private readonly IEclCustomRepository _customRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public ImportPaymentScheduleFromExcelJob (
            IPaymentScheduleExcelDataReader paymentScheduleExcelDataReader, 
            IInvalidPaymentScheduleExporter invalidPaymentScheduleExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<RetailEclDataPaymentSchedule, Guid> retailEclDataPaymentScheduleRepository,
            IRepository<WholesaleEclDataPaymentSchedule, Guid> wholesaleEclDataPaymentScheduleRepository,
            IRepository<ObeEclDataPaymentSchedule, Guid> obeEclDataPaymentScheduleRepository,
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository,
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository,
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository,
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
            IObjectMapper objectMapper)
        {
            _paymentScheduleExcelDataReader = paymentScheduleExcelDataReader;
            _invalidPaymentScheduleExporter = invalidPaymentScheduleExporter;
            _retailEclDataPaymentScheduleRepository = retailEclDataPaymentScheduleRepository;
            _wholesaleEclDataPaymentScheduleRepository = wholesaleEclDataPaymentScheduleRepository;
            _obeEclDataPaymentScheduleRepository = obeEclDataPaymentScheduleRepository;
            _retailUploadSummaryRepository = retailUploadSummaryRepository;
            _wholesaleUploadSummaryRepository = wholesaleUploadSummaryRepository;
            _obeUploadSummaryRepository = obeUploadSummaryRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _wholesaleEclRepository = wholesaleEclRepository;
            _customRepository = customRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            //UpdateSummaryTableToFileUploaded(args);

            var paymentSchedules = GetPaymentScheduleListFromExcelOrNull(args);
            if (paymentSchedules == null || !paymentSchedules.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            //CreatePaymentSchedule(args, paymentSchedules);
            //UpdateSummaryTableToCompletedAsync(args);

            var jobs = paymentSchedules.Count / 5000;
            jobs += 1;
            UpdateSummaryTableToCompletedAsync(args, jobs);

            for (int i = 0; i < jobs; i++)
            {
                var sub_ = paymentSchedules.Skip(i * 5000).Take(5000).ToList();
                _backgroundJobManager.Enqueue<SavePaymentScheduleFromExcelJob, SaveEclPaymentScheduleDataFromExcelJobArgs>(new SaveEclPaymentScheduleDataFromExcelJobArgs
                {
                    Args = args,
                    PaymentSchedules = sub_
                });
            }

            _backgroundJobManager.Enqueue<TrackPaymentScheduleUploadJob, ImportEclDataFromExcelJobArgs>(args, delay: TimeSpan.FromSeconds(30));

        }

        private List<ImportPaymentScheduleAsStringDto> GetPaymentScheduleListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _paymentScheduleExcelDataReader.GetImportPaymentScheduleFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreatePaymentSchedule(ImportEclDataFromExcelJobArgs args, List<ImportPaymentScheduleDto> paymentSchedules)
        {
            var invalidPaymentSchedule = new List<ImportPaymentScheduleDto>();

            foreach (var paymentSchedule in paymentSchedules)
            {
                if (paymentSchedule.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreatePaymentScheduleAsync(paymentSchedule, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        paymentSchedule.Exception = exception.Message;
                        invalidPaymentSchedule.Add(paymentSchedule);
                    }
                    catch(Exception exception)
                    {
                        paymentSchedule.Exception = exception.ToString();
                        invalidPaymentSchedule.Add(paymentSchedule);
                    }
                }
                else
                {
                    invalidPaymentSchedule.Add(paymentSchedule);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportPaymentScheduleResultAsync(args, invalidPaymentSchedule));
        }

        private async Task CreatePaymentScheduleAsync(ImportPaymentScheduleDto input, ImportEclDataFromExcelJobArgs args)
        {
            switch(args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    await _retailEclDataPaymentScheduleRepository.InsertAsync(new RetailEclDataPaymentSchedule()
                            {
                                ContractRefNo = input.ContractRefNo,
                                Amount = input.Amount,
                                Component = input.Component,
                                Frequency = input.Frequency,
                                NoOfSchedules = input.NoOfSchedules,
                                RetailEclUploadId = retailSummary.RetailEclId,
                                StartDate = input.StartDate
                            });
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    await _wholesaleEclDataPaymentScheduleRepository.InsertAsync(new WholesaleEclDataPaymentSchedule()
                        {
                            ContractRefNo = input.ContractRefNo,
                            Amount = input.Amount,
                            Component = input.Component,
                            Frequency = input.Frequency,
                            NoOfSchedules = input.NoOfSchedules,
                            WholesaleEclUploadId = wholesaleSummary.WholesaleEclId,
                            StartDate = input.StartDate
                        });
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    await _obeEclDataPaymentScheduleRepository.InsertAsync(new ObeEclDataPaymentSchedule()
                    {
                        ContractRefNo = input.ContractRefNo,
                        Amount = input.Amount,
                        Component = input.Component,
                        Frequency = input.Frequency,
                        NoOfSchedules = input.NoOfSchedules,
                        ObeEclUploadId = obeSummary.ObeEclId,
                        StartDate = input.StartDate
                    });
                    break;
            }
        }

        private async Task ProcessImportPaymentScheduleResultAsync(ImportEclDataFromExcelJobArgs args, List<ImportPaymentScheduleDto> invalidPaymentSchedule)
        {
            if (invalidPaymentSchedule.Any())
            {
                var file = _invalidPaymentScheduleExporter.ExportToFile(invalidPaymentSchedule);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllPaymentScheduleSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToPaymentScheduleList"),
                Abp.Notifications.NotificationSeverity.Warn));
        }

        [UnitOfWork]
        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args, int allJobs)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.AllJobs = allJobs;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if(wholesaleSummary != null)
                    {
                        wholesaleSummary.AllJobs = allJobs;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.AllJobs = allJobs;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void UpdateSummaryTableToFileUploaded(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.FileUploaded = true;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.FileUploaded = true;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.FileUploaded = true;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var rp = _retailEclDataPaymentScheduleRepository.Count(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    if (retailSummary != null && rp > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclPaymentScheduleRetail, DbHelperConst.COL_RetailEclUploadId, retailSummary.RetailEclId.ToString()));

                        //_retailEclDataPaymentScheduleRepository.HardDelete(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wp = _wholesaleEclDataPaymentScheduleRepository.Count(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    if (wholesaleSummary != null && wp > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclPaymentScheduleWholesale, DbHelperConst.COL_WholesaleEclUploadId, wholesaleSummary.WholesaleEclId.ToString()));
                        //
                        //_wholesaleEclDataPaymentScheduleRepository.HardDelete(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var op = _obeEclDataPaymentScheduleRepository.Count(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    if (obeSummary != null && op > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclPaymentScheduleObe, DbHelperConst.COL_ObeEclUploadId, obeSummary.ObeEclId.ToString()));

                        //_obeEclDataPaymentScheduleRepository.HardDelete(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    }
                    break;
            }
            //CurrentUnitOfWork.SaveChanges();
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
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " Payment schedule";
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
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " Payment schedule";
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
