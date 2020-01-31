using Abp.Organizations;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclConfig
{
	[AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds)]
    public class AffiliateOverrideThresholdsAppService : TestDemoAppServiceBase, IAffiliateOverrideThresholdsAppService
    {
		 private readonly IRepository<AffiliateOverrideThreshold> _affiliateOverrideThresholdRepository;
		 private readonly IRepository<OrganizationUnit,long> _lookup_organizationUnitRepository;
		 

		  public AffiliateOverrideThresholdsAppService(IRepository<AffiliateOverrideThreshold> affiliateOverrideThresholdRepository , IRepository<OrganizationUnit, long> lookup_organizationUnitRepository) 
		  {
			_affiliateOverrideThresholdRepository = affiliateOverrideThresholdRepository;
			_lookup_organizationUnitRepository = lookup_organizationUnitRepository;
		
		  }

		 public async Task<PagedResultDto<GetAffiliateOverrideThresholdForViewDto>> GetAll(GetAllAffiliateConfigurationInput input)
         {

            var filteredAffiliateOverrideThresholds = _affiliateOverrideThresholdRepository.GetAll()
                        .Include(e => e.OrganizationUnitFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false);
						//.WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter);

			var pagedAndFilteredAffiliateOverrideThresholds = filteredAffiliateOverrideThresholds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var affiliateOverrideThresholds = from o in pagedAndFilteredAffiliateOverrideThresholds
                         join o1 in _lookup_organizationUnitRepository.GetAll() on o.OrganizationUnitId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetAffiliateOverrideThresholdForViewDto() {
							AffiliateOverrideThreshold = new AffiliateOverrideThresholdDto
							{
                                Threshold = o.Threshold,
                                Id = o.Id
							},
                         	OrganizationUnitDisplayName = s1 == null ? "" : s1.DisplayName.ToString()
						};

            var totalCount = await filteredAffiliateOverrideThresholds.CountAsync();

            return new PagedResultDto<GetAffiliateOverrideThresholdForViewDto>(
                totalCount,
                await affiliateOverrideThresholds.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Edit)]
		 public async Task<GetAffiliateForEditOutput> GetAffiliateOverrideThresholdForEdit(EntityDto input)
         {
            var affiliateOverrideThreshold = await _affiliateOverrideThresholdRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAffiliateForEditOutput {AffiliateConfiguration = ObjectMapper.Map<CreateOrEditAffiliateDto>(affiliateOverrideThreshold)};

		    //if (output.AffiliateOverrideThreshold.OrganizationUnitId != null)
      //      {
      //          var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.AffiliateOverrideThreshold.OrganizationUnitId);
      //          output.OrganizationUnitDisplayName = _lookupOrganizationUnit.DisplayName.ToString();
      //      }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAffiliateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Create)]
		 protected virtual async Task Create(CreateOrEditAffiliateDto input)
         {
            var affiliateOverrideThreshold = ObjectMapper.Map<AffiliateOverrideThreshold>(input);

			

            await _affiliateOverrideThresholdRepository.InsertAsync(affiliateOverrideThreshold);
         }

		 [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Edit)]
		 protected virtual async Task Update(CreateOrEditAffiliateDto input)
         {
            var affiliateOverrideThreshold = await _affiliateOverrideThresholdRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, affiliateOverrideThreshold);
         }

		 [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _affiliateOverrideThresholdRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds)]
         public async Task<PagedResultDto<AffiliateOverrideThresholdOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_organizationUnitRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.DisplayName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var organizationUnitList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<AffiliateOverrideThresholdOrganizationUnitLookupTableDto>();
			foreach(var organizationUnit in organizationUnitList){
				lookupTableDtoList.Add(new AffiliateOverrideThresholdOrganizationUnitLookupTableDto
				{
					Id = organizationUnit.Id,
					DisplayName = organizationUnit.DisplayName?.ToString()
				});
			}

            return new PagedResultDto<AffiliateOverrideThresholdOrganizationUnitLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}