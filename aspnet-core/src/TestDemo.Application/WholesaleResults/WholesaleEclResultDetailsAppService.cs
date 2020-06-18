using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleResults
{
    public class WholesaleEclResultDetailsAppService : TestDemoAppServiceBase, IWholesaleEclResultDetailsAppService
    {
		 private readonly IRepository<WholesaleEclResultDetail, Guid> _wholesaleEclResultDetailRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclResultDetailsAppService(IRepository<WholesaleEclResultDetail, Guid> wholesaleEclResultDetailRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository, IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclResultDetailRepository = wholesaleEclResultDetailRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclResultDetailForViewDto>> GetAll(GetAllWholesaleEclResultDetailsInput input)
         {
			
			var filteredWholesaleEclResultDetails = _wholesaleEclResultDetailRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.Include( e => e.WholesaleEclDataLoanBookFk)
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
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclDataLoanBookCustomerNameFilter), e => e.WholesaleEclDataLoanBookFk != null && e.WholesaleEclDataLoanBookFk.CustomerName.ToLower() == input.WholesaleEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclResultDetails = filteredWholesaleEclResultDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclResultDetails = from o in pagedAndFilteredWholesaleEclResultDetails
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.WholesaleEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetWholesaleEclResultDetailForViewDto() {
							WholesaleEclResultDetail = new WholesaleEclResultDetailDto
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
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	WholesaleEclDataLoanBookCustomerName = s2 == null ? "" : s2.CustomerName.ToString()
						};

            var totalCount = await filteredWholesaleEclResultDetails.CountAsync();

            return new PagedResultDto<GetWholesaleEclResultDetailForViewDto>(
                totalCount,
                await wholesaleEclResultDetails.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclResultDetailForEditOutput> GetWholesaleEclResultDetailForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclResultDetail = await _wholesaleEclResultDetailRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclResultDetailForEditOutput {WholesaleEclResultDetail = ObjectMapper.Map<CreateOrEditWholesaleEclResultDetailDto>(wholesaleEclResultDetail)};

		    if (output.WholesaleEclResultDetail.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultDetail.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }

		    if (output.WholesaleEclResultDetail.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultDetail.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookCustomerName = _lookupWholesaleEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclResultDetailDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclResultDetailDto input)
         {
            var wholesaleEclResultDetail = ObjectMapper.Map<WholesaleEclResultDetail>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclResultDetail.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclResultDetailRepository.InsertAsync(wholesaleEclResultDetail);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclResultDetailDto input)
         {
            var wholesaleEclResultDetail = await _wholesaleEclResultDetailRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclResultDetail);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclResultDetailRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclResultDetailWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultDetailWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclResultDetailWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultDetailWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<WholesaleEclResultDetailWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultDetailWholesaleEclDataLoanBookLookupTableDto>();
			foreach(var wholesaleEclDataLoanBook in wholesaleEclDataLoanBookList){
				lookupTableDtoList.Add(new WholesaleEclResultDetailWholesaleEclDataLoanBookLookupTableDto
				{
					Id = wholesaleEclDataLoanBook.Id.ToString(),
					DisplayName = wholesaleEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultDetailWholesaleEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}