using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IMacroeconomicVariablesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMacroeconomicVariableForViewDto>> GetAll(GetAllMacroeconomicVariablesInput input);

		Task<GetMacroeconomicVariableForEditOutput> GetMacroeconomicVariableForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditMacroeconomicVariableDto input);

		Task Delete(EntityDto input);

		
    }
}