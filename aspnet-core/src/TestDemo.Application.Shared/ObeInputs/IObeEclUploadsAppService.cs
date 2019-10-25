using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeInputs
{
    public interface IObeEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclUploadForViewDto>> GetAll(GetAllObeEclUploadsInput input);

		Task<GetObeEclUploadForEditOutput> GetObeEclUploadForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclUploadDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclUploadObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}