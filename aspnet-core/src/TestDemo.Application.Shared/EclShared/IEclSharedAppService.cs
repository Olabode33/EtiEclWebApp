using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared
{
    public interface IEclSharedAppService : IApplicationService
    {
        Task<PagedResultDto<GetAllEclForWorkspaceDto>> GetAllEclForWorkspace(GetAllForLookupTableInput input);
        Task<List<AssumptionDto>> GetFrameworkAssumptionSnapshot(FrameworkEnum framework);
        Task<List<EadInputAssumptionDto>> GetEadInputAssumptionSnapshot(FrameworkEnum framework);
        Task<List<LgdAssumptionDto>> GetLgdInputAssumptionSnapshot(FrameworkEnum framework);
        Task<PagedResultDto<GetAllAffiliateAssumptionDto>> GetAllAffiliateAssumption(GetAllForLookupTableInput input);
        Task<List<AssumptionDto>> GetAffiliateFrameworkAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<EadInputAssumptionDto>> GetAffiliateEadAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<LgdAssumptionDto>> GetAffiliateLgdAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputAssumptionDto>> GetAffiliatePdAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputAssumptionMacroeconomicInputDto>> GetAffiliatePdMacroeconomicInputAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetAffiliatePdMacroeconomicProjectionAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputAssumptionNonInternalModelDto>> GetAffiliatePdNonInternalModelAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputAssumptionNplIndexDto>> GetAffiliatePdNplIndexAssumption(GetAffiliateAssumptionInputDto input);
        Task<List<PdInputSnPCummulativeDefaultRateDto>> GetAffiliatePdSnpCummulativeAssumption(GetAffiliateAssumptionInputDto input);
        Task<GetAllPdAssumptionsDto> GetAllPdAssumptionsForAffiliate(GetAffiliateAssumptionInputDto input);
        Task UpdateAffiliateAssumption(CreateOrEditAffiliateAssumptionsDto input);
        Task<CreateOrEditAffiliateAssumptionsDto> GetAffiliateAssumptionForEdit(long input);
    }
}
