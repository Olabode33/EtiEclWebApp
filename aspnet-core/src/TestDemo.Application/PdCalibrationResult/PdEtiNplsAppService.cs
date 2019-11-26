

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
	[AbpAuthorize(AppPermissions.Pages_PdEtiNpls)]
    public class PdEtiNplsAppService : TestDemoAppServiceBase, IPdEtiNplsAppService
    {
		 private readonly IRepository<CalibrationResultPdEtiNpl, Guid> _pdEtiNplRepository;
		 

		  public PdEtiNplsAppService(IRepository<CalibrationResultPdEtiNpl, Guid> pdEtiNplRepository ) 
		  {
			_pdEtiNplRepository = pdEtiNplRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdEtiNplForViewDto>> GetAll(GetAllPdEtiNplsInput input)
         {
			
			var filteredPdEtiNpls = _pdEtiNplRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredPdEtiNpls = filteredPdEtiNpls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdEtiNpls = from o in pagedAndFilteredPdEtiNpls
                         select new GetPdEtiNplForViewDto() {
							PdEtiNpl = new PdEtiNplDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdEtiNpls.CountAsync();

            return new PagedResultDto<GetPdEtiNplForViewDto>(
                totalCount,
                await pdEtiNpls.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdEtiNpls_Edit)]
		 public async Task<GetPdEtiNplForEditOutput> GetPdEtiNplForEdit(EntityDto<Guid> input)
         {
            var pdEtiNpl = await _pdEtiNplRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdEtiNplForEditOutput {PdEtiNpl = ObjectMapper.Map<CreateOrEditPdEtiNplDto>(pdEtiNpl)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdEtiNplDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdEtiNpls_Create)]
		 protected virtual async Task Create(CreateOrEditPdEtiNplDto input)
         {
            var pdEtiNpl = ObjectMapper.Map<CalibrationResultPdEtiNpl>(input);

			

            await _pdEtiNplRepository.InsertAsync(pdEtiNpl);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdEtiNpls_Edit)]
		 protected virtual async Task Update(CreateOrEditPdEtiNplDto input)
         {
            var pdEtiNpl = await _pdEtiNplRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdEtiNpl);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdEtiNpls_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdEtiNplRepository.DeleteAsync(input.Id);
         } 
    }
}