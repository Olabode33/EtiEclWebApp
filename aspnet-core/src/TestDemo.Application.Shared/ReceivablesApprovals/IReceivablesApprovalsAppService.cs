using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesApprovals.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesApprovals
{
    public interface IReceivablesApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceivablesApprovalForViewDto>> GetAll(GetAllReceivablesApprovalsInput input);

        Task<GetReceivablesApprovalForViewDto> GetReceivablesApprovalForView(Guid id);

		Task<GetReceivablesApprovalForEditOutput> GetReceivablesApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditReceivablesApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}