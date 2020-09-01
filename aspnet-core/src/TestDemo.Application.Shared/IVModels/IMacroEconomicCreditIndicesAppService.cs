using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;


namespace TestDemo.IVModels
{
    public interface IMacroEconomicCreditIndicesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMacroEconomicCreditIndexForViewDto>> GetAll(GetAllMacroEconomicCreditIndicesInput input);

        Task<GetMacroEconomicCreditIndexForViewDto> GetMacroEconomicCreditIndexForView(Guid id);

		Task<GetMacroEconomicCreditIndexForEditOutput> GetMacroEconomicCreditIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditMacroEconomicCreditIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}