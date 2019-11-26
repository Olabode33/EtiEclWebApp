

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
	[AbpAuthorize(AppPermissions.Pages_PdHistoricIndexes)]
    public class PdHistoricIndexesAppService : TestDemoAppServiceBase, IPdHistoricIndexesAppService
    {
		 private readonly IRepository<CalibrationResultPdHistoricIndex, Guid> _pdHistoricIndexRepository;
		 

		  public PdHistoricIndexesAppService(IRepository<CalibrationResultPdHistoricIndex, Guid> pdHistoricIndexRepository ) 
		  {
			_pdHistoricIndexRepository = pdHistoricIndexRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdHistoricIndexForViewDto>> GetAll(GetAllPdHistoricIndexesInput input)
         {
			
			var filteredPdHistoricIndexes = _pdHistoricIndexRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredPdHistoricIndexes = filteredPdHistoricIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdHistoricIndexes = from o in pagedAndFilteredPdHistoricIndexes
                         select new GetPdHistoricIndexForViewDto() {
							PdHistoricIndex = new PdHistoricIndexDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdHistoricIndexes.CountAsync();

            return new PagedResultDto<GetPdHistoricIndexForViewDto>(
                totalCount,
                await pdHistoricIndexes.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdHistoricIndexes_Edit)]
		 public async Task<GetPdHistoricIndexForEditOutput> GetPdHistoricIndexForEdit(EntityDto<Guid> input)
         {
            var pdHistoricIndex = await _pdHistoricIndexRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdHistoricIndexForEditOutput {PdHistoricIndex = ObjectMapper.Map<CreateOrEditPdHistoricIndexDto>(pdHistoricIndex)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdHistoricIndexDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdHistoricIndexes_Create)]
		 protected virtual async Task Create(CreateOrEditPdHistoricIndexDto input)
         {
            var pdHistoricIndex = ObjectMapper.Map<CalibrationResultPdHistoricIndex>(input);

			

            await _pdHistoricIndexRepository.InsertAsync(pdHistoricIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdHistoricIndexes_Edit)]
		 protected virtual async Task Update(CreateOrEditPdHistoricIndexDto input)
         {
            var pdHistoricIndex = await _pdHistoricIndexRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdHistoricIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdHistoricIndexes_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdHistoricIndexRepository.DeleteAsync(input.Id);
         } 
    }
}