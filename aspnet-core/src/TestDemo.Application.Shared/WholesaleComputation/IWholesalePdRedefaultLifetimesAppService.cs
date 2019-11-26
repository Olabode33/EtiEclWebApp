using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdRedefaultLifetimesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdRedefaultLifetimeForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimesInput input);

		Task<GetWholesalePdRedefaultLifetimeForEditOutput> GetWholesalePdRedefaultLifetimeForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdRedefaultLifetimeWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}