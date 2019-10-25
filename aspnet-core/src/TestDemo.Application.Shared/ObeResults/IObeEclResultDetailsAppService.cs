using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeResults
{
    public interface IObeEclResultDetailsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclResultDetailForViewDto>> GetAll(GetAllObeEclResultDetailsInput input);

		Task<GetObeEclResultDetailForEditOutput> GetObeEclResultDetailForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclResultDetailDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclResultDetailObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclResultDetailObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}