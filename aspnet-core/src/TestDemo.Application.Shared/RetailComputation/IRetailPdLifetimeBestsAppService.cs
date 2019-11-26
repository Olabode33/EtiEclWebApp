using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdLifetimeBestsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdLifetimeBestForViewDto>> GetAll(GetAllRetailPdLifetimeBestsInput input);

		Task<GetRetailPdLifetimeBestForEditOutput> GetRetailPdLifetimeBestForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdLifetimeBestDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdLifetimeBestRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}