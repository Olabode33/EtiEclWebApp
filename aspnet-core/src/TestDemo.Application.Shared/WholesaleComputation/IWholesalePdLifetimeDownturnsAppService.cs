using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdLifetimeDownturnForViewDto>> GetAll(GetAllWholesalePdLifetimeDownturnsInput input);

		Task<GetWholesalePdLifetimeDownturnForEditOutput> GetWholesalePdLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdLifetimeDownturnWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}