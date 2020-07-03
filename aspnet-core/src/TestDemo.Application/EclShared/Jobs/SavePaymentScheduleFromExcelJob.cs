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
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.EclShared.Importing.Utils;
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
    public class SavePaymentScheduleFromExcelJob : BackgroundJob<SaveEclPaymentScheduleDataFromExcelJobArgs>, ITransientDependency
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
        private readonly IRepository<TrackEclDataPaymentScheduleException> _exceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IValidationUtil _validator;

        public SavePaymentScheduleFromExcelJob(
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
            IRepository<TrackEclDataPaymentScheduleException> exceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IEclCustomRepository customRepository,
            IValidationUtil validator,
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
            _exceptionTrackerRepository = exceptionTrackerRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _validator = validator;
        }

        [UnitOfWork]
        public override void Execute(SaveEclPaymentScheduleDataFromExcelJobArgs args)
        {
            var records = args.PaymentSchedules;
            var validatedRecords = ValidateRecords(records);
            var EclId = GetEclId(args.Args);
            if (EclId != null)
            {
                CreatePaymentSchedule(args.Args, validatedRecords, (Guid)EclId);
            }
            else
            {
                Logger.Debug("SavePaymentScheduleFromExcelJob: Error Retrieving ECL Id, " + DateTime.Now.ToString());
            }
        }

        private List<ImportPaymentScheduleDto> ValidateRecords(List<ImportPaymentScheduleAsStringDto> inputs)
        {
            var records = new List<ImportPaymentScheduleDto>();
            //var loanbookArray = loanbooks.ToArray();

            foreach (var item in inputs)
            {
                var exceptionMessage = new StringBuilder();
                var record = new ImportPaymentScheduleDto();

                try
                {
                    record.Amount = _validator.ValidateDoubleValueFromRowOrNull(item.Amount, nameof(record.Amount), exceptionMessage);
                    record.Component = item.Component;
                    record.ContractRefNo = item.ContractRefNo;
                    record.Frequency = item.Frequency;
                    record.NoOfSchedules = _validator.ValidateIntegerValueFromRowOrNull(item.NoOfSchedules, nameof(record.NoOfSchedules), exceptionMessage);
                    record.StartDate = _validator.ValidateDateTimeValueFromRowOrNull(item.StartDate, nameof(record.StartDate), exceptionMessage);

                    if (exceptionMessage.Length > 0)
                    {
                        record.Exception = exceptionMessage.ToString();
                    }


                }
                catch (Exception e)
                {
                    record.Exception = e.Message;
                }

                records.Add(record);
            }

            return records;
        }

        private Guid? GetEclId(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return retailSummary == null ? (Guid?)null : retailSummary.RetailEclId;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return wholesaleSummary == null ? (Guid?)null : wholesaleSummary.WholesaleEclId;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return obeSummary == null ? (Guid?)null : (Guid)obeSummary.ObeEclId;
                default:
                    return null;
            }
        }


        private void CreatePaymentSchedule(ImportEclDataFromExcelJobArgs args, List<ImportPaymentScheduleDto> paymentSchedules, Guid eclId)
        {
            var invalidPaymentSchedule = new List<ImportPaymentScheduleDto>();

            foreach (var paymentSchedule in paymentSchedules)
            {
                if (paymentSchedule.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreatePaymentScheduleAsync(paymentSchedule, args, eclId));
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

            AsyncHelper.RunSync(() => ProcessImportPaymentScheduleResultAsync(args, invalidPaymentSchedule, eclId));
        }

        private async Task CreatePaymentScheduleAsync(ImportPaymentScheduleDto input, ImportEclDataFromExcelJobArgs args, Guid eclId)
        {
            switch(args.Framework)
            {
                case FrameworkEnum.Retail:
                    await _retailEclDataPaymentScheduleRepository.InsertAsync(new RetailEclDataPaymentSchedule()
                            {
                                ContractRefNo = input.ContractRefNo,
                                Amount = input.Amount,
                                Component = input.Component,
                                Frequency = input.Frequency,
                                NoOfSchedules = input.NoOfSchedules,
                                RetailEclUploadId = eclId,
                                StartDate = input.StartDate
                            });
                    break;

                case FrameworkEnum.Wholesale:
                    await _wholesaleEclDataPaymentScheduleRepository.InsertAsync(new WholesaleEclDataPaymentSchedule()
                        {
                            ContractRefNo = input.ContractRefNo,
                            Amount = input.Amount,
                            Component = input.Component,
                            Frequency = input.Frequency,
                            NoOfSchedules = input.NoOfSchedules,
                            WholesaleEclUploadId = eclId,
                            StartDate = input.StartDate
                        });
                    break;

                case FrameworkEnum.OBE:
                    await _obeEclDataPaymentScheduleRepository.InsertAsync(new ObeEclDataPaymentSchedule()
                    {
                        ContractRefNo = input.ContractRefNo,
                        Amount = input.Amount,
                        Component = input.Component,
                        Frequency = input.Frequency,
                        NoOfSchedules = input.NoOfSchedules,
                        ObeEclUploadId = eclId,
                        StartDate = input.StartDate
                    });
                    break;
            }
        }

        private async Task ProcessImportPaymentScheduleResultAsync(ImportEclDataFromExcelJobArgs args, List<ImportPaymentScheduleDto> invalidPaymentSchedule, Guid eclId)
        {
            if (invalidPaymentSchedule.Any())
            {
                foreach (var item in invalidPaymentSchedule)
                {
                    await _exceptionTrackerRepository.InsertAsync(new TrackEclDataPaymentScheduleException
                    {
                        Amount = item.Amount,
                        Component = item.Component,
                        ContractRefNo = item.ContractRefNo,
                        EclId = eclId,
                        Exception = item.Exception,
                        Frequency = item.Frequency,
                        NoOfSchedules = item.NoOfSchedules,
                        StartDate = item.StartDate
                    });
                }
            }
            _uploadJobsTrackerRepository.Insert(new TrackRunningUploadJobs
            {
                RegisterId = args.UploadSummaryId
            });
        }

    }
}
