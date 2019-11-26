using TestDemo.Retail;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailComputation
{
	[AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas)]
    public class RetailLgdContractDatasAppService : TestDemoAppServiceBase, IRetailLgdContractDatasAppService
    {
		 private readonly IRepository<RetailLgdContractData, Guid> _retailLgdContractDataRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailLgdContractDatasAppService(IRepository<RetailLgdContractData, Guid> retailLgdContractDataRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailLgdContractDataRepository = retailLgdContractDataRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailLgdContractDataForViewDto>> GetAll(GetAllRetailLgdContractDatasInput input)
         {
			
			var filteredRetailLgdContractDatas = _retailLgdContractDataRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CONTRACT_NO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.CONTRACT_NOFilter),  e => e.CONTRACT_NO == input.CONTRACT_NOFilter);

			var pagedAndFilteredRetailLgdContractDatas = filteredRetailLgdContractDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailLgdContractDatas = from o in pagedAndFilteredRetailLgdContractDatas
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailLgdContractDataForViewDto() {
							RetailLgdContractData = new RetailLgdContractDataDto
							{
                                CONTRACT_NO = o.CONTRACT_NO,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailLgdContractDatas.CountAsync();

            return new PagedResultDto<GetRetailLgdContractDataForViewDto>(
                totalCount,
                await retailLgdContractDatas.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas_Edit)]
		 public async Task<GetRetailLgdContractDataForEditOutput> GetRetailLgdContractDataForEdit(EntityDto<Guid> input)
         {
            var retailLgdContractData = await _retailLgdContractDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailLgdContractDataForEditOutput {RetailLgdContractData = ObjectMapper.Map<CreateOrEditRetailLgdContractDataDto>(retailLgdContractData)};

		    if (output.RetailLgdContractData.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailLgdContractData.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailLgdContractDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas_Create)]
		 protected virtual async Task Create(CreateOrEditRetailLgdContractDataDto input)
         {
            var retailLgdContractData = ObjectMapper.Map<RetailLgdContractData>(input);

			

            await _retailLgdContractDataRepository.InsertAsync(retailLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailLgdContractDataDto input)
         {
            var retailLgdContractData = await _retailLgdContractDataRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailLgdContractDataRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailLgdContractDatas)]
         public async Task<PagedResultDto<RetailLgdContractDataRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailLgdContractDataRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailLgdContractDataRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailLgdContractDataRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}