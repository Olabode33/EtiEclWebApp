using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
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
using TestDemo.InvestmentInputs;
using TestDemo.Notifications;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class ImportAssetBookFromExcelJob : BackgroundJob<ImportEclDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IAssetBookExcelDataReader _assetBookExcelDataReader;
        private readonly IInvalidAssetBookExporter _invalidassetBookExporter;
        private readonly IRepository<InvestmentAssetBook, Guid> _investmentAssetbookRepository;
        private readonly IRepository<InvestmentEclUpload, Guid> _investmentEclUploadRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;

        public ImportAssetBookFromExcelJob(
            IAssetBookExcelDataReader assetBookExcelDataReader,
            IInvalidAssetBookExporter invalidAssetBookExporter,
            IRepository<InvestmentAssetBook, Guid> investmentAssetBookRepository,
            IRepository<InvestmentEclUpload, Guid> investmentEclUploadRepository,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IObjectMapper objectMapper)
        {
            _assetBookExcelDataReader = assetBookExcelDataReader;
            _invalidassetBookExporter = invalidAssetBookExporter;
            _investmentAssetbookRepository = investmentAssetBookRepository;
            _investmentEclUploadRepository = investmentEclUploadRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _objectMapper = objectMapper;
        }

        [UnitOfWork]
        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            var assetBooks = GetAssetListFromExcelOrNull(args);
            if (assetBooks == null || !assetBooks.Any())
            {
                SendInvalidExcelNotification(args);
            }
            CreateAssetbook(args, assetBooks);
            UpdateSummaryTableToCompletedAsync(args);
        }

        private List<ImportAssetBookDto> GetAssetListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _assetBookExcelDataReader.GetImportAssetBookFromExcel(file.Bytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void CreateAssetbook(ImportEclDataFromExcelJobArgs args, List<ImportAssetBookDto> assetBooks)
        {
            var invalidAssetBook = new List<ImportAssetBookDto>();

            foreach (var assetBook in assetBooks)
            {
                if (assetBook.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateAssetBookAsync(assetBook, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        assetBook.Exception = exception.Message;
                        invalidAssetBook.Add(assetBook);
                    }
                    catch (Exception exception)
                    {
                        assetBook.Exception = exception.ToString();
                        invalidAssetBook.Add(assetBook);
                    }
                }
                else
                {
                    invalidAssetBook.Add(assetBook);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportLoanbookResultAsync(args, invalidAssetBook));
        }

        private async Task CreateAssetBookAsync(ImportAssetBookDto input, ImportEclDataFromExcelJobArgs args)
        {
            await _investmentAssetbookRepository.InsertAsync(new InvestmentAssetBook()
            {
                AssetDescription = input.AssetDescription,
                AssetType = input.AssetType,
                CounterParty = input.CounterParty,
                SovereignDebt = input.SovereignDebt,
                RatingAgency = input.RatingAgency,
                CreditRatingAtPurchaseDate = input.CreditRatingAtPurchaseDate,
                CurrentCreditRating = input.CurrentCreditRating,
                NominalAmount = input.NominalAmount,
                PrincipalAmortisation = input.PrincipalAmortisation,
                RepaymentTerms = input.RepaymentTerms,
                CarryAmountNGAAP = input.CarryAmountNGAAP,
                CarryingAmountIFRS = input.CarryingAmountIFRS,
                Coupon = input.Coupon,
                Eir = input.Eir,
                PurchaseDate = input.PurchaseDate,
                IssueDate = input.IssueDate,
                PurchasePrice = input.PurchasePrice,
                MaturityDate = input.MaturityDate,
                RedemptionPrice = input.RedemptionPrice,
                BusinessModelClassification = input.BusinessModelClassification,
                Ias39Impairment = input.Ias39Impairment,
                PrudentialClassification = input.PrudentialClassification,
                ForebearanceFlag = input.ForebearanceFlag,
                InvestmentEclUploadId = args.UploadSummaryId
            });
        }

        private async Task ProcessImportLoanbookResultAsync(ImportEclDataFromExcelJobArgs args, List<ImportAssetBookDto> invalidAssetbook)
        {
            if (invalidAssetbook.Any())
            {
                var file = _invalidassetBookExporter.ExportToFile(invalidAssetbook);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllAssetBookSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToAssetBookList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            var investmentSummary = _investmentEclUploadRepository.FirstOrDefault((Guid)args.UploadSummaryId);
            investmentSummary.Status = GeneralStatusEnum.Completed;
            _investmentEclUploadRepository.Update(investmentSummary);
        }

    }
}
