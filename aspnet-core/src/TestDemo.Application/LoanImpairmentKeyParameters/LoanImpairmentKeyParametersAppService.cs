

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Exporting;

namespace TestDemo.LoanImpairmentKeyParameters
{
	//[AbpAuthorize(AppPermissions.Pages_LoanImpairmentKeyParameters)]
    public class LoanImpairmentKeyParametersAppService : TestDemoAppServiceBase, ILoanImpairmentKeyParametersAppService
    {
		 private readonly IRepository<LoanImpairmentKeyParameter, Guid> _loanImpairmentKeyParameterRepository;
        private readonly IInputPdCrDrExporter _inputDataExporter;


        public LoanImpairmentKeyParametersAppService(IRepository<LoanImpairmentKeyParameter, Guid> loanImpairmentKeyParameterRepository, IInputPdCrDrExporter inputDataExporter) 
		  {
			_loanImpairmentKeyParameterRepository = loanImpairmentKeyParameterRepository;
            _inputDataExporter = inputDataExporter;

        }

        public async Task<PagedResultDto<GetLoanImpairmentKeyParameterForViewDto>> GetAll(GetAllLoanImpairmentKeyParametersInput input)
         {
			
			var filteredLoanImpairmentKeyParameters = _loanImpairmentKeyParameterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredLoanImpairmentKeyParameters = filteredLoanImpairmentKeyParameters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentKeyParameters = from o in pagedAndFilteredLoanImpairmentKeyParameters
                         select new GetLoanImpairmentKeyParameterForViewDto() {
							LoanImpairmentKeyParameter = new LoanImpairmentKeyParameterDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentKeyParameters.CountAsync();

            return new PagedResultDto<GetLoanImpairmentKeyParameterForViewDto>(
                totalCount,
                await loanImpairmentKeyParameters.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentKeyParameterForViewDto> GetLoanImpairmentKeyParameterForView(Guid id)
         {
            var loanImpairmentKeyParameter = await _loanImpairmentKeyParameterRepository.GetAsync(id);

            var output = new GetLoanImpairmentKeyParameterForViewDto { LoanImpairmentKeyParameter = ObjectMapper.Map<LoanImpairmentKeyParameterDto>(loanImpairmentKeyParameter) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentKeyParameters_Edit)]
		 public async Task<GetLoanImpairmentKeyParameterForEditOutput> GetLoanImpairmentKeyParameterForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentKeyParameter = await _loanImpairmentKeyParameterRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentKeyParameterForEditOutput {LoanImpairmentKeyParameter = ObjectMapper.Map<CreateOrEditLoanImpairmentKeyParameterDto>(loanImpairmentKeyParameter)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentKeyParameterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentKeyParameters_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentKeyParameterDto input)
         {
            var loanImpairmentKeyParameter = ObjectMapper.Map<LoanImpairmentKeyParameter>(input);

			

            await _loanImpairmentKeyParameterRepository.InsertAsync(loanImpairmentKeyParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentKeyParameters_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentKeyParameterDto input)
         {
            var loanImpairmentKeyParameter = await _loanImpairmentKeyParameterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentKeyParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentKeyParameters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentKeyParameterRepository.DeleteAsync(input.Id);
         }

        public async Task<FileDto> ExportToExcel(EntityDto<Guid> input)
        {

            var items = await _loanImpairmentKeyParameterRepository.GetAll().Where(x => x.RegisterId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputLoanImpairmentKeyParameterDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }
    }
}