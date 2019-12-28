﻿
using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

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
	[AbpAuthorize(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections)]
    public class PdInputAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IPdInputAssumptionMacroeconomicProjectionsAppService
    {
		 private readonly IRepository<PdInputAssumptionMacroeconomicProjection, Guid> _pdInputAssumptionMacroeconomicProjectionRepository;
		 

		  public PdInputAssumptionMacroeconomicProjectionsAppService(IRepository<PdInputAssumptionMacroeconomicProjection, Guid> pdInputAssumptionMacroeconomicProjectionRepository ) 
		  {
			_pdInputAssumptionMacroeconomicProjectionRepository = pdInputAssumptionMacroeconomicProjectionRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdInputAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllPdInputAssumptionMacroeconomicProjectionsInput input)
         {
			
			var filteredPdInputAssumptionMacroeconomicProjections = _pdInputAssumptionMacroeconomicProjectionRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

			var pagedAndFilteredPdInputAssumptionMacroeconomicProjections = filteredPdInputAssumptionMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdInputAssumptionMacroeconomicProjections = from o in pagedAndFilteredPdInputAssumptionMacroeconomicProjections
                         select new GetPdInputAssumptionMacroeconomicProjectionForViewDto() {
							PdInputAssumptionMacroeconomicProjection = new PdInputAssumptionMacroeconomicProjectionDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdInputAssumptionMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionMacroeconomicProjectionForViewDto>(
                totalCount,
                await pdInputAssumptionMacroeconomicProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Edit)]
		 public async Task<GetPdInputAssumptionMacroeconomicProjectionForEditOutput> GetPdInputAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input)
         {
            var pdInputAssumptionMacroeconomicProjection = await _pdInputAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdInputAssumptionMacroeconomicProjectionForEditOutput {PdInputAssumptionMacroeconomicProjection = ObjectMapper.Map<CreateOrEditPdInputAssumptionMacroeconomicProjectionDto>(pdInputAssumptionMacroeconomicProjection)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Create)]
		 protected virtual async Task Create(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
         {
            var pdInputAssumptionMacroeconomicProjection = ObjectMapper.Map<PdInputAssumptionMacroeconomicProjection>(input);

			

            await _pdInputAssumptionMacroeconomicProjectionRepository.InsertAsync(pdInputAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
         {
            var pdInputAssumptionMacroeconomicProjection = await _pdInputAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdInputAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdInputAssumptionMacroeconomicProjectionRepository.DeleteAsync(input.Id);
         } 
    }
}