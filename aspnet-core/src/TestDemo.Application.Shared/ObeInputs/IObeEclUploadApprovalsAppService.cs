using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeInputs
{
    public interface IObeEclUploadApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclUploadApprovalForViewDto>> GetAll(GetAllObeEclUploadApprovalsInput input);

		Task<GetObeEclUploadApprovalForEditOutput> GetObeEclUploadApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclUploadApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclUploadApprovalObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}