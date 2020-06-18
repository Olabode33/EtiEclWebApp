

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclShared
{
	[AbpAuthorize(AppPermissions.Pages_Configuration)]
    public class MacroeconomicVariablesAppService : TestDemoAppServiceBase, IMacroeconomicVariablesAppService
    {
		 private readonly IRepository<MacroeconomicVariable> _macroeconomicVariableRepository;
		 

		  public MacroeconomicVariablesAppService(IRepository<MacroeconomicVariable> macroeconomicVariableRepository ) 
		  {
			_macroeconomicVariableRepository = macroeconomicVariableRepository;
			
		  }

		 public async Task<PagedResultDto<GetMacroeconomicVariableForViewDto>> GetAll(GetAllMacroeconomicVariablesInput input)
         {
			
			var filteredMacroeconomicVariables = _macroeconomicVariableRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

			var pagedAndFilteredMacroeconomicVariables = filteredMacroeconomicVariables
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var macroeconomicVariables = from o in pagedAndFilteredMacroeconomicVariables
                         select new GetMacroeconomicVariableForViewDto() {
							MacroeconomicVariable = new MacroeconomicVariableDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredMacroeconomicVariables.CountAsync();

            return new PagedResultDto<GetMacroeconomicVariableForViewDto>(
                totalCount,
                await macroeconomicVariables.ToListAsync()
            );
         }
		 
		 public async Task<GetMacroeconomicVariableForEditOutput> GetMacroeconomicVariableForEdit(EntityDto input)
         {
            var macroeconomicVariable = await _macroeconomicVariableRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetMacroeconomicVariableForEditOutput {MacroeconomicVariable = ObjectMapper.Map<CreateOrEditMacroeconomicVariableDto>(macroeconomicVariable)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditMacroeconomicVariableDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
		 protected virtual async Task Create(CreateOrEditMacroeconomicVariableDto input)
         {
            var macroeconomicVariable = ObjectMapper.Map<MacroeconomicVariable>(input);

			

            await _macroeconomicVariableRepository.InsertAsync(macroeconomicVariable);
         }

		 [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
		 protected virtual async Task Update(CreateOrEditMacroeconomicVariableDto input)
         {
            var macroeconomicVariable = await _macroeconomicVariableRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, macroeconomicVariable);
         }

		 [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
         public async Task Delete(EntityDto input)
         {
            await _macroeconomicVariableRepository.DeleteAsync(input.Id);
         } 
    }
}