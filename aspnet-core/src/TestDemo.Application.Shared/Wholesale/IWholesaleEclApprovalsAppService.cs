using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.Wholesale.Dtos;
using TestDemo.Dto;

namespace TestDemo.Wholesale
{
    public interface IWholesaleEclApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclApprovalForViewDto>> GetAll(GetAllWholesaleEclApprovalsInput input);

		Task<GetWholesaleEclApprovalForEditOutput> GetWholesaleEclApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclApprovalWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}