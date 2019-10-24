using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleInputs
{
    public interface IWholesaleEclDataPaymentSchedulesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclDataPaymentScheduleForViewDto>> GetAll(GetAllWholesaleEclDataPaymentSchedulesInput input);

		Task<GetWholesaleEclDataPaymentScheduleForEditOutput> GetWholesaleEclDataPaymentScheduleForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclDataPaymentScheduleDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input);
		
    }
}