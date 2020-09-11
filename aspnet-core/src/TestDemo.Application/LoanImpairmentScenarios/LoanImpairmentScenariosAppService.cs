

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.LoanImpairmentScenarios
{
	[AbpAuthorize(AppPermissions.Pages_LoanImpairmentScenarios)]
    public class LoanImpairmentScenariosAppService : TestDemoAppServiceBase, ILoanImpairmentScenariosAppService
    {
		 private readonly IRepository<LoanImpairmentScenario, Guid> _loanImpairmentScenarioRepository;
		 

		  public LoanImpairmentScenariosAppService(IRepository<LoanImpairmentScenario, Guid> loanImpairmentScenarioRepository ) 
		  {
			_loanImpairmentScenarioRepository = loanImpairmentScenarioRepository;
			
		  }

		 public async Task<PagedResultDto<GetLoanImpairmentScenarioForViewDto>> GetAll(GetAllLoanImpairmentScenariosInput input)
         {
			
			var filteredLoanImpairmentScenarios = _loanImpairmentScenarioRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ScenarioOption.Contains(input.Filter) || e.ApplyOverridesBaseScenario.Contains(input.Filter) || e.ApplyOverridesOptimisticScenario.Contains(input.Filter) || e.ApplyOverridesDownturnScenario.Contains(input.Filter));

			var pagedAndFilteredLoanImpairmentScenarios = filteredLoanImpairmentScenarios
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var loanImpairmentScenarios = from o in pagedAndFilteredLoanImpairmentScenarios
                         select new GetLoanImpairmentScenarioForViewDto() {
							LoanImpairmentScenario = new LoanImpairmentScenarioDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredLoanImpairmentScenarios.CountAsync();

            return new PagedResultDto<GetLoanImpairmentScenarioForViewDto>(
                totalCount,
                await loanImpairmentScenarios.ToListAsync()
            );
         }
		 
		 public async Task<GetLoanImpairmentScenarioForViewDto> GetLoanImpairmentScenarioForView(Guid id)
         {
            var loanImpairmentScenario = await _loanImpairmentScenarioRepository.GetAsync(id);

            var output = new GetLoanImpairmentScenarioForViewDto { LoanImpairmentScenario = ObjectMapper.Map<LoanImpairmentScenarioDto>(loanImpairmentScenario) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentScenarios_Edit)]
		 public async Task<GetLoanImpairmentScenarioForEditOutput> GetLoanImpairmentScenarioForEdit(EntityDto<Guid> input)
         {
            var loanImpairmentScenario = await _loanImpairmentScenarioRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLoanImpairmentScenarioForEditOutput {LoanImpairmentScenario = ObjectMapper.Map<CreateOrEditLoanImpairmentScenarioDto>(loanImpairmentScenario)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLoanImpairmentScenarioDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentScenarios_Create)]
		 protected virtual async Task Create(CreateOrEditLoanImpairmentScenarioDto input)
         {
            var loanImpairmentScenario = ObjectMapper.Map<LoanImpairmentScenario>(input);

			

            await _loanImpairmentScenarioRepository.InsertAsync(loanImpairmentScenario);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentScenarios_Edit)]
		 protected virtual async Task Update(CreateOrEditLoanImpairmentScenarioDto input)
         {
            var loanImpairmentScenario = await _loanImpairmentScenarioRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, loanImpairmentScenario);
         }

		 [AbpAuthorize(AppPermissions.Pages_LoanImpairmentScenarios_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _loanImpairmentScenarioRepository.DeleteAsync(input.Id);
         } 
    }
}