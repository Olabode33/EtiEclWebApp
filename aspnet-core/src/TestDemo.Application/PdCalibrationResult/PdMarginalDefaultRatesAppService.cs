

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
	[AbpAuthorize(AppPermissions.Pages_PdMarginalDefaultRates)]
    public class PdMarginalDefaultRatesAppService : TestDemoAppServiceBase, IPdMarginalDefaultRatesAppService
    {
		 private readonly IRepository<CalibrationResultPdMarginalDefaultRate, Guid> _pdMarginalDefaultRateRepository;
		 

		  public PdMarginalDefaultRatesAppService(IRepository<CalibrationResultPdMarginalDefaultRate, Guid> pdMarginalDefaultRateRepository ) 
		  {
			_pdMarginalDefaultRateRepository = pdMarginalDefaultRateRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdMarginalDefaultRateForViewDto>> GetAll(GetAllPdMarginalDefaultRatesInput input)
         {
			
			var filteredPdMarginalDefaultRates = _pdMarginalDefaultRateRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredPdMarginalDefaultRates = filteredPdMarginalDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdMarginalDefaultRates = from o in pagedAndFilteredPdMarginalDefaultRates
                         select new GetPdMarginalDefaultRateForViewDto() {
							PdMarginalDefaultRate = new PdMarginalDefaultRateDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdMarginalDefaultRates.CountAsync();

            return new PagedResultDto<GetPdMarginalDefaultRateForViewDto>(
                totalCount,
                await pdMarginalDefaultRates.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdMarginalDefaultRates_Edit)]
		 public async Task<GetPdMarginalDefaultRateForEditOutput> GetPdMarginalDefaultRateForEdit(EntityDto<Guid> input)
         {
            var pdMarginalDefaultRate = await _pdMarginalDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdMarginalDefaultRateForEditOutput {PdMarginalDefaultRate = ObjectMapper.Map<CreateOrEditPdMarginalDefaultRateDto>(pdMarginalDefaultRate)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdMarginalDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdMarginalDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditPdMarginalDefaultRateDto input)
         {
            var pdMarginalDefaultRate = ObjectMapper.Map<CalibrationResultPdMarginalDefaultRate>(input);

			

            await _pdMarginalDefaultRateRepository.InsertAsync(pdMarginalDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdMarginalDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditPdMarginalDefaultRateDto input)
         {
            var pdMarginalDefaultRate = await _pdMarginalDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdMarginalDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdMarginalDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdMarginalDefaultRateRepository.DeleteAsync(input.Id);
         } 
    }
}