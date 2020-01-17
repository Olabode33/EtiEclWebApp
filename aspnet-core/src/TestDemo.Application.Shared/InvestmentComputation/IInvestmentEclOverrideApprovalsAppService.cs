using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentComputation
{
    public interface IInvestmentEclOverrideApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclOverrideApprovalForViewDto>> GetAll(GetAllInvestmentEclOverrideApprovalsInput input);

		Task<GetInvestmentEclOverrideApprovalForEditOutput> GetInvestmentEclOverrideApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclOverrideApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclOverrideApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<InvestmentEclOverrideApprovalInvestmentEclOverrideLookupTableDto>> GetAllInvestmentEclOverrideForLookupTable(GetAllForLookupTableInput input);
		
    }
}