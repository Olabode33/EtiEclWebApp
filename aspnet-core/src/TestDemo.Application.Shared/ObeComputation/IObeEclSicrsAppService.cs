using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEclSicrsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclSicrForViewDto>> GetAll(GetAllObeEclSicrsInput input);

		Task<GetObeEclSicrForEditOutput> GetObeEclSicrForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclSicrDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclSicrObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}