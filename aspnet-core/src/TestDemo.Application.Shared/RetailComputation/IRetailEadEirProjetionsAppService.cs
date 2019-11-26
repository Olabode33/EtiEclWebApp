using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEadEirProjetionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEadEirProjetionForViewDto>> GetAll(GetAllRetailEadEirProjetionsInput input);

		Task<GetRetailEadEirProjetionForEditOutput> GetRetailEadEirProjetionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEadEirProjetionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEadEirProjetionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}