

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
	[AbpAuthorize(AppPermissions.Pages_PdUpperbounds)]
    public class PdUpperboundsAppService : TestDemoAppServiceBase, IPdUpperboundsAppService
    {
		 private readonly IRepository<CalibrationResultPdUpperbound, Guid> _pdUpperboundRepository;
		 

		  public PdUpperboundsAppService(IRepository<CalibrationResultPdUpperbound, Guid> pdUpperboundRepository ) 
		  {
			_pdUpperboundRepository = pdUpperboundRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdUpperboundForViewDto>> GetAll(GetAllPdUpperboundsInput input)
         {
			
			var filteredPdUpperbounds = _pdUpperboundRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Rating.Contains(input.Filter));

			var pagedAndFilteredPdUpperbounds = filteredPdUpperbounds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdUpperbounds = from o in pagedAndFilteredPdUpperbounds
                         select new GetPdUpperboundForViewDto() {
							PdUpperbound = new PdUpperboundDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdUpperbounds.CountAsync();

            return new PagedResultDto<GetPdUpperboundForViewDto>(
                totalCount,
                await pdUpperbounds.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdUpperbounds_Edit)]
		 public async Task<GetPdUpperboundForEditOutput> GetPdUpperboundForEdit(EntityDto<Guid> input)
         {
            var pdUpperbound = await _pdUpperboundRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdUpperboundForEditOutput {PdUpperbound = ObjectMapper.Map<CreateOrEditPdUpperboundDto>(pdUpperbound)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdUpperboundDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdUpperbounds_Create)]
		 protected virtual async Task Create(CreateOrEditPdUpperboundDto input)
         {
            var pdUpperbound = ObjectMapper.Map<CalibrationResultPdUpperbound>(input);

			

            await _pdUpperboundRepository.InsertAsync(pdUpperbound);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdUpperbounds_Edit)]
		 protected virtual async Task Update(CreateOrEditPdUpperboundDto input)
         {
            var pdUpperbound = await _pdUpperboundRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdUpperbound);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdUpperbounds_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdUpperboundRepository.DeleteAsync(input.Id);
         } 
    }
}