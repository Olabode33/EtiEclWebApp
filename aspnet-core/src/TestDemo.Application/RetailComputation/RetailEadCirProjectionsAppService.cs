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
	[AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections)]
    public class RetailEadCirProjectionsAppService : TestDemoAppServiceBase, IRetailEadCirProjectionsAppService
    {
		 private readonly IRepository<RetailEadCirProjection, Guid> _retailEadCirProjectionRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEadCirProjectionsAppService(IRepository<RetailEadCirProjection, Guid> retailEadCirProjectionRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEadCirProjectionRepository = retailEadCirProjectionRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEadCirProjectionForViewDto>> GetAll(GetAllRetailEadCirProjectionsInput input)
         {
			
			var filteredRetailEadCirProjections = _retailEadCirProjectionRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CIR_GROUP.Contains(input.Filter) || e.CIR_EFFECTIVE.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailEadCirProjections = filteredRetailEadCirProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEadCirProjections = from o in pagedAndFilteredRetailEadCirProjections
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEadCirProjectionForViewDto() {
							RetailEadCirProjection = new RetailEadCirProjectionDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEadCirProjections.CountAsync();

            return new PagedResultDto<GetRetailEadCirProjectionForViewDto>(
                totalCount,
                await retailEadCirProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections_Edit)]
		 public async Task<GetRetailEadCirProjectionForEditOutput> GetRetailEadCirProjectionForEdit(EntityDto<Guid> input)
         {
            var retailEadCirProjection = await _retailEadCirProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEadCirProjectionForEditOutput {RetailEadCirProjection = ObjectMapper.Map<CreateOrEditRetailEadCirProjectionDto>(retailEadCirProjection)};

		    if (output.RetailEadCirProjection.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEadCirProjection.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEadCirProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEadCirProjectionDto input)
         {
            var retailEadCirProjection = ObjectMapper.Map<RetailEadCirProjection>(input);

			

            await _retailEadCirProjectionRepository.InsertAsync(retailEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEadCirProjectionDto input)
         {
            var retailEadCirProjection = await _retailEadCirProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEadCirProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEadCirProjections)]
         public async Task<PagedResultDto<RetailEadCirProjectionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEadCirProjectionRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEadCirProjectionRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEadCirProjectionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}