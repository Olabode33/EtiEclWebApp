using TestDemo.Retail;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailAssumption
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels)]
    public class RetailEclPdAssumptionNonInteralModelsAppService : TestDemoAppServiceBase, IRetailEclPdAssumptionNonInteralModelsAppService
    {
		 private readonly IRepository<RetailEclPdAssumptionNonInteralModel, Guid> _retailEclPdAssumptionNonInteralModelRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclPdAssumptionNonInteralModelsAppService(IRepository<RetailEclPdAssumptionNonInteralModel, Guid> retailEclPdAssumptionNonInteralModelRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclPdAssumptionNonInteralModelRepository = retailEclPdAssumptionNonInteralModelRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclPdAssumptionNonInteralModelForViewDto>> GetAll(GetAllRetailEclPdAssumptionNonInteralModelsInput input)
         {
			
			var filteredRetailEclPdAssumptionNonInteralModels = _retailEclPdAssumptionNonInteralModelRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredRetailEclPdAssumptionNonInteralModels = filteredRetailEclPdAssumptionNonInteralModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclPdAssumptionNonInteralModels = from o in pagedAndFilteredRetailEclPdAssumptionNonInteralModels
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclPdAssumptionNonInteralModelForViewDto() {
							RetailEclPdAssumptionNonInteralModel = new RetailEclPdAssumptionNonInteralModelDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclPdAssumptionNonInteralModels.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumptionNonInteralModelForViewDto>(
                totalCount,
                await retailEclPdAssumptionNonInteralModels.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Edit)]
		 public async Task<GetRetailEclPdAssumptionNonInteralModelForEditOutput> GetRetailEclPdAssumptionNonInteralModelForEdit(EntityDto<Guid> input)
         {
            var retailEclPdAssumptionNonInteralModel = await _retailEclPdAssumptionNonInteralModelRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclPdAssumptionNonInteralModelForEditOutput {RetailEclPdAssumptionNonInteralModel = ObjectMapper.Map<CreateOrEditRetailEclPdAssumptionNonInteralModelDto>(retailEclPdAssumptionNonInteralModel)};

		    if (output.RetailEclPdAssumptionNonInteralModel.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumptionNonInteralModel.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionNonInteralModelDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclPdAssumptionNonInteralModelDto input)
         {
            var retailEclPdAssumptionNonInteralModel = ObjectMapper.Map<RetailEclPdAssumptionNonInteralModel>(input);

			

            await _retailEclPdAssumptionNonInteralModelRepository.InsertAsync(retailEclPdAssumptionNonInteralModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclPdAssumptionNonInteralModelDto input)
         {
            var retailEclPdAssumptionNonInteralModel = await _retailEclPdAssumptionNonInteralModelRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclPdAssumptionNonInteralModel);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclPdAssumptionNonInteralModelRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels)]
         public async Task<PagedResultDto<RetailEclPdAssumptionNonInteralModelRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclPdAssumptionNonInteralModelRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclPdAssumptionNonInteralModelRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclPdAssumptionNonInteralModelRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}