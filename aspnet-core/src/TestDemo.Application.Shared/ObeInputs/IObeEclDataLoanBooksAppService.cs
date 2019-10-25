using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeInputs
{
    public interface IObeEclDataLoanBooksAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclDataLoanBookForViewDto>> GetAll(GetAllObeEclDataLoanBooksInput input);

		Task<GetObeEclDataLoanBookForEditOutput> GetObeEclDataLoanBookForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclDataLoanBookDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclDataLoanBookObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}