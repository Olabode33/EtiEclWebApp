using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;


namespace TestDemo.IVModels
{
    public interface IHoldCoInputParametersAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHoldCoInputParameterForViewDto>> GetAll(GetAllHoldCoInputParametersInput input);

		Task<GetHoldCoInputParameterForEditOutput> GetHoldCoInputParameterForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditHoldCoInputParameterDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}