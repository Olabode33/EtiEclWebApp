
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
	[AbpAuthorize(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries)]
    public class LgdAssumptionUnsecuredRecoveriesAppService : TestDemoAppServiceBase, ILgdAssumptionUnsecuredRecoveriesAppService
    {
		 private readonly IRepository<LgdAssumptionUnsecuredRecovery, Guid> _lgdAssumptionUnsecuredRecoveryRepository;
		 

		  public LgdAssumptionUnsecuredRecoveriesAppService(IRepository<LgdAssumptionUnsecuredRecovery, Guid> lgdAssumptionUnsecuredRecoveryRepository ) 
		  {
			_lgdAssumptionUnsecuredRecoveryRepository = lgdAssumptionUnsecuredRecoveryRepository;
			
		  }

		 public async Task<PagedResultDto<GetLgdAssumptionUnsecuredRecoveryForViewDto>> GetAll(GetAllLgdAssumptionUnsecuredRecoveriesInput input)
         {
			
			var filteredLgdAssumptionUnsecuredRecoveries = _lgdAssumptionUnsecuredRecoveryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

			var pagedAndFilteredLgdAssumptionUnsecuredRecoveries = filteredLgdAssumptionUnsecuredRecoveries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var lgdAssumptionUnsecuredRecoveries = from o in pagedAndFilteredLgdAssumptionUnsecuredRecoveries
                         select new GetLgdAssumptionUnsecuredRecoveryForViewDto() {
							LgdAssumptionUnsecuredRecovery = new LgdAssumptionUnsecuredRecoveryDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                Id = o.Id
							}
						};

            var totalCount = await filteredLgdAssumptionUnsecuredRecoveries.CountAsync();

            return new PagedResultDto<GetLgdAssumptionUnsecuredRecoveryForViewDto>(
                totalCount,
                await lgdAssumptionUnsecuredRecoveries.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Edit)]
		 public async Task<GetLgdAssumptionUnsecuredRecoveryForEditOutput> GetLgdAssumptionUnsecuredRecoveryForEdit(EntityDto<Guid> input)
         {
            var lgdAssumptionUnsecuredRecovery = await _lgdAssumptionUnsecuredRecoveryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetLgdAssumptionUnsecuredRecoveryForEditOutput {LgdAssumptionUnsecuredRecovery = ObjectMapper.Map<CreateOrEditLgdAssumptionUnsecuredRecoveryDto>(lgdAssumptionUnsecuredRecovery)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Create)]
		 protected virtual async Task Create(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
         {
            var lgdAssumptionUnsecuredRecovery = ObjectMapper.Map<LgdAssumptionUnsecuredRecovery>(input);

			

            await _lgdAssumptionUnsecuredRecoveryRepository.InsertAsync(lgdAssumptionUnsecuredRecovery);
         }

		 [AbpAuthorize(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Edit)]
		 protected virtual async Task Update(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
         {
            var lgdAssumptionUnsecuredRecovery = await _lgdAssumptionUnsecuredRecoveryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, lgdAssumptionUnsecuredRecovery);
         }

		 [AbpAuthorize(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _lgdAssumptionUnsecuredRecoveryRepository.DeleteAsync(input.Id);
         } 
    }
}