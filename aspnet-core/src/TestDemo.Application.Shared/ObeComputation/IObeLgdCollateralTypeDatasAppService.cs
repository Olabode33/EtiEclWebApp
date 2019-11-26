using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeLgdCollateralTypeDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeLgdCollateralTypeDataForViewDto>> GetAll(GetAllObeLgdCollateralTypeDatasInput input);

		Task<GetObeLgdCollateralTypeDataForEditOutput> GetObeLgdCollateralTypeDataForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeLgdCollateralTypeDataDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeLgdCollateralTypeDataObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}