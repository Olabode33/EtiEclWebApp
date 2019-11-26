using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdMappingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdMappingForViewDto>> GetAll(GetAllObePdMappingsInput input);

		Task<GetObePdMappingForEditOutput> GetObePdMappingForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdMappingDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdMappingObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}