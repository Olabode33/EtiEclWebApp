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
using TestDemo.EclShared;
using TestDemo.EclShared.Dtos;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;

namespace TestDemo.EclConfig
{
    public class AffiliateConfigurationAppService : TestDemoAppServiceBase
    {
        private readonly IRepository<Affiliate, long> _affiliateRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _assumptionRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public AffiliateConfigurationAppService(
            IRepository<Affiliate, long> affiliateRepository,
            IRepository<AffiliateAssumption, Guid> assumptionRepository,
            IBackgroundJobManager backgroundJobManager,
            OrganizationUnitManager organizationUnitManager)
        {
            _affiliateRepository = affiliateRepository;
            _assumptionRepository = assumptionRepository;
            _organizationUnitManager = organizationUnitManager;
            _backgroundJobManager = backgroundJobManager;
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
                                                      Currency=o.Currency,
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

        public async Task<GetAffiliateForEditOutput> GetAffiliateEdit(EntityDto input)
        {
            var affiliateConfiguration = await _affiliateRepository.FirstOrDefaultAsync(input.Id);
            var output = new GetAffiliateForEditOutput { AffiliateConfiguration = ObjectMapper.Map<CreateOrEditAffiliateDto>(affiliateConfiguration) };
            return output;
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

        protected virtual async Task Create(CreateOrEditAffiliateDto input)
        {
            var affiliate = new Affiliate
            {
                DisplayName = input.DisplayName,
                OverrideThreshold = input.OverrideThreshold,
                Currency=input.Currency,
                ParentId = null //input.ParentId
            };
            
            await _organizationUnitManager.CreateAsync(affiliate);
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.ParentId != null)
            {
                await CreateAssumption(affiliate.Id, (long)input.ParentId);
            }
        }

        protected virtual async Task CreateAssumption(long id, long from)
        {
            await _assumptionRepository.InsertAsync(new AffiliateAssumption
            {
                OrganizationUnitId = id
            });
            await _backgroundJobManager.EnqueueAsync<EclShared.Importing.CopyAffiliateAssumptionJob, CopyAffiliateAssumptionJobArgs>(new CopyAffiliateAssumptionJobArgs()
            {
                FromAffiliateId = from,
                ToAffiliateId = id,
                User = AbpSession.ToUserIdentifier()
            });
        }

        protected virtual async Task Update(CreateOrEditAffiliateDto input)
        {
            var affiliate = await _affiliateRepository.FirstOrDefaultAsync((int)input.Id);

            affiliate.OverrideThreshold = input.OverrideThreshold;
            affiliate.Currency = input.Currency;
            affiliate.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(affiliate);
        }
    }
}
