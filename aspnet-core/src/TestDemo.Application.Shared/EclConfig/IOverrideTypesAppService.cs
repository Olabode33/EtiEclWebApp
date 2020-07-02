using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;


namespace TestDemo.EclConfig
{
    public interface IOverrideTypesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetOverrideTypeForViewDto>> GetAll(GetAllOverrideTypesInput input);

		Task<GetOverrideTypeForEditOutput> GetOverrideTypeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditOverrideTypeDto input);

		Task Delete(EntityDto input);

		
    }
}