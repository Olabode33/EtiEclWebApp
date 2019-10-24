using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclAssumptionApprovalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclAssumptionApprovalForViewDto>> GetAll(GetAllWholesaleEclAssumptionApprovalsInput input);

		Task<GetWholesaleEclAssumptionApprovalForEditOutput> GetWholesaleEclAssumptionApprovalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclAssumptionApprovalDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclAssumptionApprovalWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclAssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input);
		
    }
}