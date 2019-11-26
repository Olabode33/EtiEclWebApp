using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdLifetimeBestsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdLifetimeBestForViewDto>> GetAll(GetAllWholesalePdLifetimeBestsInput input);

		Task<GetWholesalePdLifetimeBestForEditOutput> GetWholesalePdLifetimeBestForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdLifetimeBestDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdLifetimeBestWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}