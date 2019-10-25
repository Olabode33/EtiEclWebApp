using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;

namespace TestDemo.Retail
{
    public interface IRetailEclApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclApprovalForViewDto>> GetAll(GetAllRetailEclApprovalsInput input);

		Task<GetRetailEclApprovalForEditOutput> GetRetailEclApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclApprovalRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}