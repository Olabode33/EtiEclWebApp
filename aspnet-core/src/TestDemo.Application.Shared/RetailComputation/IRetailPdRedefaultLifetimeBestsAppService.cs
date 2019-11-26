using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdRedefaultLifetimeBestsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdRedefaultLifetimeBestForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeBestsInput input);

		Task<GetRetailPdRedefaultLifetimeBestForEditOutput> GetRetailPdRedefaultLifetimeBestForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeBestDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdRedefaultLifetimeBestRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}