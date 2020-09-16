

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentModelResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.LoanImpairmentModelResults
{
	//[AbpAuthorize(AppPermissions.Pages_LoanImpairmentModelResults)]
    public class LoanImpairmentModelResultsAppService : TestDemoAppServiceBase, ILoanImpairmentModelResultsAppService
    {
		 private readonly IRepository<LoanImpairmentModelResult, Guid> _loanImpairmentModelResultRepository;
        private readonly IInputPdCrDrExporter _inputDataExporter;


        public LoanImpairmentModelResultsAppService(IRepository<LoanImpairmentModelResult, Guid> loanImpairmentModelResultRepository, IInputPdCrDrExporter inputDataExporter) 
		  {
			_loanImpairmentModelResultRepository = loanImpairmentModelResultRepository;
            _inputDataExporter = inputDataExporter;

        }

        public async Task<PagedResultDto<GetLoanImpairmentModelResultForViewDto>> GetAll(GetAllLoanImpairmentModelResultsInput input)
         {
			
			var filteredLoanImpairmentModelResults = _loanImpairmentModelResultRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredLoanImpairmentModelResults = filteredLoanImpairmentModelResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentModelResults = from o in pagedAndFilteredLoanImpairmentModelResults
                         select new GetLoanImpairmentModelResultForViewDto() {
							LoanImpairmentModelResult = new LoanImpairmentModelResultDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentModelResults.CountAsync();

            return new PagedResultDto<GetLoanImpairmentModelResultForViewDto>(
                totalCount,
                await loanImpairmentModelResults.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentModelResultForViewDto> GetLoanImpairmentModelResultForView(Guid id)
         {
            var loanImpairmentModelResult = await _loanImpairmentModelResultRepository.GetAsync(id);

            var output = new GetLoanImpairmentModelResultForViewDto { LoanImpairmentModelResult = ObjectMapper.Map<LoanImpairmentModelResultDto>(loanImpairmentModelResult) };
			
            return output;
         }


        public async Task<List<CreateOrEditLoanImpairmentModelResultDto>> GetResults(Guid id)
        {
            return ObjectMapper.Map<List<CreateOrEditLoanImpairmentModelResultDto>>(await _loanImpairmentModelResultRepository.GetAll().Where(a => a.RegisterId == id).ToListAsync());

        }

        //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentModelResults_Edit)]
		 public async Task<GetLoanImpairmentModelResultForEditOutput> GetLoanImpairmentModelResultForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentModelResult = await _loanImpairmentModelResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentModelResultForEditOutput {LoanImpairmentModelResult = ObjectMapper.Map<CreateOrEditLoanImpairmentModelResultDto>(loanImpairmentModelResult)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentModelResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentModelResults_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentModelResultDto input)
         {
            var loanImpairmentModelResult = ObjectMapper.Map<LoanImpairmentModelResult>(input);

			

            await _loanImpairmentModelResultRepository.InsertAsync(loanImpairmentModelResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentModelResults_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentModelResultDto input)
         {
            var loanImpairmentModelResult = await _loanImpairmentModelResultRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentModelResult);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentModelResults_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentModelResultRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _loanImpairmentModelResultRepository.GetAll().Where(x => x.RegisterId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputLoanImpairmentModelResultDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }
    }
}