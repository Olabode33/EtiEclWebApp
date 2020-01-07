using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclConfig
{
    public interface IAffiliateOverrideThresholdsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAffiliateOverrideThresholdForViewDto>> GetAll(GetAllAffiliateOverrideThresholdsInput input);

		Task<GetAffiliateOverrideThresholdForEditOutput> GetAffiliateOverrideThresholdForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditAffiliateOverrideThresholdDto input);

		Task Delete(EntityDto input);

		
		Task<PagedResultDto<AffiliateOverrideThresholdOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input);
		
    }
}