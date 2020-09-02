

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.HoldCoResult
{
	[AbpAuthorize(AppPermissions.Pages_ResultSummaryByStages)]
    public class ResultSummaryByStagesAppService : TestDemoAppServiceBase, IResultSummaryByStagesAppService
    {
		 private readonly IRepository<ResultSummaryByStage, Guid> _resultSummaryByStageRepository;
        private readonly IInputPdCrDrExporter _inputDataExporter;

        public ResultSummaryByStagesAppService(IRepository<ResultSummaryByStage, Guid> resultSummaryByStageRepository , IInputPdCrDrExporter inputDataExporter) 
		  {
			_resultSummaryByStageRepository = resultSummaryByStageRepository;
            _inputDataExporter = inputDataExporter;
        }

        public async Task<List<CreateOrEditResultSummaryByStageDto>> GetResults(Guid id)
        {
            return ObjectMapper.Map<List<CreateOrEditResultSummaryByStageDto>>(await _resultSummaryByStageRepository.GetAll().Where(a => a.RegistrationId == id).ToListAsync());

        }

        public async Task<PagedResultDto<GetResultSummaryByStageForViewDto>> GetAll(GetAllResultSummaryByStagesInput input)
         {
			
			var filteredResultSummaryByStages = _resultSummaryByStageRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredResultSummaryByStages = filteredResultSummaryByStages
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var resultSummaryByStages = from o in pagedAndFilteredResultSummaryByStages
                         select new GetResultSummaryByStageForViewDto() {
							ResultSummaryByStage = new ResultSummaryByStageDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredResultSummaryByStages.CountAsync();

            return new PagedResultDto<GetResultSummaryByStageForViewDto>(
                totalCount,
                await resultSummaryByStages.ToListAsync()
            );
         }
		 
		 public async Task<GetResultSummaryByStageForViewDto> GetResultSummaryByStageForView(Guid id)
         {
            var resultSummaryByStage = await _resultSummaryByStageRepository.GetAsync(id);

            var output = new GetResultSummaryByStageForViewDto { ResultSummaryByStage = ObjectMapper.Map<ResultSummaryByStageDto>(resultSummaryByStage) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ResultSummaryByStages_Edit)]
		 public async Task<GetResultSummaryByStageForEditOutput> GetResultSummaryByStageForEdit(EntityDto<Guid> input)
         {
            var resultSummaryByStage = await _resultSummaryByStageRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetResultSummaryByStageForEditOutput {ResultSummaryByStage = ObjectMapper.Map<CreateOrEditResultSummaryByStageDto>(resultSummaryByStage)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditResultSummaryByStageDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ResultSummaryByStages_Create)]
		 protected virtual async Task Create(CreateOrEditResultSummaryByStageDto input)
         {
            var resultSummaryByStage = ObjectMapper.Map<ResultSummaryByStage>(input);

			

            await _resultSummaryByStageRepository.InsertAsync(resultSummaryByStage);
         }

		 [AbpAuthorize(AppPermissions.Pages_ResultSummaryByStages_Edit)]
		 protected virtual async Task Update(CreateOrEditResultSummaryByStageDto input)
         {
            var resultSummaryByStage = await _resultSummaryByStageRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, resultSummaryByStage);
         }

		 [AbpAuthorize(AppPermissions.Pages_ResultSummaryByStages_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _resultSummaryByStageRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _resultSummaryByStageRepository.GetAll().Where(x => x.RegistrationId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputResultSummaryByStageDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }
    }
}