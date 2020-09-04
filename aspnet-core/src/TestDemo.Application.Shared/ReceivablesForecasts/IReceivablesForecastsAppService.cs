using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.Dto;


namespace TestDemo.ReceivablesForecasts
{
    public interface IReceivablesForecastsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetReceivablesForecastForViewDto>> GetAll(GetAllReceivablesForecastsInput input);

        Task<GetReceivablesForecastForViewDto> GetReceivablesForecastForView(Guid id);

		Task<GetReceivablesForecastForEditOutput> GetReceivablesForecastForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditReceivablesForecastDto input);

		Task Delete(EntityDto<Guid> input);
		
    }
}