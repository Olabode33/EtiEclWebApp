

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
	[AbpAuthorize(AppPermissions.Pages_Pd12MonthPds)]
    public class Pd12MonthPdsAppService : TestDemoAppServiceBase, IPd12MonthPdsAppService
    {
		 private readonly IRepository<CalibrationResult12MonthPd, Guid> _pd12MonthPdRepository;
		 

		  public Pd12MonthPdsAppService(IRepository<CalibrationResult12MonthPd, Guid> pd12MonthPdRepository ) 
		  {
			_pd12MonthPdRepository = pd12MonthPdRepository;
			
		  }

		 public async Task<PagedResultDto<GetPd12MonthPdForViewDto>> GetAll(GetAllPd12MonthPdsInput input)
         {
			
			var filteredPd12MonthPds = _pd12MonthPdRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SnPMappingEtiCreditPolicy.Contains(input.Filter) || e.SnPMappingBestFit.Contains(input.Filter));

			var pagedAndFilteredPd12MonthPds = filteredPd12MonthPds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pd12MonthPds = from o in pagedAndFilteredPd12MonthPds
                         select new GetPd12MonthPdForViewDto() {
							Pd12MonthPd = new Pd12MonthPdDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPd12MonthPds.CountAsync();

            return new PagedResultDto<GetPd12MonthPdForViewDto>(
                totalCount,
                await pd12MonthPds.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Pd12MonthPds_Edit)]
		 public async Task<GetPd12MonthPdForEditOutput> GetPd12MonthPdForEdit(EntityDto<Guid> input)
         {
            var pd12MonthPd = await _pd12MonthPdRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPd12MonthPdForEditOutput {Pd12MonthPd = ObjectMapper.Map<CreateOrEditPd12MonthPdDto>(pd12MonthPd)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPd12MonthPdDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Pd12MonthPds_Create)]
		 protected virtual async Task Create(CreateOrEditPd12MonthPdDto input)
         {
            var pd12MonthPd = ObjectMapper.Map<CalibrationResult12MonthPd>(input);

			

            await _pd12MonthPdRepository.InsertAsync(pd12MonthPd);
         }

		 [AbpAuthorize(AppPermissions.Pages_Pd12MonthPds_Edit)]
		 protected virtual async Task Update(CreateOrEditPd12MonthPdDto input)
         {
            var pd12MonthPd = await _pd12MonthPdRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pd12MonthPd);
         }

		 [AbpAuthorize(AppPermissions.Pages_Pd12MonthPds_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pd12MonthPdRepository.DeleteAsync(input.Id);
         } 
    }
}