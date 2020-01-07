using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclConfig
{
    public interface IEclConfigurationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEclConfigurationForViewDto>> GetAll(GetAllEclConfigurationsInput input);

		Task<GetEclConfigurationForEditOutput> GetEclConfigurationForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditEclConfigurationDto input);

		Task Delete(EntityDto input);

		
    }
}