

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.IVModels
{
	[AbpAuthorize(AppPermissions.Pages_MacroEconomicCreditIndices)]
    public class MacroEconomicCreditIndicesAppService : TestDemoAppServiceBase, IMacroEconomicCreditIndicesAppService
    {
		 private readonly IRepository<MacroEconomicCreditIndex, Guid> _macroEconomicCreditIndexRepository;
		 

		  public MacroEconomicCreditIndicesAppService(IRepository<MacroEconomicCreditIndex, Guid> macroEconomicCreditIndexRepository ) 
		  {
			_macroEconomicCreditIndexRepository = macroEconomicCreditIndexRepository;
			
		  }

		 public async Task<PagedResultDto<GetMacroEconomicCreditIndexForViewDto>> GetAll(GetAllMacroEconomicCreditIndicesInput input)
         {
			
			var filteredMacroEconomicCreditIndices = _macroEconomicCreditIndexRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredMacroEconomicCreditIndices = filteredMacroEconomicCreditIndices
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var macroEconomicCreditIndices = from o in pagedAndFilteredMacroEconomicCreditIndices
                         select new GetMacroEconomicCreditIndexForViewDto() {
							MacroEconomicCreditIndex = new MacroEconomicCreditIndexDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredMacroEconomicCreditIndices.CountAsync();

            return new PagedResultDto<GetMacroEconomicCreditIndexForViewDto>(
                totalCount,
                await macroEconomicCreditIndices.ToListAsync()
            );
         }
		 
		 public async Task<GetMacroEconomicCreditIndexForViewDto> GetMacroEconomicCreditIndexForView(Guid id)
         {
            var macroEconomicCreditIndex = await _macroEconomicCreditIndexRepository.GetAsync(id);

            var output = new GetMacroEconomicCreditIndexForViewDto { MacroEconomicCreditIndex = ObjectMapper.Map<MacroEconomicCreditIndexDto>(macroEconomicCreditIndex) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_MacroEconomicCreditIndices_Edit)]
		 public async Task<GetMacroEconomicCreditIndexForEditOutput> GetMacroEconomicCreditIndexForEdit(EntityDto<Guid> input)
         {
            var macroEconomicCreditIndex = await _macroEconomicCreditIndexRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetMacroEconomicCreditIndexForEditOutput {MacroEconomicCreditIndex = ObjectMapper.Map<CreateOrEditMacroEconomicCreditIndexDto>(macroEconomicCreditIndex)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditMacroEconomicCreditIndexDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_MacroEconomicCreditIndices_Create)]
		 protected virtual async Task Create(CreateOrEditMacroEconomicCreditIndexDto input)
         {
            var macroEconomicCreditIndex = ObjectMapper.Map<MacroEconomicCreditIndex>(input);

			

            await _macroEconomicCreditIndexRepository.InsertAsync(macroEconomicCreditIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_MacroEconomicCreditIndices_Edit)]
		 protected virtual async Task Update(CreateOrEditMacroEconomicCreditIndexDto input)
         {
            var macroEconomicCreditIndex = await _macroEconomicCreditIndexRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, macroEconomicCreditIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_MacroEconomicCreditIndices_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _macroEconomicCreditIndexRepository.DeleteAsync(input.Id);
         } 
    }
}