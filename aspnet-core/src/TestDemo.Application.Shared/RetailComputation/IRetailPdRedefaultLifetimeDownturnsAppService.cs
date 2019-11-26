using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdRedefaultLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeDownturnsInput input);

		Task<GetRetailPdRedefaultLifetimeDownturnForEditOutput> GetRetailPdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdRedefaultLifetimeDownturnRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}