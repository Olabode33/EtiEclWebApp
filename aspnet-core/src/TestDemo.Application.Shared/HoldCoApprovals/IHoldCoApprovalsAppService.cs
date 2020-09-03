using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.HoldCoApprovals.Dtos;
using TestDemo.Dto;


namespace TestDemo.HoldCoApprovals
{
    public interface IHoldCoApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHoldCoApprovalForViewDto>> GetAll(GetAllHoldCoApprovalsInput input);

        Task<GetHoldCoApprovalForViewDto> GetHoldCoApprovalForView(int id);

		Task<GetHoldCoApprovalForEditOutput> GetHoldCoApprovalForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditHoldCoApprovalDto input);

		Task Delete(EntityDto input);

		
    }
}