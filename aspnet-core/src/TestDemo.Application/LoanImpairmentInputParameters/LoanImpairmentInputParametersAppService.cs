

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentInputParameters.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.LoanImpairmentInputParameters
{
	//[AbpAuthorize(AppPermissions.Pages_LoanImpairmentInputParameters)]
    public class LoanImpairmentInputParametersAppService : TestDemoAppServiceBase, ILoanImpairmentInputParametersAppService
    {
		 private readonly IRepository<LoanImpairmentInputParameter, Guid> _loanImpairmentInputParameterRepository;
		 

		  public LoanImpairmentInputParametersAppService(IRepository<LoanImpairmentInputParameter, Guid> loanImpairmentInputParameterRepository ) 
		  {
			_loanImpairmentInputParameterRepository = loanImpairmentInputParameterRepository;
			
		  }

		 public async Task<PagedResultDto<GetLoanImpairmentInputParameterForViewDto>> GetAll(GetAllLoanImpairmentInputParametersInput input)
         {
			
			var filteredLoanImpairmentInputParameters = _loanImpairmentInputParameterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredLoanImpairmentInputParameters = filteredLoanImpairmentInputParameters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentInputParameters = from o in pagedAndFilteredLoanImpairmentInputParameters
                         select new GetLoanImpairmentInputParameterForViewDto() {
							LoanImpairmentInputParameter = new LoanImpairmentInputParameterDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentInputParameters.CountAsync();

            return new PagedResultDto<GetLoanImpairmentInputParameterForViewDto>(
                totalCount,
                await loanImpairmentInputParameters.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentInputParameterForViewDto> GetLoanImpairmentInputParameterForView(Guid id)
         {
            var loanImpairmentInputParameter = await _loanImpairmentInputParameterRepository.GetAsync(id);

            var output = new GetLoanImpairmentInputParameterForViewDto { LoanImpairmentInputParameter = ObjectMapper.Map<LoanImpairmentInputParameterDto>(loanImpairmentInputParameter) };
			
            return output;
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentInputParameters_Edit)]
		 public async Task<GetLoanImpairmentInputParameterForEditOutput> GetLoanImpairmentInputParameterForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentInputParameter = await _loanImpairmentInputParameterRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentInputParameterForEditOutput {LoanImpairmentInputParameter = ObjectMapper.Map<CreateOrEditLoanImpairmentInputParameterDto>(loanImpairmentInputParameter)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentInputParameterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentInputParameters_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentInputParameterDto input)
         {
            var loanImpairmentInputParameter = ObjectMapper.Map<LoanImpairmentInputParameter>(input);

			

            await _loanImpairmentInputParameterRepository.InsertAsync(loanImpairmentInputParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentInputParameters_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentInputParameterDto input)
         {
            var loanImpairmentInputParameter = await _loanImpairmentInputParameterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentInputParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_LoanImpairmentInputParameters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentInputParameterRepository.DeleteAsync(input.Id);
         } 
    }
}