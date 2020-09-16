

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ReceivablesResults.Exporting;
using TestDemo.ReceivablesResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.ReceivablesResults
{
	//[AbpAuthorize(AppPermissions.Pages_ReceivablesResults)]
    public class ReceivablesResultsAppService : TestDemoAppServiceBase, IReceivablesResultsAppService
    {
	    private readonly IRepository<ReceivablesResult, Guid> _receivablesResultRepository;
		private readonly IReceivablesResultsExcelExporter _receivablesResultsExcelExporter;
        private readonly IInputPdCrDrExporter _inputDataExporter;


        public ReceivablesResultsAppService(IRepository<ReceivablesResult, Guid> receivablesResultRepository, IReceivablesResultsExcelExporter receivablesResultsExcelExporter,
            IInputPdCrDrExporter inputDataExporter) 
		  {
			_receivablesResultRepository = receivablesResultRepository;
			_receivablesResultsExcelExporter = receivablesResultsExcelExporter;
            _inputDataExporter = inputDataExporter;

          }

		 public async Task<PagedResultDto<GetReceivablesResultForViewDto>> GetAll(GetAllReceivablesResultsInput input)
         {
			
			var filteredReceivablesResults = _receivablesResultRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredReceivablesResults = filteredReceivablesResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var receivablesResults = from o in pagedAndFilteredReceivablesResults
                         select new GetReceivablesResultForViewDto() {
							ReceivablesResult = new ReceivablesResultDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredReceivablesResults.CountAsync();

            return new PagedResultDto<GetReceivablesResultForViewDto>(
                totalCount,
                await receivablesResults.ToListAsync()
            );
         }

		public async Task<List<CreateOrEditReceivablesResultDto>> GetResults(Guid id)
		{
			return ObjectMapper.Map<List<CreateOrEditReceivablesResultDto>>(await _receivablesResultRepository.GetAll().Where(a => a.RegisterId == id).ToListAsync());

		}

		public async Task<GetReceivablesResultForViewDto> GetReceivablesResultForView(Guid id)
         {
            var receivablesResult = await _receivablesResultRepository.GetAsync(id);

            var output = new GetReceivablesResultForViewDto { ReceivablesResult = ObjectMapper.Map<ReceivablesResultDto>(receivablesResult) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesResults_Edit)]
		 public async Task<GetReceivablesResultForEditOutput> GetReceivablesResultForEdit(EntityDto<Guid> input)
         {
            var receivablesResult = await _receivablesResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetReceivablesResultForEditOutput {ReceivablesResult = ObjectMapper.Map<CreateOrEditReceivablesResultDto>(receivablesResult)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditReceivablesResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesResults_Create)]
		 protected virtual async Task Create(CreateOrEditReceivablesResultDto input)
         {
            var receivablesResult = ObjectMapper.Map<ReceivablesResult>(input);

			

            await _receivablesResultRepository.InsertAsync(receivablesResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesResults_Edit)]
		 protected virtual async Task Update(CreateOrEditReceivablesResultDto input)
         {
            var receivablesResult = await _receivablesResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, receivablesResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_ReceivablesResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _receivablesResultRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _receivablesResultRepository.GetAll().Where(x => x.RegisterId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputReceivablesResultDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }


    }
}