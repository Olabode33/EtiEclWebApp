using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Investment.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;

namespace TestDemo.Investment
{
    public interface IInvestmentEclApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclApprovalForViewDto>> GetAll(GetAllInvestmentEclApprovalsInput input);

		Task<GetInvestmentEclApprovalForEditOutput> GetInvestmentEclApprovalForEdit(EntityDto<Guid> input);

		Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<InvestmentEclApprovalInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}