using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdMappingsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdMappingForViewDto>> GetAll(GetAllWholesalePdMappingsInput input);

		Task<GetWholesalePdMappingForEditOutput> GetWholesalePdMappingForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdMappingDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdMappingWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}