using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclAssumptionApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclAssumptionApprovalForViewDto>> GetAll(GetAllObeEclAssumptionApprovalsInput input);

		Task<GetObeEclAssumptionApprovalForEditOutput> GetObeEclAssumptionApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclAssumptionApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclAssumptionApprovalObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclAssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}