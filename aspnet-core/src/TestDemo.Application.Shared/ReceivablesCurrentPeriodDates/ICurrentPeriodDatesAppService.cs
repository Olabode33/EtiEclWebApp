using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesCurrentPeriodDates
{
    public interface ICurrentPeriodDatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCurrentPeriodDateForViewDto>> GetAll(GetAllCurrentPeriodDatesInput input);

        Task<GetCurrentPeriodDateForViewDto> GetCurrentPeriodDateForView(Guid id);

		Task<GetCurrentPeriodDateForEditOutput> GetCurrentPeriodDateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditCurrentPeriodDateDto input);

		Task Delete(EntityDto<Guid> input);

		Task<FileDto> GetCurrentPeriodDatesToExcel(GetAllCurrentPeriodDatesForExcelInput input);

		
    }
}