

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesCurrentPeriodDates.Exporting;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ReceivablesCurrentPeriodDates
{
	[AbpAuthorize(AppPermissions.Pages_CurrentPeriodDates)]
    public class CurrentPeriodDatesAppService : TestDemoAppServiceBase, ICurrentPeriodDatesAppService
    {
		 private readonly IRepository<CurrentPeriodDate, Guid> _currentPeriodDateRepository;
		 private readonly ICurrentPeriodDatesExcelExporter _currentPeriodDatesExcelExporter;
		 

		  public CurrentPeriodDatesAppService(IRepository<CurrentPeriodDate, Guid> currentPeriodDateRepository, ICurrentPeriodDatesExcelExporter currentPeriodDatesExcelExporter ) 
		  {
			_currentPeriodDateRepository = currentPeriodDateRepository;
			_currentPeriodDatesExcelExporter = currentPeriodDatesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetCurrentPeriodDateForViewDto>> GetAll(GetAllCurrentPeriodDatesInput input)
         {
			
			var filteredCurrentPeriodDates = _currentPeriodDateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Account.Contains(input.Filter));

			var pagedAndFilteredCurrentPeriodDates = filteredCurrentPeriodDates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var currentPeriodDates = from o in pagedAndFilteredCurrentPeriodDates
                         select new GetCurrentPeriodDateForViewDto() {
							CurrentPeriodDate = new CurrentPeriodDateDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredCurrentPeriodDates.CountAsync();

            return new PagedResultDto<GetCurrentPeriodDateForViewDto>(
                totalCount,
                await currentPeriodDates.ToListAsync()
            );
         }
		 
		 public async Task<GetCurrentPeriodDateForViewDto> GetCurrentPeriodDateForView(Guid id)
         {
            var currentPeriodDate = await _currentPeriodDateRepository.GetAsync(id);

            var output = new GetCurrentPeriodDateForViewDto { CurrentPeriodDate = ObjectMapper.Map<CurrentPeriodDateDto>(currentPeriodDate) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_CurrentPeriodDates_Edit)]
		 public async Task<GetCurrentPeriodDateForEditOutput> GetCurrentPeriodDateForEdit(EntityDto<Guid> input)
         {
            var currentPeriodDate = await _currentPeriodDateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCurrentPeriodDateForEditOutput {CurrentPeriodDate = ObjectMapper.Map<CreateOrEditCurrentPeriodDateDto>(currentPeriodDate)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCurrentPeriodDateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_CurrentPeriodDates_Create)]
		 protected virtual async Task Create(CreateOrEditCurrentPeriodDateDto input)
         {
            var currentPeriodDate = ObjectMapper.Map<CurrentPeriodDate>(input);

			

            await _currentPeriodDateRepository.InsertAsync(currentPeriodDate);
         }

		 [AbpAuthorize(AppPermissions.Pages_CurrentPeriodDates_Edit)]
		 protected virtual async Task Update(CreateOrEditCurrentPeriodDateDto input)
         {
            var currentPeriodDate = await _currentPeriodDateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, currentPeriodDate);
         }

		 [AbpAuthorize(AppPermissions.Pages_CurrentPeriodDates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _currentPeriodDateRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetCurrentPeriodDatesToExcel(GetAllCurrentPeriodDatesForExcelInput input)
         {
			
			var filteredCurrentPeriodDates = _currentPeriodDateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Account.Contains(input.Filter));

			var query = (from o in filteredCurrentPeriodDates
                         select new GetCurrentPeriodDateForViewDto() { 
							CurrentPeriodDate = new CurrentPeriodDateDto
							{
                                Id = o.Id
							}
						 });


            var currentPeriodDateListDtos = await query.ToListAsync();

            return _currentPeriodDatesExcelExporter.ExportToFile(currentPeriodDateListDtos);
         }


    }
}