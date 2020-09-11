using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LoanImpairmentApprovals.Dtos;
using TestDemo.Dto;


namespace TestDemo.LoanImpairmentApprovals
{
    public interface ILoanImpairmentApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetLoanImpairmentApprovalForViewDto>> GetAll(GetAllLoanImpairmentApprovalsInput input);

        Task<GetLoanImpairmentApprovalForViewDto> GetLoanImpairmentApprovalForView(Guid id);

		Task<GetLoanImpairmentApprovalForEditOutput> GetLoanImpairmentApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditLoanImpairmentApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}