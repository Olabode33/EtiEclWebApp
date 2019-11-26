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
	[AbpAuthorize(AppPermissions.Pages_ObeEadInputs)]
    public class ObeEadInputsAppService : TestDemoAppServiceBase, IObeEadInputsAppService
    {
		 private readonly IRepository<ObeEadInput, Guid> _obeEadInputRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEadInputsAppService(IRepository<ObeEadInput, Guid> obeEadInputRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEadInputRepository = obeEadInputRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEadInputForViewDto>> GetAll(GetAllObeEadInputsInput input)
         {
			
			var filteredObeEadInputs = _obeEadInputRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.EIR_GROUP.Contains(input.Filter) || e.CIR_GROUP.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObeEadInputs = filteredObeEadInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEadInputs = from o in pagedAndFilteredObeEadInputs
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEadInputForViewDto() {
							ObeEadInput = new ObeEadInputDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEadInputs.CountAsync();

            return new PagedResultDto<GetObeEadInputForViewDto>(
                totalCount,
                await obeEadInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEadInputs_Edit)]
		 public async Task<GetObeEadInputForEditOutput> GetObeEadInputForEdit(EntityDto<Guid> input)
         {
            var obeEadInput = await _obeEadInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEadInputForEditOutput {ObeEadInput = ObjectMapper.Map<CreateOrEditObeEadInputDto>(obeEadInput)};

		    if (output.ObeEadInput.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEadInput.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEadInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadInputs_Create)]
		 protected virtual async Task Create(CreateOrEditObeEadInputDto input)
         {
            var obeEadInput = ObjectMapper.Map<ObeEadInput>(input);

			

            await _obeEadInputRepository.InsertAsync(obeEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEadInputDto input)
         {
            var obeEadInput = await _obeEadInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEadInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEadInputs)]
         public async Task<PagedResultDto<ObeEadInputObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEadInputObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEadInputObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEadInputObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}