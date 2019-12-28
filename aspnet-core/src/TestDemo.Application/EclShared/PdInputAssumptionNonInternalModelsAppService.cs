﻿
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
	[AbpAuthorize(AppPermissions.Pages_PdInputAssumptionNonInternalModels)]
    public class PdInputAssumptionNonInternalModelsAppService : TestDemoAppServiceBase, IPdInputAssumptionNonInternalModelsAppService
    {
		 private readonly IRepository<PdInputAssumptionNonInternalModel, Guid> _pdInputAssumptionNonInternalModelRepository;
		 

		  public PdInputAssumptionNonInternalModelsAppService(IRepository<PdInputAssumptionNonInternalModel, Guid> pdInputAssumptionNonInternalModelRepository ) 
		  {
			_pdInputAssumptionNonInternalModelRepository = pdInputAssumptionNonInternalModelRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdInputAssumptionNonInternalModelForViewDto>> GetAll(GetAllPdInputAssumptionNonInternalModelsInput input)
         {
			
			var filteredPdInputAssumptionNonInternalModels = _pdInputAssumptionNonInternalModelRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredPdInputAssumptionNonInternalModels = filteredPdInputAssumptionNonInternalModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdInputAssumptionNonInternalModels = from o in pagedAndFilteredPdInputAssumptionNonInternalModels
                         select new GetPdInputAssumptionNonInternalModelForViewDto() {
							PdInputAssumptionNonInternalModel = new PdInputAssumptionNonInternalModelDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdInputAssumptionNonInternalModels.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionNonInternalModelForViewDto>(
                totalCount,
                await pdInputAssumptionNonInternalModels.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Edit)]
		 public async Task<GetPdInputAssumptionNonInternalModelForEditOutput> GetPdInputAssumptionNonInternalModelForEdit(EntityDto<Guid> input)
         {
            var pdInputAssumptionNonInternalModel = await _pdInputAssumptionNonInternalModelRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdInputAssumptionNonInternalModelForEditOutput {PdInputAssumptionNonInternalModel = ObjectMapper.Map<CreateOrEditPdInputAssumptionNonInternalModelDto>(pdInputAssumptionNonInternalModel)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdInputAssumptionNonInternalModelDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Create)]
		 protected virtual async Task Create(CreateOrEditPdInputAssumptionNonInternalModelDto input)
         {
            var pdInputAssumptionNonInternalModel = ObjectMapper.Map<PdInputAssumptionNonInternalModel>(input);

			

            await _pdInputAssumptionNonInternalModelRepository.InsertAsync(pdInputAssumptionNonInternalModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Edit)]
		 protected virtual async Task Update(CreateOrEditPdInputAssumptionNonInternalModelDto input)
         {
            var pdInputAssumptionNonInternalModel = await _pdInputAssumptionNonInternalModelRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdInputAssumptionNonInternalModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdInputAssumptionNonInternalModelRepository.DeleteAsync(input.Id);
         } 
    }
}