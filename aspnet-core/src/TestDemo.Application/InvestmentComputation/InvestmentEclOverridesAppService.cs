using TestDemo.InvestmentComputation;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.InvestmentComputation
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides)]
    public class InvestmentEclOverridesAppService : TestDemoAppServiceBase, IInvestmentEclOverridesAppService
    {
		 private readonly IRepository<InvestmentEclOverride, Guid> _investmentEclOverrideRepository;
		 private readonly IRepository<InvestmentEclSicr,Guid> _lookup_investmentEclSicrRepository;
		 

		  public InvestmentEclOverridesAppService(IRepository<InvestmentEclOverride, Guid> investmentEclOverrideRepository , IRepository<InvestmentEclSicr, Guid> lookup_investmentEclSicrRepository) 
		  {
			_investmentEclOverrideRepository = investmentEclOverrideRepository;
			_lookup_investmentEclSicrRepository = lookup_investmentEclSicrRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclOverrideForViewDto>> GetAll(GetAllInvestmentEclOverridesInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredInvestmentEclOverrides = _investmentEclOverrideRepository.GetAll()
						.Include( e => e.InvestmentEclSicrFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.OverrideComment.Contains(input.Filter))
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.InvestmentEclSicrAssetDescriptionFilter), e => e.InvestmentEclSicrFk != null && e.InvestmentEclSicrFk.AssetDescription == input.InvestmentEclSicrAssetDescriptionFilter);

			var pagedAndFilteredInvestmentEclOverrides = filteredInvestmentEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclOverrides = from o in pagedAndFilteredInvestmentEclOverrides
                         join o1 in _lookup_investmentEclSicrRepository.GetAll() on o.InvestmentEclSicrId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclOverrideForViewDto() {
							InvestmentEclOverride = new InvestmentEclOverrideDto
							{
                                StageOverride = o.StageOverride,
                                OverrideComment = o.OverrideComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	InvestmentEclSicrAssetDescription = s1 == null ? "" : s1.AssetDescription.ToString()
						};

            var totalCount = await filteredInvestmentEclOverrides.CountAsync();

            return new PagedResultDto<GetInvestmentEclOverrideForViewDto>(
                totalCount,
                await investmentEclOverrides.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Edit)]
		 public async Task<GetInvestmentEclOverrideForEditOutput> GetInvestmentEclOverrideForEdit(EntityDto<Guid> input)
         {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclOverrideForEditOutput {InvestmentEclOverride = ObjectMapper.Map<CreateOrEditInvestmentEclOverrideDto>(investmentEclOverride)};

		    if (output.InvestmentEclOverride.InvestmentEclSicrId != null)
            {
                var _lookupInvestmentEclSicr = await _lookup_investmentEclSicrRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclOverride.InvestmentEclSicrId);
                output.InvestmentEclSicrAssetDescription = _lookupInvestmentEclSicr.AssetDescription.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclOverrideDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentEclOverrideDto input)
         {
            var investmentEclOverride = ObjectMapper.Map<InvestmentEclOverride>(input);

			

            await _investmentEclOverrideRepository.InsertAsync(investmentEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclOverrideDto input)
         {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclOverride);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclOverrideRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides)]
         public async Task<PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>> GetAllInvestmentEclSicrForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclSicrRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.AssetDescription.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclSicrList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>();
			foreach(var investmentEclSicr in investmentEclSicrList){
				lookupTableDtoList.Add(new InvestmentEclOverrideInvestmentEclSicrLookupTableDto
				{
					Id = investmentEclSicr.Id.ToString(),
					DisplayName = investmentEclSicr.AssetDescription?.ToString()
				});
			}

            return new PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}