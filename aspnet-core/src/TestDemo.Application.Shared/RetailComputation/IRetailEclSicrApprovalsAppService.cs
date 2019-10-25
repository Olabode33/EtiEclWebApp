using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEclSicrApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclSicrApprovalForViewDto>> GetAll(GetAllRetailEclSicrApprovalsInput input);

		Task<GetRetailEclSicrApprovalForEditOutput> GetRetailEclSicrApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclSicrApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclSicrApprovalRetailEclSicrLookupTableDto>> GetAllRetailEclSicrForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}