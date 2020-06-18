using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleAssumption
{
    public class WholesaleEclPdAssumption12MonthsesAppService : TestDemoAppServiceBase, IWholesaleEclPdAssumption12MonthsesAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumption12Month, Guid> _wholesaleEclPdAssumption12MonthsRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdAssumption12MonthsesAppService(IRepository<WholesaleEclPdAssumption12Month, Guid> wholesaleEclPdAssumption12MonthsRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdAssumption12MonthsRepository = wholesaleEclPdAssumption12MonthsRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclPdAssumption12MonthsForViewDto>> GetAll(GetAllWholesaleEclPdAssumption12MonthsesInput input)
         {
			
			var filteredWholesaleEclPdAssumption12Monthses = _wholesaleEclPdAssumption12MonthsRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SnPMappingEtiCreditPolicy.Contains(input.Filter) || e.SnPMappingBestFit.Contains(input.Filter))
						.WhereIf(input.MinCreditFilter != null, e => e.Credit >= input.MinCreditFilter)
						.WhereIf(input.MaxCreditFilter != null, e => e.Credit <= input.MaxCreditFilter)
						.WhereIf(input.MinPDFilter != null, e => e.PD >= input.MinPDFilter)
						.WhereIf(input.MaxPDFilter != null, e => e.PD <= input.MaxPDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingEtiCreditPolicyFilter),  e => e.SnPMappingEtiCreditPolicy.ToLower() == input.SnPMappingEtiCreditPolicyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingBestFitFilter),  e => e.SnPMappingBestFit.ToLower() == input.SnPMappingBestFitFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclPdAssumption12Monthses = filteredWholesaleEclPdAssumption12Monthses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdAssumption12Monthses = from o in pagedAndFilteredWholesaleEclPdAssumption12Monthses
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdAssumption12MonthsForViewDto() {
							WholesaleEclPdAssumption12Months = new WholesaleEclPdAssumption12MonthsDto
							{
                                Credit = o.Credit,
                                PD = o.PD,
                                SnPMappingEtiCreditPolicy = o.SnPMappingEtiCreditPolicy,
                                SnPMappingBestFit = o.SnPMappingBestFit,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdAssumption12Monthses.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdAssumption12MonthsForViewDto>(
                totalCount,
                await wholesaleEclPdAssumption12Monthses.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclPdAssumption12MonthsForEditOutput> GetWholesaleEclPdAssumption12MonthsForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdAssumption12Months = await _wholesaleEclPdAssumption12MonthsRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdAssumption12MonthsForEditOutput {WholesaleEclPdAssumption12Months = ObjectMapper.Map<CreateOrEditWholesaleEclPdAssumption12MonthsDto>(wholesaleEclPdAssumption12Months)};

		    if (output.WholesaleEclPdAssumption12Months.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdAssumption12Months.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumption12MonthsDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclPdAssumption12MonthsDto input)
         {
            var wholesaleEclPdAssumption12Months = ObjectMapper.Map<WholesaleEclPdAssumption12Month>(input);

            await _wholesaleEclPdAssumption12MonthsRepository.InsertAsync(wholesaleEclPdAssumption12Months);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclPdAssumption12MonthsDto input)
         {
            var wholesaleEclPdAssumption12Months = await _wholesaleEclPdAssumption12MonthsRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdAssumption12Months);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclPdAssumption12MonthsRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}