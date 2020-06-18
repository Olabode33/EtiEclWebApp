using TestDemo.OBE;
using TestDemo.ObeInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeResults
{
    public class ObeEclResultDetailsAppService : TestDemoAppServiceBase, IObeEclResultDetailsAppService
    {
		 private readonly IRepository<ObeEclResultDetail, Guid> _obeEclResultDetailRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclResultDetailsAppService(IRepository<ObeEclResultDetail, Guid> obeEclResultDetailRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository, IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclResultDetailRepository = obeEclResultDetailRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclResultDetailForViewDto>> GetAll(GetAllObeEclResultDetailsInput input)
         {
			
			var filteredObeEclResultDetails = _obeEclResultDetailRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.Include( e => e.ObeEclDataLoanBookFk)
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
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclDataLoanBookCustomerNameFilter), e => e.ObeEclDataLoanBookFk != null && e.ObeEclDataLoanBookFk.CustomerName.ToLower() == input.ObeEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEclResultDetails = filteredObeEclResultDetails
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclResultDetails = from o in pagedAndFilteredObeEclResultDetails
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ObeEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetObeEclResultDetailForViewDto() {
							ObeEclResultDetail = new ObeEclResultDetailDto
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
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	ObeEclDataLoanBookCustomerName = s2 == null ? "" : s2.CustomerName.ToString()
						};

            var totalCount = await filteredObeEclResultDetails.CountAsync();

            return new PagedResultDto<GetObeEclResultDetailForViewDto>(
                totalCount,
                await obeEclResultDetails.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclResultDetailForEditOutput> GetObeEclResultDetailForEdit(EntityDto<Guid> input)
         {
            var obeEclResultDetail = await _obeEclResultDetailRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclResultDetailForEditOutput {ObeEclResultDetail = ObjectMapper.Map<CreateOrEditObeEclResultDetailDto>(obeEclResultDetail)};

		    if (output.ObeEclResultDetail.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclResultDetail.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }

		    if (output.ObeEclResultDetail.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclResultDetail.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookCustomerName = _lookupObeEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclResultDetailDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclResultDetailDto input)
         {
            var obeEclResultDetail = ObjectMapper.Map<ObeEclResultDetail>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclResultDetail.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclResultDetailRepository.InsertAsync(obeEclResultDetail);
         }

		 protected virtual async Task Update(CreateOrEditObeEclResultDetailDto input)
         {
            var obeEclResultDetail = await _obeEclResultDetailRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclResultDetail);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclResultDetailRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclResultDetailObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclResultDetailObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclResultDetailObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclResultDetailObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<ObeEclResultDetailObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclResultDetailObeEclDataLoanBookLookupTableDto>();
			foreach(var obeEclDataLoanBook in obeEclDataLoanBookList){
				lookupTableDtoList.Add(new ObeEclResultDetailObeEclDataLoanBookLookupTableDto
				{
					Id = obeEclDataLoanBook.Id.ToString(),
					DisplayName = obeEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<ObeEclResultDetailObeEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}