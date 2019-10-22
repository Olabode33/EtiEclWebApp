

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
	[AbpAuthorize(AppPermissions.Pages_PdInputAssumption12Months)]
    public class PdInputAssumption12MonthsAppService : TestDemoAppServiceBase, IPdInputAssumption12MonthsAppService
    {
		 private readonly IRepository<PdInputAssumption12Month, Guid> _pdInputAssumption12MonthRepository;
		 

		  public PdInputAssumption12MonthsAppService(IRepository<PdInputAssumption12Month, Guid> pdInputAssumption12MonthRepository ) 
		  {
			_pdInputAssumption12MonthRepository = pdInputAssumption12MonthRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdInputAssumption12MonthForViewDto>> GetAll(GetAllPdInputAssumption12MonthsInput input)
         {
			
			var filteredPdInputAssumption12Months = _pdInputAssumption12MonthRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SnPMappingEtiCreditPolicy.Contains(input.Filter) || e.SnPMappingBestFit.Contains(input.Filter))
						.WhereIf(input.MinCreditFilter != null, e => e.Credit >= input.MinCreditFilter)
						.WhereIf(input.MaxCreditFilter != null, e => e.Credit <= input.MaxCreditFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingEtiCreditPolicyFilter),  e => e.SnPMappingEtiCreditPolicy.ToLower() == input.SnPMappingEtiCreditPolicyFilter.ToLower().Trim());

			var pagedAndFilteredPdInputAssumption12Months = filteredPdInputAssumption12Months
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdInputAssumption12Months = from o in pagedAndFilteredPdInputAssumption12Months
                         select new GetPdInputAssumption12MonthForViewDto() {
							PdInputAssumption12Month = new PdInputAssumption12MonthDto
							{
                                Credit = o.Credit,
                                PD = o.PD,
                                SnPMappingEtiCreditPolicy = o.SnPMappingEtiCreditPolicy,
                                SnPMappingBestFit = o.SnPMappingBestFit,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdInputAssumption12Months.CountAsync();

            return new PagedResultDto<GetPdInputAssumption12MonthForViewDto>(
                totalCount,
                await pdInputAssumption12Months.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumption12Months_Edit)]
		 public async Task<GetPdInputAssumption12MonthForEditOutput> GetPdInputAssumption12MonthForEdit(EntityDto<Guid> input)
         {
            var pdInputAssumption12Month = await _pdInputAssumption12MonthRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdInputAssumption12MonthForEditOutput {PdInputAssumption12Month = ObjectMapper.Map<CreateOrEditPdInputAssumption12MonthDto>(pdInputAssumption12Month)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdInputAssumption12MonthDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumption12Months_Create)]
		 protected virtual async Task Create(CreateOrEditPdInputAssumption12MonthDto input)
         {
            var pdInputAssumption12Month = ObjectMapper.Map<PdInputAssumption12Month>(input);

			

            await _pdInputAssumption12MonthRepository.InsertAsync(pdInputAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumption12Months_Edit)]
		 protected virtual async Task Update(CreateOrEditPdInputAssumption12MonthDto input)
         {
            var pdInputAssumption12Month = await _pdInputAssumption12MonthRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdInputAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdInputAssumption12Months_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdInputAssumption12MonthRepository.DeleteAsync(input.Id);
         } 
    }
}