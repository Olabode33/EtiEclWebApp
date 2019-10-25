using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.OBE.Dtos;
using TestDemo.Dto;

namespace TestDemo.OBE
{
    public interface IObeEclApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclApprovalForViewDto>> GetAll(GetAllObeEclApprovalsInput input);

		Task<GetObeEclApprovalForEditOutput> GetObeEclApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclApprovalObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}