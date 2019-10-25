using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailInputs
{
    public interface IRetailEclDataPaymentSchedulesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclDataPaymentScheduleForViewDto>> GetAll(GetAllRetailEclDataPaymentSchedulesInput input);

		Task<GetRetailEclDataPaymentScheduleForEditOutput> GetRetailEclDataPaymentScheduleForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclDataPaymentScheduleDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclDataPaymentScheduleRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}