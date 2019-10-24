using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEclSicrApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclSicrApprovalForViewDto>> GetAll(GetAllWholesaleEclSicrApprovalsInput input);

		Task<GetWholesaleEclSicrApprovalForEditOutput> GetWholesaleEclSicrApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclSicrApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclSicrApprovalWholesaleEclSicrLookupTableDto>> GetAllWholesaleEclSicrForLookupTable(GetAllForLookupTableInput input);
		
    }
}