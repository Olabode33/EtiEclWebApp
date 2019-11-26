using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleLgdCollateralTypeDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleLgdCollateralTypeDataForViewDto>> GetAll(GetAllWholesaleLgdCollateralTypeDatasInput input);

		Task<GetWholesaleLgdCollateralTypeDataForEditOutput> GetWholesaleLgdCollateralTypeDataForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleLgdCollateralTypeDataDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleLgdCollateralTypeDataWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}