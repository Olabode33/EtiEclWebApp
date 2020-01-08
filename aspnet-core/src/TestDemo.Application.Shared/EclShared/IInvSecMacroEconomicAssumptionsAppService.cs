using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IInvSecMacroEconomicAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvSecMacroEconomicAssumptionForViewDto>> GetAll(GetAllInvSecMacroEconomicAssumptionsInput input);

		Task<GetInvSecMacroEconomicAssumptionForEditOutput> GetInvSecMacroEconomicAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvSecMacroEconomicAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}