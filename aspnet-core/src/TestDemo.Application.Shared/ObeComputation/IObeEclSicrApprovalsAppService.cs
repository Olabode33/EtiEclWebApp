using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEclSicrApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclSicrApprovalForViewDto>> GetAll(GetAllObeEclSicrApprovalsInput input);

		Task<GetObeEclSicrApprovalForEditOutput> GetObeEclSicrApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclSicrApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclSicrApprovalObeEclSicrLookupTableDto>> GetAllObeEclSicrForLookupTable(GetAllForLookupTableInput input);
		
    }
}