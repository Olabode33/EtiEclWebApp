

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.PdCalibrationResult
{
	[AbpAuthorize(AppPermissions.Pages_PdSnPCummulativeDefaultRates)]
    public class PdSnPCummulativeDefaultRatesAppService : TestDemoAppServiceBase, IPdSnPCummulativeDefaultRatesAppService
    {
		 private readonly IRepository<CalibrationResultPdSnPCummulativeDefaultRate, Guid> _pdSnPCummulativeDefaultRateRepository;
		 

		  public PdSnPCummulativeDefaultRatesAppService(IRepository<CalibrationResultPdSnPCummulativeDefaultRate, Guid> pdSnPCummulativeDefaultRateRepository ) 
		  {
			_pdSnPCummulativeDefaultRateRepository = pdSnPCummulativeDefaultRateRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllPdSnPCummulativeDefaultRatesInput input)
         {
			
			var filteredPdSnPCummulativeDefaultRates = _pdSnPCummulativeDefaultRateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter));

			var pagedAndFilteredPdSnPCummulativeDefaultRates = filteredPdSnPCummulativeDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdSnPCummulativeDefaultRates = from o in pagedAndFilteredPdSnPCummulativeDefaultRates
                         select new GetPdSnPCummulativeDefaultRateForViewDto() {
							PdSnPCummulativeDefaultRate = new PdSnPCummulativeDefaultRateDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdSnPCummulativeDefaultRates.CountAsync();

            return new PagedResultDto<GetPdSnPCummulativeDefaultRateForViewDto>(
                totalCount,
                await pdSnPCummulativeDefaultRates.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Edit)]
		 public async Task<GetPdSnPCummulativeDefaultRateForEditOutput> GetPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input)
         {
            var pdSnPCummulativeDefaultRate = await _pdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdSnPCummulativeDefaultRateForEditOutput {PdSnPCummulativeDefaultRate = ObjectMapper.Map<CreateOrEditPdSnPCummulativeDefaultRateDto>(pdSnPCummulativeDefaultRate)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdSnPCummulativeDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditPdSnPCummulativeDefaultRateDto input)
         {
            var pdSnPCummulativeDefaultRate = ObjectMapper.Map<CalibrationResultPdSnPCummulativeDefaultRate>(input);

			

            await _pdSnPCummulativeDefaultRateRepository.InsertAsync(pdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditPdSnPCummulativeDefaultRateDto input)
         {
            var pdSnPCummulativeDefaultRate = await _pdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdSnPCummulativeDefaultRateRepository.DeleteAsync(input.Id);
         } 
    }
}