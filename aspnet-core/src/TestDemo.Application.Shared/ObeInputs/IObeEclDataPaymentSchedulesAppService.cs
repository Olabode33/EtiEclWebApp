using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeInputs
{
    public interface IObeEclDataPaymentSchedulesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclDataPaymentScheduleForViewDto>> GetAll(GetAllObeEclDataPaymentSchedulesInput input);

		Task<GetObeEclDataPaymentScheduleForEditOutput> GetObeEclDataPaymentScheduleForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclDataPaymentScheduleDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclDataPaymentScheduleObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}