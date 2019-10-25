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
	[AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures)]
    public class ObeEclResultSummaryTopExposuresAppService : TestDemoAppServiceBase, IObeEclResultSummaryTopExposuresAppService
    {
		 private readonly IRepository<ObeEclResultSummaryTopExposure, Guid> _obeEclResultSummaryTopExposureRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclResultSummaryTopExposuresAppService(IRepository<ObeEclResultSummaryTopExposure, Guid> obeEclResultSummaryTopExposureRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository, IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclResultSummaryTopExposureRepository = obeEclResultSummaryTopExposureRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclResultSummaryTopExposureForViewDto>> GetAll(GetAllObeEclResultSummaryTopExposuresInput input)
         {
			
			var filteredObeEclResultSummaryTopExposures = _obeEclResultSummaryTopExposureRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.Include( e => e.ObeEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinPreOverrideExposureFilter != null, e => e.PreOverrideExposure >= input.MinPreOverrideExposureFilter)
						.WhereIf(input.MaxPreOverrideExposureFilter != null, e => e.PreOverrideExposure <= input.MaxPreOverrideExposureFilter)
						.WhereIf(input.MinPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment >= input.MinPreOverrideImpairmentFilter)
						.WhereIf(input.MaxPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment <= input.MaxPreOverrideImpairmentFilter)
						.WhereIf(input.MinPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio >= input.MinPreOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio <= input.MaxPreOverrideCoverageRatioFilter)
						.WhereIf(input.MinPostOverrideExposureFilter != null, e => e.PostOverrideExposure >= input.MinPostOverrideExposureFilter)
						.WhereIf(input.MaxPostOverrideExposureFilter != null, e => e.PostOverrideExposure <= input.MaxPostOverrideExposureFilter)
						.WhereIf(input.MinPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment >= input.MinPostOverrideImpairmentFilter)
						.WhereIf(input.MaxPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment <= input.MaxPostOverrideImpairmentFilter)
						.WhereIf(input.MinPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio >= input.MinPostOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio <= input.MaxPostOverrideCoverageRatioFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId.ToLower() == input.ObeEclTenantIdFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclDataLoanBookCustomerNameFilter), e => e.ObeEclDataLoanBookFk != null && e.ObeEclDataLoanBookFk.CustomerName.ToLower() == input.ObeEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEclResultSummaryTopExposures = filteredObeEclResultSummaryTopExposures
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclResultSummaryTopExposures = from o in pagedAndFilteredObeEclResultSummaryTopExposures
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ObeEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetObeEclResultSummaryTopExposureForViewDto() {
							ObeEclResultSummaryTopExposure = new ObeEclResultSummaryTopExposureDto
							{
                                PreOverrideExposure = o.PreOverrideExposure,
                                PreOverrideImpairment = o.PreOverrideImpairment,
                                PreOverrideCoverageRatio = o.PreOverrideCoverageRatio,
                                PostOverrideExposure = o.PostOverrideExposure,
                                PostOverrideImpairment = o.PostOverrideImpairment,
                                PostOverrideCoverageRatio = o.PostOverrideCoverageRatio,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	ObeEclDataLoanBookCustomerName = s2 == null ? "" : s2.CustomerName.ToString()
						};

            var totalCount = await filteredObeEclResultSummaryTopExposures.CountAsync();

            return new PagedResultDto<GetObeEclResultSummaryTopExposureForViewDto>(
                totalCount,
                await obeEclResultSummaryTopExposures.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Edit)]
		 public async Task<GetObeEclResultSummaryTopExposureForEditOutput> GetObeEclResultSummaryTopExposureForEdit(EntityDto<Guid> input)
         {
            var obeEclResultSummaryTopExposure = await _obeEclResultSummaryTopExposureRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclResultSummaryTopExposureForEditOutput {ObeEclResultSummaryTopExposure = ObjectMapper.Map<CreateOrEditObeEclResultSummaryTopExposureDto>(obeEclResultSummaryTopExposure)};

		    if (output.ObeEclResultSummaryTopExposure.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclResultSummaryTopExposure.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }

		    if (output.ObeEclResultSummaryTopExposure.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclResultSummaryTopExposure.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookCustomerName = _lookupObeEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclResultSummaryTopExposureDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclResultSummaryTopExposureDto input)
         {
            var obeEclResultSummaryTopExposure = ObjectMapper.Map<ObeEclResultSummaryTopExposure>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclResultSummaryTopExposure.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclResultSummaryTopExposureRepository.InsertAsync(obeEclResultSummaryTopExposure);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclResultSummaryTopExposureDto input)
         {
            var obeEclResultSummaryTopExposure = await _obeEclResultSummaryTopExposureRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclResultSummaryTopExposure);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclResultSummaryTopExposureRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures)]
         public async Task<PagedResultDto<ObeEclResultSummaryTopExposureObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclResultSummaryTopExposureObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclResultSummaryTopExposureObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclResultSummaryTopExposureObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryTopExposures)]
         public async Task<PagedResultDto<ObeEclResultSummaryTopExposureObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclResultSummaryTopExposureObeEclDataLoanBookLookupTableDto>();
			foreach(var obeEclDataLoanBook in obeEclDataLoanBookList){
				lookupTableDtoList.Add(new ObeEclResultSummaryTopExposureObeEclDataLoanBookLookupTableDto
				{
					Id = obeEclDataLoanBook.Id.ToString(),
					DisplayName = obeEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<ObeEclResultSummaryTopExposureObeEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}