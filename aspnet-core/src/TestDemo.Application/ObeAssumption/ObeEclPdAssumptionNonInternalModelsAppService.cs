using TestDemo.OBE;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeAssumption
{
    public class ObeEclPdAssumptionNonInternalModelsAppService : TestDemoAppServiceBase, IObeEclPdAssumptionNonInternalModelsAppService
    {
		 private readonly IRepository<ObeEclPdAssumptionNonInternalModel, Guid> _obeEclPdAssumptionNonInternalModelRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumptionNonInternalModelsAppService(IRepository<ObeEclPdAssumptionNonInternalModel, Guid> obeEclPdAssumptionNonInternalModelRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumptionNonInternalModelRepository = obeEclPdAssumptionNonInternalModelRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumptionNonInternalModelForViewDto>> GetAll(GetAllObeEclPdAssumptionNonInternalModelsInput input)
         {
			
			var filteredObeEclPdAssumptionNonInternalModels = _obeEclPdAssumptionNonInternalModelRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredObeEclPdAssumptionNonInternalModels = filteredObeEclPdAssumptionNonInternalModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumptionNonInternalModels = from o in pagedAndFilteredObeEclPdAssumptionNonInternalModels
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumptionNonInternalModelForViewDto() {
							ObeEclPdAssumptionNonInternalModel = new ObeEclPdAssumptionNonInternalModelDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumptionNonInternalModels.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumptionNonInternalModelForViewDto>(
                totalCount,
                await obeEclPdAssumptionNonInternalModels.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclPdAssumptionNonInternalModelForEditOutput> GetObeEclPdAssumptionNonInternalModelForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumptionNonInternalModel = await _obeEclPdAssumptionNonInternalModelRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumptionNonInternalModelForEditOutput {ObeEclPdAssumptionNonInternalModel = ObjectMapper.Map<CreateOrEditObeEclPdAssumptionNonInternalModelDto>(obeEclPdAssumptionNonInternalModel)};

		    if (output.ObeEclPdAssumptionNonInternalModel.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumptionNonInternalModel.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumptionNonInternalModelDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclPdAssumptionNonInternalModelDto input)
         {
            var obeEclPdAssumptionNonInternalModel = ObjectMapper.Map<ObeEclPdAssumptionNonInternalModel>(input);

			

            await _obeEclPdAssumptionNonInternalModelRepository.InsertAsync(obeEclPdAssumptionNonInternalModel);
         }

		 protected virtual async Task Update(CreateOrEditObeEclPdAssumptionNonInternalModelDto input)
         {
            var obeEclPdAssumptionNonInternalModel = await _obeEclPdAssumptionNonInternalModelRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumptionNonInternalModel);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumptionNonInternalModelRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclPdAssumptionNonInternalModelObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumptionNonInternalModelObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumptionNonInternalModelObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumptionNonInternalModelObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}