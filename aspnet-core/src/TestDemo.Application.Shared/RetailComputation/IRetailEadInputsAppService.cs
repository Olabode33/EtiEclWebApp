using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEadInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEadInputForViewDto>> GetAll(GetAllRetailEadInputsInput input);

		Task<GetRetailEadInputForEditOutput> GetRetailEadInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEadInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEadInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}