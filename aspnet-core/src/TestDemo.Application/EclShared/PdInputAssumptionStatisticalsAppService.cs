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
	[AbpAuthorize(AppPermissions.Pages_PdInputAssumptionStatisticals)]
    public class PdInputAssumptionStatisticalsAppService : TestDemoAppServiceBase, IPdInputAssumptionStatisticalsAppService
    {
		 private readonly IRepository<PdInputAssumptionMacroeconomicInput, Guid> _pdInputAssumptionStatisticalRepository;
		 

		  public PdInputAssumptionStatisticalsAppService(IRepository<PdInputAssumptionMacroeconomicInput, Guid> pdInputAssumptionStatisticalRepository ) 
		  {
			_pdInputAssumptionStatisticalRepository = pdInputAssumptionStatisticalRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdInputAssumptionStatisticalForViewDto>> GetAll(GetAllPdInputAssumptionStatisticalsInput input)
         {
			
			var filteredPdInputAssumptionStatisticals = _pdInputAssumptionStatisticalRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter))
						.WhereIf(input.CanAffiliateEditFilter > -1,  e => (input.CanAffiliateEditFilter == 1 && e.CanAffiliateEdit) || (input.CanAffiliateEditFilter == 0 && !e.CanAffiliateEdit) );

			var pagedAndFilteredPdInputAssumptionStatisticals = filteredPdInputAssumptionStatisticals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdInputAssumptionStatisticals = from o in pagedAndFilteredPdInputAssumptionStatisticals
                         select new GetPdInputAssumptionStatisticalForViewDto() {
							PdInputAssumptionStatistical = new PdInputAssumptionStatisticalDto
							{
                                CanAffiliateEdit = o.CanAffiliateEdit,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdInputAssumptionStatisticals.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionStatisticalForViewDto>(
                totalCount,
                await pdInputAssumptionStatisticals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionStatisticals_Edit)]
		 public async Task<GetPdInputAssumptionStatisticalForEditOutput> GetPdInputAssumptionStatisticalForEdit(EntityDto<Guid> input)
         {
            var pdInputAssumptionStatistical = await _pdInputAssumptionStatisticalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdInputAssumptionStatisticalForEditOutput {PdInputAssumptionStatistical = ObjectMapper.Map<CreateOrEditPdInputAssumptionStatisticalDto>(pdInputAssumptionStatistical)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdInputAssumptionStatisticalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionStatisticals_Create)]
		 protected virtual async Task Create(CreateOrEditPdInputAssumptionStatisticalDto input)
         {
            var pdInputAssumptionStatistical = ObjectMapper.Map<PdInputAssumptionMacroeconomicInput>(input);

			

            await _pdInputAssumptionStatisticalRepository.InsertAsync(pdInputAssumptionStatistical);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionStatisticals_Edit)]
		 protected virtual async Task Update(CreateOrEditPdInputAssumptionStatisticalDto input)
         {
            var pdInputAssumptionStatistical = await _pdInputAssumptionStatisticalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdInputAssumptionStatistical);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumptionStatisticals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdInputAssumptionStatisticalRepository.DeleteAsync(input.Id);
         } 
    }
}