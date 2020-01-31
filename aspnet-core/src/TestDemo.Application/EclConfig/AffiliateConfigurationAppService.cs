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
    public class AffiliateConfigurationAppService : TestDemoAppServiceBase
    {
        private readonly IRepository<AffiliateConfiguration, long> _affiliateRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;

        public AffiliateConfigurationAppService(
            IRepository<AffiliateConfiguration, long> affiliateRepository, 
            OrganizationUnitManager organizationUnitManager)
        {
            _affiliateRepository = affiliateRepository;
            _organizationUnitManager = organizationUnitManager;
        }

        public async Task<PagedResultDto<GetAffiliateConfigurationForViewDto>> GetAll(GetAllAffiliateConfigurationInput input)
        {

            var filteredAffiliateOverrideThresholds = _affiliateRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false);
            //.WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.OrganizationUnitFk != null && e.OrganizationUnitFk.DisplayName == input.OrganizationUnitDisplayNameFilter);

            var pagedAndFilteredAffiliateOverrideThresholds = filteredAffiliateOverrideThresholds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var affiliateOverrideThresholds = from o in pagedAndFilteredAffiliateOverrideThresholds
                                              select new GetAffiliateConfigurationForViewDto()
                                              {
                                                  AffiliateConfiguration = new AffiliateConfigurationDto
                                                  {
                                                      OverrideThreshold = o.OverrideThreshold,
                                                      AffiliateName = o.DisplayName,
                                                      Code = o.Code,
                                                      Id = (int)o.Id
                                                  }
                                              };

            var totalCount = await filteredAffiliateOverrideThresholds.CountAsync();

            return new PagedResultDto<GetAffiliateConfigurationForViewDto>(
                totalCount,
                await affiliateOverrideThresholds.ToListAsync()
            );
        }

        public async Task CreateOrEdit(CreateOrEditAffiliateDto input)
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

        [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Create)]
        protected virtual async Task Create(CreateOrEditAffiliateDto input)
        {
            var affiliate = ObjectMapper.Map<AffiliateConfiguration>(input);
            
            await _organizationUnitManager.CreateAsync(affiliate);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissions.Pages_AffiliateOverrideThresholds_Edit)]
        protected virtual async Task Update(CreateOrEditAffiliateDto input)
        {
            var affiliate = await _affiliateRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, affiliate);
            await _organizationUnitManager.UpdateAsync(affiliate);
        }

    }
}
