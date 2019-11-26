using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEadCirProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEadCirProjectionForViewDto>> GetAll(GetAllRetailEadCirProjectionsInput input);

		Task<GetRetailEadCirProjectionForEditOutput> GetRetailEadCirProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEadCirProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEadCirProjectionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}