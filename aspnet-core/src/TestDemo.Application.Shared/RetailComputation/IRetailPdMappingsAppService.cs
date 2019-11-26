using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdMappingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdMappingForViewDto>> GetAll(GetAllRetailPdMappingsInput input);

		Task<GetRetailPdMappingForEditOutput> GetRetailPdMappingForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdMappingDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdMappingRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}