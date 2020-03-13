using TestDemo.InvestmentInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Dto.Inputs;

namespace TestDemo.InvestmentInputs
{
    [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks)]
    public class InvestmentAssetBooksAppService : TestDemoAppServiceBase, IInvestmentAssetBooksAppService
    {
        private readonly IRepository<InvestmentAssetBook, Guid> _investmentAssetBookRepository;
        private readonly IRepository<InvestmentEclUpload, Guid> _lookup_investmentEclUploadRepository;


        public InvestmentAssetBooksAppService(IRepository<InvestmentAssetBook, Guid> investmentAssetBookRepository, IRepository<InvestmentEclUpload, Guid> lookup_investmentEclUploadRepository)
        {
            _investmentAssetBookRepository = investmentAssetBookRepository;
            _lookup_investmentEclUploadRepository = lookup_investmentEclUploadRepository;

        }

        public async Task<PagedResultDto<GetInvestmentAssetBookForViewDto>> GetAll(GetAllInvestmentAssetBooksInput input)
        {

            var filteredInvestmentAssetBooks = _investmentAssetBookRepository.GetAll()
                        .Include(e => e.InvestmentEclUploadFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AssetDescription.Contains(input.Filter) || e.AssetType.Contains(input.Filter) || e.CounterParty.Contains(input.Filter) || e.SovereignDebt.Contains(input.Filter) || e.RatingAgency.Contains(input.Filter) || e.CreditRatingAtPurchaseDate.Contains(input.Filter) || e.CurrentCreditRating.Contains(input.Filter) || e.PrincipalAmortisation.Contains(input.Filter) || e.RepaymentTerms.Contains(input.Filter) || e.BusinessModelClassification.Contains(input.Filter) || e.PrudentialClassification.Contains(input.Filter) || e.ForebearanceFlag.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InvestmentEclUploadUploadCommentFilter), e => e.InvestmentEclUploadFk != null && e.InvestmentEclUploadFk.UploadComment == input.InvestmentEclUploadUploadCommentFilter);

            var pagedAndFilteredInvestmentAssetBooks = filteredInvestmentAssetBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentAssetBooks = from o in pagedAndFilteredInvestmentAssetBooks
                                       join o1 in _lookup_investmentEclUploadRepository.GetAll() on o.InvestmentEclUploadId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       select new GetInvestmentAssetBookForViewDto()
                                       {
                                           InvestmentAssetBook = new InvestmentAssetBookDto
                                           {
                                               AssetDescription = o.AssetDescription,
                                               AssetType = o.AssetType,
                                               CounterParty = o.CounterParty,
                                               SovereignDebt = o.SovereignDebt,
                                               RatingAgency = o.RatingAgency,
                                               CreditRatingAtPurchaseDate = o.CreditRatingAtPurchaseDate,
                                               CurrentCreditRating = o.CurrentCreditRating,
                                               NominalAmount = o.NominalAmount,
                                               PrincipalAmortisation = o.PrincipalAmortisation,
                                               RepaymentTerms = o.RepaymentTerms,
                                               CarryAmountNGAAP = o.CarryAmountNGAAP,
                                               CarryingAmountIFRS = o.CarryingAmountIFRS,
                                               Coupon = o.Coupon,
                                               Eir = o.Eir,
                                               PurchaseDate = o.PurchaseDate,
                                               IssueDate = o.IssueDate,
                                               PurchasePrice = o.PurchasePrice,
                                               MaturityDate = o.MaturityDate,
                                               RedemptionPrice = o.RedemptionPrice,
                                               BusinessModelClassification = o.BusinessModelClassification,
                                               Ias39Impairment = o.Ias39Impairment,
                                               PrudentialClassification = o.PrudentialClassification,
                                               ForebearanceFlag = o.ForebearanceFlag,
                                               Id = o.Id
                                           },
                                           InvestmentEclUploadUploadComment = s1 == null ? "" : s1.UploadComment.ToString()
                                       };

            var totalCount = await filteredInvestmentAssetBooks.CountAsync();

            return new PagedResultDto<GetInvestmentAssetBookForViewDto>(
                totalCount,
                await investmentAssetBooks.ToListAsync()
            );
        }

        public async Task<PagedResultDto<GetInvestmentAssetBookForViewDto>> GetEclData(GetAllEclAssetBookDataInput input)
        {
            //input.Filter = input.Filter.ToLower();
            var filteredInvestmentAssetBooks = _investmentAssetBookRepository.GetAll()
                        .Include(e => e.InvestmentEclUploadFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AssetDescription.ToLower().Contains(input.Filter) || e.AssetType.ToLower().Contains(input.Filter) || e.CounterParty.ToLower().Contains(input.Filter) || e.SovereignDebt.ToLower().Contains(input.Filter) || e.RatingAgency.ToLower().Contains(input.Filter) || e.CreditRatingAtPurchaseDate.ToLower().Contains(input.Filter) || e.CurrentCreditRating.ToLower().Contains(input.Filter) || e.PrincipalAmortisation.ToLower().Contains(input.Filter) || e.RepaymentTerms.ToLower().Contains(input.Filter) || e.BusinessModelClassification.ToLower().Contains(input.Filter) || e.PrudentialClassification.ToLower().Contains(input.Filter) || e.ForebearanceFlag.ToLower().Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AccountNoFilter), e => e.AssetDescription.ToLower().Contains(input.AccountNoFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ContractNoFilter), e => e.AssetType == input.ContractNoFilter)
                        .Where(x => x.InvestmentEclUploadId == input.EclId);

            var pagedAndFilteredInvestmentAssetBooks = filteredInvestmentAssetBooks
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentAssetBooks = from o in pagedAndFilteredInvestmentAssetBooks
                                       select new GetInvestmentAssetBookForViewDto()
                                       {
                                           InvestmentAssetBook = new InvestmentAssetBookDto
                                           {
                                               AssetDescription = o.AssetDescription,
                                               AssetType = o.AssetType,
                                               CounterParty = o.CounterParty,
                                               SovereignDebt = o.SovereignDebt,
                                               RatingAgency = o.RatingAgency,
                                               CreditRatingAtPurchaseDate = o.CreditRatingAtPurchaseDate,
                                               CurrentCreditRating = o.CurrentCreditRating,
                                               NominalAmount = o.NominalAmount,
                                               PrincipalAmortisation = o.PrincipalAmortisation,
                                               RepaymentTerms = o.RepaymentTerms,
                                               CarryAmountNGAAP = o.CarryAmountNGAAP,
                                               CarryingAmountIFRS = o.CarryingAmountIFRS,
                                               Coupon = o.Coupon,
                                               Eir = o.Eir,
                                               PurchaseDate = o.PurchaseDate,
                                               IssueDate = o.IssueDate,
                                               PurchasePrice = o.PurchasePrice,
                                               MaturityDate = o.MaturityDate,
                                               RedemptionPrice = o.RedemptionPrice,
                                               BusinessModelClassification = o.BusinessModelClassification,
                                               Ias39Impairment = o.Ias39Impairment,
                                               PrudentialClassification = o.PrudentialClassification,
                                               ForebearanceFlag = o.ForebearanceFlag,
                                               Id = o.Id
                                           }
                                       };

            var totalCount = await filteredInvestmentAssetBooks.CountAsync();

            return new PagedResultDto<GetInvestmentAssetBookForViewDto>(
                totalCount,
                await investmentAssetBooks.ToListAsync()
            );
        }


        [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks_Edit)]
        public async Task<GetInvestmentAssetBookForEditOutput> GetInvestmentAssetBookForEdit(EntityDto<Guid> input)
        {
            var investmentAssetBook = await _investmentAssetBookRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvestmentAssetBookForEditOutput { InvestmentAssetBook = ObjectMapper.Map<CreateOrEditInvestmentAssetBookDto>(investmentAssetBook) };

            if (output.InvestmentAssetBook.InvestmentEclUploadId != null)
            {
                var _lookupInvestmentEclUpload = await _lookup_investmentEclUploadRepository.FirstOrDefaultAsync((Guid)output.InvestmentAssetBook.InvestmentEclUploadId);
                output.InvestmentEclUploadUploadComment = _lookupInvestmentEclUpload.UploadComment.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvestmentAssetBookDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks_Create)]
        protected virtual async Task Create(CreateOrEditInvestmentAssetBookDto input)
        {
            var investmentAssetBook = ObjectMapper.Map<InvestmentAssetBook>(input);



            await _investmentAssetBookRepository.InsertAsync(investmentAssetBook);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks_Edit)]
        protected virtual async Task Update(CreateOrEditInvestmentAssetBookDto input)
        {
            var investmentAssetBook = await _investmentAssetBookRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, investmentAssetBook);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentAssetBookRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentAssetBooks)]
        public async Task<PagedResultDto<InvestmentAssetBookInvestmentEclUploadLookupTableDto>> GetAllInvestmentEclUploadForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_investmentEclUploadRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.UploadComment.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var investmentEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentAssetBookInvestmentEclUploadLookupTableDto>();
            foreach (var investmentEclUpload in investmentEclUploadList)
            {
                lookupTableDtoList.Add(new InvestmentAssetBookInvestmentEclUploadLookupTableDto
                {
                    Id = investmentEclUpload.Id.ToString(),
                    DisplayName = investmentEclUpload.UploadComment?.ToString()
                });
            }

            return new PagedResultDto<InvestmentAssetBookInvestmentEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}