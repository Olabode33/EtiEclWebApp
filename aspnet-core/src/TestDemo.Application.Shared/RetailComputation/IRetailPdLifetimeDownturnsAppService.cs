using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdLifetimeDownturnForViewDto>> GetAll(GetAllRetailPdLifetimeDownturnsInput input);

		Task<GetRetailPdLifetimeDownturnForEditOutput> GetRetailPdLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdLifetimeDownturnRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}