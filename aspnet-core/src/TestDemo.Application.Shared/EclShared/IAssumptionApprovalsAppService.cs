using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IAssumptionApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAssumptionApprovalForViewDto>> GetAll(GetAllAssumptionApprovalsInput input);

        Task<GetAssumptionApprovalForViewDto> GetAssumptionApprovalForView(Guid id);

		Task<GetAssumptionApprovalForEditOutput> GetAssumptionApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditAssumptionApprovalDto input);

		Task Delete(EntityDto<Guid> input);

        Task ApproveReject(AssumptionApprovalDto input);

        Task<PagedResultDto<AssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}