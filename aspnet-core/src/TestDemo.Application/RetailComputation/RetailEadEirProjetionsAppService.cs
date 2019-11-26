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
	[AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions)]
    public class RetailEadEirProjetionsAppService : TestDemoAppServiceBase, IRetailEadEirProjetionsAppService
    {
		 private readonly IRepository<RetailEadEirProjetion, Guid> _retailEadEirProjetionRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEadEirProjetionsAppService(IRepository<RetailEadEirProjetion, Guid> retailEadEirProjetionRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEadEirProjetionRepository = retailEadEirProjetionRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEadEirProjetionForViewDto>> GetAll(GetAllRetailEadEirProjetionsInput input)
         {
			
			var filteredRetailEadEirProjetions = _retailEadEirProjetionRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.EIR_Group.Contains(input.Filter));

			var pagedAndFilteredRetailEadEirProjetions = filteredRetailEadEirProjetions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEadEirProjetions = from o in pagedAndFilteredRetailEadEirProjetions
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEadEirProjetionForViewDto() {
							RetailEadEirProjetion = new RetailEadEirProjetionDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEadEirProjetions.CountAsync();

            return new PagedResultDto<GetRetailEadEirProjetionForViewDto>(
                totalCount,
                await retailEadEirProjetions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions_Edit)]
		 public async Task<GetRetailEadEirProjetionForEditOutput> GetRetailEadEirProjetionForEdit(EntityDto<Guid> input)
         {
            var retailEadEirProjetion = await _retailEadEirProjetionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEadEirProjetionForEditOutput {RetailEadEirProjetion = ObjectMapper.Map<CreateOrEditRetailEadEirProjetionDto>(retailEadEirProjetion)};

		    if (output.RetailEadEirProjetion.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEadEirProjetion.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEadEirProjetionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEadEirProjetionDto input)
         {
            var retailEadEirProjetion = ObjectMapper.Map<RetailEadEirProjetion>(input);

			

            await _retailEadEirProjetionRepository.InsertAsync(retailEadEirProjetion);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEadEirProjetionDto input)
         {
            var retailEadEirProjetion = await _retailEadEirProjetionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEadEirProjetion);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEadEirProjetionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEadEirProjetions)]
         public async Task<PagedResultDto<RetailEadEirProjetionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEadEirProjetionRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEadEirProjetionRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEadEirProjetionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}