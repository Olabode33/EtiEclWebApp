using TestDemo.OBE;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeComputation
{
	[AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas)]
    public class ObeLgdCollateralTypeDatasAppService : TestDemoAppServiceBase, IObeLgdCollateralTypeDatasAppService
    {
		 private readonly IRepository<ObeLgdCollateralTypeData, Guid> _obeLgdCollateralTypeDataRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeLgdCollateralTypeDatasAppService(IRepository<ObeLgdCollateralTypeData, Guid> obeLgdCollateralTypeDataRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeLgdCollateralTypeDataRepository = obeLgdCollateralTypeDataRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeLgdCollateralTypeDataForViewDto>> GetAll(GetAllObeLgdCollateralTypeDatasInput input)
         {
			
			var filteredObeLgdCollateralTypeDatas = _obeLgdCollateralTypeDataRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CONTRACT_NO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObeLgdCollateralTypeDatas = filteredObeLgdCollateralTypeDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeLgdCollateralTypeDatas = from o in pagedAndFilteredObeLgdCollateralTypeDatas
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeLgdCollateralTypeDataForViewDto() {
							ObeLgdCollateralTypeData = new ObeLgdCollateralTypeDataDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeLgdCollateralTypeDatas.CountAsync();

            return new PagedResultDto<GetObeLgdCollateralTypeDataForViewDto>(
                totalCount,
                await obeLgdCollateralTypeDatas.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Edit)]
		 public async Task<GetObeLgdCollateralTypeDataForEditOutput> GetObeLgdCollateralTypeDataForEdit(EntityDto<Guid> input)
         {
            var obeLgdCollateralTypeData = await _obeLgdCollateralTypeDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeLgdCollateralTypeDataForEditOutput {ObeLgdCollateralTypeData = ObjectMapper.Map<CreateOrEditObeLgdCollateralTypeDataDto>(obeLgdCollateralTypeData)};

		    if (output.ObeLgdCollateralTypeData.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeLgdCollateralTypeData.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeLgdCollateralTypeDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Create)]
		 protected virtual async Task Create(CreateOrEditObeLgdCollateralTypeDataDto input)
         {
            var obeLgdCollateralTypeData = ObjectMapper.Map<ObeLgdCollateralTypeData>(input);

			

            await _obeLgdCollateralTypeDataRepository.InsertAsync(obeLgdCollateralTypeData);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditObeLgdCollateralTypeDataDto input)
         {
            var obeLgdCollateralTypeData = await _obeLgdCollateralTypeDataRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeLgdCollateralTypeData);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeLgdCollateralTypeDataRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeLgdCollateralTypeDatas)]
         public async Task<PagedResultDto<ObeLgdCollateralTypeDataObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeLgdCollateralTypeDataObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeLgdCollateralTypeDataObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeLgdCollateralTypeDataObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}