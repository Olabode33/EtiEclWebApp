using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;
using TestDemo.Dto.Inputs;
using System.Collections.Generic;

namespace TestDemo.ObeInputs
{
    public interface IObeEclUploadsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclUploadForViewDto>> GetAll(GetAllObeEclUploadsInput input);

		Task<List<GetEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input);

		Task<GetObeEclUploadForEditOutput> GetObeEclUploadForEdit(EntityDto<Guid> input);

		Task<Guid> CreateOrEdit(CreateOrEditObeEclUploadDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclUploadObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}