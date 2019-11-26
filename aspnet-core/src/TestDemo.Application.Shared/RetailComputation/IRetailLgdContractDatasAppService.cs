using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailLgdContractDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailLgdContractDataForViewDto>> GetAll(GetAllRetailLgdContractDatasInput input);

		Task<GetRetailLgdContractDataForEditOutput> GetRetailLgdContractDataForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailLgdContractDataDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailLgdContractDataRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}