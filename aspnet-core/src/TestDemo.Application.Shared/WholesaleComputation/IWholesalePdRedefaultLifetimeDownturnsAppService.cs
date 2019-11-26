using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdRedefaultLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimeDownturnsInput input);

		Task<GetWholesalePdRedefaultLifetimeDownturnForEditOutput> GetWholesalePdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdRedefaultLifetimeDownturnWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}