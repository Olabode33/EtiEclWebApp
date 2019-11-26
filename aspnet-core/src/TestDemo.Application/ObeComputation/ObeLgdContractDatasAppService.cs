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
	[AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas)]
    public class ObeLgdContractDatasAppService : TestDemoAppServiceBase, IObeLgdContractDatasAppService
    {
		 private readonly IRepository<ObeLgdContractData, Guid> _obeLgdContractDataRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeLgdContractDatasAppService(IRepository<ObeLgdContractData, Guid> obeLgdContractDataRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeLgdContractDataRepository = obeLgdContractDataRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeLgdContractDataForViewDto>> GetAll(GetAllObeLgdContractDatasInput input)
         {
			
			var filteredObeLgdContractDatas = _obeLgdContractDataRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CONTRACT_NO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObeLgdContractDatas = filteredObeLgdContractDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeLgdContractDatas = from o in pagedAndFilteredObeLgdContractDatas
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeLgdContractDataForViewDto() {
							ObeLgdContractData = new ObeLgdContractDataDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeLgdContractDatas.CountAsync();

            return new PagedResultDto<GetObeLgdContractDataForViewDto>(
                totalCount,
                await obeLgdContractDatas.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas_Edit)]
		 public async Task<GetObeLgdContractDataForEditOutput> GetObeLgdContractDataForEdit(EntityDto<Guid> input)
         {
            var obeLgdContractData = await _obeLgdContractDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeLgdContractDataForEditOutput {ObeLgdContractData = ObjectMapper.Map<CreateOrEditObeLgdContractDataDto>(obeLgdContractData)};

		    if (output.ObeLgdContractData.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeLgdContractData.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeLgdContractDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas_Create)]
		 protected virtual async Task Create(CreateOrEditObeLgdContractDataDto input)
         {
            var obeLgdContractData = ObjectMapper.Map<ObeLgdContractData>(input);

			

            await _obeLgdContractDataRepository.InsertAsync(obeLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditObeLgdContractDataDto input)
         {
            var obeLgdContractData = await _obeLgdContractDataRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeLgdContractDataRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeLgdContractDatas)]
         public async Task<PagedResultDto<ObeLgdContractDataObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeLgdContractDataObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeLgdContractDataObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeLgdContractDataObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}