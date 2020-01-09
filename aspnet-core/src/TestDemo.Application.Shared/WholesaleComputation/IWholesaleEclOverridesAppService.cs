using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclOverrideForViewDto>> GetAll(GetAllWholesaleEclOverridesInput input);

		Task<GetWholesaleEclOverrideForEditOutput> GetWholesaleEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclOverrideWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}