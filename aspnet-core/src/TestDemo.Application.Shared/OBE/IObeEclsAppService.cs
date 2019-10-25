using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.OBE.Dtos;
using TestDemo.Dto;

namespace TestDemo.OBE
{
    public interface IObeEclsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclForViewDto>> GetAll(GetAllObeEclsInput input);

		Task<GetObeEclForEditOutput> GetObeEclForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}