using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Threading;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Notifications;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
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
        }

        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            var paymentSchedules = GetPaymentScheduleListFromExcelOrNull(args);
            if (paymentSchedules == null || !paymentSchedules.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreatePaymentSchedule(args, paymentSchedules);
            UpdateSummaryTableToCompletedAsync(args);
        }

        private List<ImportPaymentScheduleDto> GetPaymentScheduleListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
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
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllPaymentScheduleSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToPaymentScheduleList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.Status = GeneralStatusEnum.Completed;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if(wholesaleSummary != null)
                    {
                        wholesaleSummary.Status = GeneralStatusEnum.Completed;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.Status = GeneralStatusEnum.Completed;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
        }

        private void DeleteExistingDataAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        _retailEclDataPaymentScheduleRepository.HardDelete(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        _wholesaleEclDataPaymentScheduleRepository.HardDelete(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        _obeEclDataPaymentScheduleRepository.HardDelete(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    }
                    break;
            }
        }

    }
}
