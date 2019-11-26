using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEadEirProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEadEirProjectionForViewDto>> GetAll(GetAllWholesaleEadEirProjectionsInput input);

		Task<GetWholesaleEadEirProjectionForEditOutput> GetWholesaleEadEirProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEadEirProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEadEirProjectionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}