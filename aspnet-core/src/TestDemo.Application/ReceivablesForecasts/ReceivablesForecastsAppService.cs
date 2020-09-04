

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesForecasts.Exporting;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.ReceivablesForecasts
{
	[AbpAuthorize(AppPermissions.Pages_ReceivablesForecasts)]
    public class ReceivablesForecastsAppService : TestDemoAppServiceBase, IReceivablesForecastsAppService
    {
		 private readonly IRepository<ReceivablesForecast, Guid> _receivablesForecastRepository;
		 private readonly IReceivablesForecastsExcelExporter _receivablesForecastsExcelExporter;
        private readonly IInputPdCrDrExporter _inputDataExporter;


        public ReceivablesForecastsAppService(IRepository<ReceivablesForecast, Guid> receivablesForecastRepository, IReceivablesForecastsExcelExporter receivablesForecastsExcelExporter,
            IInputPdCrDrExporter inputDataExporter) 
		  {
			_receivablesForecastRepository = receivablesForecastRepository;
			_receivablesForecastsExcelExporter = receivablesForecastsExcelExporter;
            _inputDataExporter = inputDataExporter;


          }

		 public async Task<PagedResultDto<GetReceivablesForecastForViewDto>> GetAll(GetAllReceivablesForecastsInput input)
         {
			
			var filteredReceivablesForecasts = _receivablesForecastRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Period.Contains(input.Filter));

			var pagedAndFilteredReceivablesForecasts = filteredReceivablesForecasts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receivablesForecasts = from o in pagedAndFilteredReceivablesForecasts
                         select new GetReceivablesForecastForViewDto() {
							ReceivablesForecast = new ReceivablesForecastDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceivablesForecasts.CountAsync();

            return new PagedResultDto<GetReceivablesForecastForViewDto>(
                totalCount,
                await receivablesForecasts.ToListAsync()
            );
         }
		 
		 public async Task<GetReceivablesForecastForViewDto> GetReceivablesForecastForView(Guid id)
         {
            var receivablesForecast = await _receivablesForecastRepository.GetAsync(id);

            var output = new GetReceivablesForecastForViewDto { ReceivablesForecast = ObjectMapper.Map<ReceivablesForecastDto>(receivablesForecast) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ReceivablesForecasts_Edit)]
		 public async Task<GetReceivablesForecastForEditOutput> GetReceivablesForecastForEdit(EntityDto<Guid> input)
         {
            var receivablesForecast = await _receivablesForecastRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceivablesForecastForEditOutput {ReceivablesForecast = ObjectMapper.Map<CreateOrEditReceivablesForecastDto>(receivablesForecast)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceivablesForecastDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesForecasts_Create)]
		 protected virtual async Task Create(CreateOrEditReceivablesForecastDto input)
         {
            var receivablesForecast = ObjectMapper.Map<ReceivablesForecast>(input);

			

            await _receivablesForecastRepository.InsertAsync(receivablesForecast);
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesForecasts_Edit)]
		 protected virtual async Task Update(CreateOrEditReceivablesForecastDto input)
         {
            var receivablesForecast = await _receivablesForecastRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, receivablesForecast);
         }

		 [AbpAuthorize(AppPermissions.Pages_ReceivablesForecasts_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _receivablesForecastRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _receivablesForecastRepository.GetAll().Where(x => x.RegisterId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputReceivablesForecastDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }


    }
}