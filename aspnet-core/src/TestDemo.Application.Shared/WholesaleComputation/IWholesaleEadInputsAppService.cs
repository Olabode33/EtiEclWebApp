using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEadInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEadInputForViewDto>> GetAll(GetAllWholesaleEadInputsInput input);

		Task<GetWholesaleEadInputForEditOutput> GetWholesaleEadInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEadInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEadInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}