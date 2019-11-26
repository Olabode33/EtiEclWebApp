using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeLgdContractDatasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeLgdContractDataForViewDto>> GetAll(GetAllObeLgdContractDatasInput input);

		Task<GetObeLgdContractDataForEditOutput> GetObeLgdContractDataForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeLgdContractDataDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeLgdContractDataObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}