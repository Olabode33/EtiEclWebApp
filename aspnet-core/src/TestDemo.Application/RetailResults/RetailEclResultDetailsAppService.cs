using TestDemo.Retail;
using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailResults
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails)]
    public class RetailEclResultDetailsAppService : TestDemoAppServiceBase, IRetailEclResultDetailsAppService
    {
		 private readonly IRepository<RetailEclResultDetail, Guid> _retailEclResultDetailRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclResultDetailsAppService(IRepository<RetailEclResultDetail, Guid> retailEclResultDetailRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository, IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclResultDetailRepository = retailEclResultDetailRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclResultDetailForViewDto>> GetAll(GetAllRetailEclResultDetailsInput input)
         {
			
			var filteredRetailEclResultDetails = _retailEclResultDetailRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.Include( e => e.RetailEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractID.Contains(input.Filter) || e.AccountNo.Contains(input.Filter) || e.CustomerNo.Contains(input.Filter) || e.Segment.Contains(input.Filter) || e.ProductType.Contains(input.Filter) || e.Sector.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractIDFilter),  e => e.ContractID.ToLower() == input.ContractIDFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.AccountNoFilter),  e => e.AccountNo.ToLower() == input.AccountNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNoFilter),  e => e.CustomerNo.ToLower() == input.CustomerNoFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SegmentFilter),  e => e.Segment.ToLower() == input.SegmentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ProductTypeFilter),  e => e.ProductType.ToLower() == input.ProductTypeFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SectorFilter),  e => e.Sector.ToLower() == input.SectorFilter.ToLower().Trim())
						.WhereIf(input.MinStageFilter != null, e => e.Stage >= input.MinStageFilter)
						.WhereIf(input.MaxStageFilter != null, e => e.Stage <= input.MaxStageFilter)
						.WhereIf(input.MinOutstandingBalanceFilter != null, e => e.OutstandingBalance >= input.MinOutstandingBalanceFilter)
						.WhereIf(input.MaxOutstandingBalanceFilter != null, e => e.OutstandingBalance <= input.MaxOutstandingBalanceFilter)
						.WhereIf(input.MinPreOverrideEclBestFilter != null, e => e.PreOverrideEclBest >= input.MinPreOverrideEclBestFilter)
						.WhereIf(input.MaxPreOverrideEclBestFilter != null, e => e.PreOverrideEclBest <= input.MaxPreOverrideEclBestFilter)
						.WhereIf(input.MinPreOverrideEclOptimisticFilter != null, e => e.PreOverrideEclOptimistic >= input.MinPreOverrideEclOptimisticFilter)
						.WhereIf(input.MaxPreOverrideEclOptimisticFilter != null, e => e.PreOverrideEclOptimistic <= input.MaxPreOverrideEclOptimisticFilter)
						.WhereIf(input.MinPreOverrideEclDownturnFilter != null, e => e.PreOverrideEclDownturn >= input.MinPreOverrideEclDownturnFilter)
						.WhereIf(input.MaxPreOverrideEclDownturnFilter != null, e => e.PreOverrideEclDownturn <= input.MaxPreOverrideEclDownturnFilter)
						.WhereIf(input.MinOverrideStageFilter != null, e => e.OverrideStage >= input.MinOverrideStageFilter)
						.WhereIf(input.MaxOverrideStageFilter != null, e => e.OverrideStage <= input.MaxOverrideStageFilter)
						.WhereIf(input.MinOverrideTTRYearsFilter != null, e => e.OverrideTTRYears >= input.MinOverrideTTRYearsFilter)
						.WhereIf(input.MaxOverrideTTRYearsFilter != null, e => e.OverrideTTRYears <= input.MaxOverrideTTRYearsFilter)
						.WhereIf(input.MinOverrideFSVFilter != null, e => e.OverrideFSV >= input.MinOverrideFSVFilter)
						.WhereIf(input.MaxOverrideFSVFilter != null, e => e.OverrideFSV <= input.MaxOverrideFSVFilter)
						.WhereIf(input.MinOverrideOverlayFilter != null, e => e.OverrideOverlay >= input.MinOverrideOverlayFilter)
						.WhereIf(input.MaxOverrideOverlayFilter != null, e => e.OverrideOverlay <= input.MaxOverrideOverlayFilter)
						.WhereIf(input.MinPostOverrideEclBestFilter != null, e => e.PostOverrideEclBest >= input.MinPostOverrideEclBestFilter)
						.WhereIf(input.MaxPostOverrideEclBestFilter != null, e => e.PostOverrideEclBest <= input.MaxPostOverrideEclBestFilter)
						.WhereIf(input.MinPostOverrideEclOptimisticFilter != null, e => e.PostOverrideEclOptimistic >= input.MinPostOverrideEclOptimisticFilter)
						.WhereIf(input.MaxPostOverrideEclOptimisticFilter != null, e => e.PostOverrideEclOptimistic <= input.MaxPostOverrideEclOptimisticFilter)
						.WhereIf(input.MinPostOverrideEclDownturnFilter != null, e => e.PostOverrideEclDownturn >= input.MinPostOverrideEclDownturnFilter)
						.WhereIf(input.MaxPostOverrideEclDownturnFilter != null, e => e.PostOverrideEclDownturn <= input.MaxPostOverrideEclDownturnFilter)
						.WhereIf(input.MinPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment >= input.MinPreOverrideImpairmentFilter)
						.WhereIf(input.MaxPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment <= input.MaxPreOverrideImpairmentFilter)
						.WhereIf(input.MinPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment >= input.MinPostOverrideImpairmentFilter)
						.WhereIf(input.MaxPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment <= input.MaxPostOverrideImpairmentFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclDataLoanBookCustomerNameFilter), e => e.RetailEclDataLoanBookFk != null && e.RetailEclDataLoanBookFk.CustomerName.ToLower() == input.RetailEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclResultDetails = filteredRetailEclResultDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclResultDetails = from o in pagedAndFilteredRetailEclResultDetails
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.RetailEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetRetailEclResultDetailForViewDto() {
							RetailEclResultDetail = new RetailEclResultDetailDto
							{
                                ContractID = o.ContractID,
                                AccountNo = o.AccountNo,
                                CustomerNo = o.CustomerNo,
                                Segment = o.Segment,
                                ProductType = o.ProductType,
                                Sector = o.Sector,
                                Stage = o.Stage,
                                OutstandingBalance = o.OutstandingBalance,
                                PreOverrideEclBest = o.PreOverrideEclBest,
                                PreOverrideEclOptimistic = o.PreOverrideEclOptimistic,
                                PreOverrideEclDownturn = o.PreOverrideEclDownturn,
                                OverrideStage = o.OverrideStage,
                                OverrideTTRYears = o.OverrideTTRYears,
                                OverrideFSV = o.OverrideFSV,
                                OverrideOverlay = o.OverrideOverlay,
                                PostOverrideEclBest = o.PostOverrideEclBest,
                                PostOverrideEclOptimistic = o.PostOverrideEclOptimistic,
                                PostOverrideEclDownturn = o.PostOverrideEclDownturn,
                                PreOverrideImpairment = o.PreOverrideImpairment,
                                PostOverrideImpairment = o.PostOverrideImpairment,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	RetailEclDataLoanBookCustomerName = s2 == null ? "" : s2.CustomerName.ToString()
						};

            var totalCount = await filteredRetailEclResultDetails.CountAsync();

            return new PagedResultDto<GetRetailEclResultDetailForViewDto>(
                totalCount,
                await retailEclResultDetails.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails_Edit)]
		 public async Task<GetRetailEclResultDetailForEditOutput> GetRetailEclResultDetailForEdit(EntityDto<Guid> input)
         {
            var retailEclResultDetail = await _retailEclResultDetailRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclResultDetailForEditOutput {RetailEclResultDetail = ObjectMapper.Map<CreateOrEditRetailEclResultDetailDto>(retailEclResultDetail)};

		    if (output.RetailEclResultDetail.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultDetail.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

		    if (output.RetailEclResultDetail.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultDetail.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookCustomerName = _lookupRetailEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclResultDetailDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclResultDetailDto input)
         {
            var retailEclResultDetail = ObjectMapper.Map<RetailEclResultDetail>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclResultDetail.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclResultDetailRepository.InsertAsync(retailEclResultDetail);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclResultDetailDto input)
         {
            var retailEclResultDetail = await _retailEclResultDetailRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclResultDetail);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclResultDetailRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails)]
         public async Task<PagedResultDto<RetailEclResultDetailRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultDetailRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclResultDetailRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultDetailRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_RetailEclResultDetails)]
         public async Task<PagedResultDto<RetailEclResultDetailRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultDetailRetailEclDataLoanBookLookupTableDto>();
			foreach(var retailEclDataLoanBook in retailEclDataLoanBookList){
				lookupTableDtoList.Add(new RetailEclResultDetailRetailEclDataLoanBookLookupTableDto
				{
					Id = retailEclDataLoanBook.Id.ToString(),
					DisplayName = retailEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultDetailRetailEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}