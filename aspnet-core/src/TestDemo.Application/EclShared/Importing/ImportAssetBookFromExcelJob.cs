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
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Investment;
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
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<InvestmentEcl, Guid> _eclRepository;

        public ImportAssetBookFromExcelJob(
            IAssetBookExcelDataReader assetBookExcelDataReader,
            IInvalidAssetBookExporter invalidAssetBookExporter,
            IRepository<InvestmentAssetBook, Guid> investmentAssetBookRepository,
            IRepository<InvestmentEclUpload, Guid> investmentEclUploadRepository,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<InvestmentEcl, Guid> eclRepository,
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
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _eclRepository = eclRepository;
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
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllAssetBookSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);

                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToAssetBookList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        [UnitOfWork]
        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            var investmentSummary = _investmentEclUploadRepository.FirstOrDefault((Guid)args.UploadSummaryId);
            investmentSummary.Status = GeneralStatusEnum.Completed;
            _investmentEclUploadRepository.Update(investmentSummary);
            //CurrentUnitOfWork.SaveChanges();
        }

        private void SendEmailAlert(ImportEclDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var summary = _investmentEclUploadRepository.FirstOrDefault((Guid)args.UploadSummaryId);
            var ecl = _eclRepository.FirstOrDefault((Guid)summary.InvestmentEclId);

            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var frameworkId = (int)args.Framework;
            string link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + ecl.Id;

            var ou = _ouRepository.FirstOrDefault(ecl.OrganizationUnitId);
            var type = args.Framework.ToString() + " Assetbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportEclDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var summary = _investmentEclUploadRepository.FirstOrDefault((Guid)args.UploadSummaryId);
            var ecl = _eclRepository.FirstOrDefault((Guid)summary.InvestmentEclId);

            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var ou = _ouRepository.FirstOrDefault(ecl.OrganizationUnitId);
            var type = args.Framework.ToString() + " Assetbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
