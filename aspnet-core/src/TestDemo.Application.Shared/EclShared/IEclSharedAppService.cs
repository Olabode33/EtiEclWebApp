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
    }
}
