using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleLgdContractDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleLgdContractDataForViewDto>> GetAll(GetAllWholesaleLgdContractDatasInput input);

		Task<GetWholesaleLgdContractDataForEditOutput> GetWholesaleLgdContractDataForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleLgdContractDataDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleLgdContractDataWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}