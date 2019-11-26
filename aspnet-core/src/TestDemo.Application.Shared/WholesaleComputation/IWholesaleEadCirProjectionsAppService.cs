using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEadCirProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEadCirProjectionForViewDto>> GetAll(GetAllWholesaleEadCirProjectionsInput input);

		Task<GetWholesaleEadCirProjectionForEditOutput> GetWholesaleEadCirProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEadCirProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEadCirProjectionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}