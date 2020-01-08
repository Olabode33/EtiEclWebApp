
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
	[AbpAuthorize(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates)]
    public class InvSecFitchCummulativeDefaultRatesAppService : TestDemoAppServiceBase, IInvSecFitchCummulativeDefaultRatesAppService
    {
		 private readonly IRepository<InvSecFitchCummulativeDefaultRate, Guid> _invSecFitchCummulativeDefaultRateRepository;
		 

		  public InvSecFitchCummulativeDefaultRatesAppService(IRepository<InvSecFitchCummulativeDefaultRate, Guid> invSecFitchCummulativeDefaultRateRepository ) 
		  {
			_invSecFitchCummulativeDefaultRateRepository = invSecFitchCummulativeDefaultRateRepository;
			
		  }

		 public async Task<PagedResultDto<GetInvSecFitchCummulativeDefaultRateForViewDto>> GetAll(GetAllInvSecFitchCummulativeDefaultRatesInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredInvSecFitchCummulativeDefaultRates = _invSecFitchCummulativeDefaultRateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter))
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredInvSecFitchCummulativeDefaultRates = filteredInvSecFitchCummulativeDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var invSecFitchCummulativeDefaultRates = from o in pagedAndFilteredInvSecFitchCummulativeDefaultRates
                         select new GetInvSecFitchCummulativeDefaultRateForViewDto() {
							InvSecFitchCummulativeDefaultRate = new InvSecFitchCummulativeDefaultRateDto
							{
                                Rating = o.Rating,
                                Year = o.Year,
                                Value = o.Value,
                                Status = o.Status,
                                Id = o.Id
							}
						};

            var totalCount = await filteredInvSecFitchCummulativeDefaultRates.CountAsync();

            return new PagedResultDto<GetInvSecFitchCummulativeDefaultRateForViewDto>(
                totalCount,
                await invSecFitchCummulativeDefaultRates.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Edit)]
		 public async Task<GetInvSecFitchCummulativeDefaultRateForEditOutput> GetInvSecFitchCummulativeDefaultRateForEdit(EntityDto<Guid> input)
         {
            var invSecFitchCummulativeDefaultRate = await _invSecFitchCummulativeDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvSecFitchCummulativeDefaultRateForEditOutput {InvSecFitchCummulativeDefaultRate = ObjectMapper.Map<CreateOrEditInvSecFitchCummulativeDefaultRateDto>(invSecFitchCummulativeDefaultRate)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvSecFitchCummulativeDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditInvSecFitchCummulativeDefaultRateDto input)
         {
            var invSecFitchCummulativeDefaultRate = ObjectMapper.Map<InvSecFitchCummulativeDefaultRate>(input);

			

            await _invSecFitchCummulativeDefaultRateRepository.InsertAsync(invSecFitchCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditInvSecFitchCummulativeDefaultRateDto input)
         {
            var invSecFitchCummulativeDefaultRate = await _invSecFitchCummulativeDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, invSecFitchCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _invSecFitchCummulativeDefaultRateRepository.DeleteAsync(input.Id);
         } 
    }
}