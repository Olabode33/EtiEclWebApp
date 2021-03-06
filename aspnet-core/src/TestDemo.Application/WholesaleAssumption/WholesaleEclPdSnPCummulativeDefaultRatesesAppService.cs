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
using TestDemo.EclShared.Dtos;

namespace TestDemo.WholesaleAssumption
{
    public class WholesaleEclPdSnPCummulativeDefaultRatesesAppService : TestDemoAppServiceBase, IWholesaleEclPdSnPCummulativeDefaultRatesesAppService
    {
		 private readonly IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> _wholesaleEclPdSnPCummulativeDefaultRatesRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdSnPCummulativeDefaultRatesesAppService(IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> wholesaleEclPdSnPCummulativeDefaultRatesRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdSnPCummulativeDefaultRatesRepository = wholesaleEclPdSnPCummulativeDefaultRatesRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

        public async Task<List<PdInputSnPCummulativeDefaultRateDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _wholesaleEclPdSnPCummulativeDefaultRatesRepository.GetAll().Where(x => x.WholesaleEclId == input.Id)
                                                              .Select(x => new PdInputSnPCummulativeDefaultRateDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Rating = x.Rating,
                                                                  Years = x.Years,
                                                                  Value = x.Value,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        public async Task<PagedResultDto<GetWholesaleEclPdSnPCummulativeDefaultRatesForViewDto>> GetAll(GetAllWholesaleEclPdSnPCummulativeDefaultRatesesInput input)
         {
			
			var filteredWholesaleEclPdSnPCummulativeDefaultRateses = _wholesaleEclPdSnPCummulativeDefaultRatesRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RatingFilter),  e => e.Rating.ToLower() == input.RatingFilter.ToLower().Trim())
						.WhereIf(input.MinYearsFilter != null, e => e.Years >= input.MinYearsFilter)
						.WhereIf(input.MaxYearsFilter != null, e => e.Years <= input.MaxYearsFilter)
						.WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
						.WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter);

			var pagedAndFilteredWholesaleEclPdSnPCummulativeDefaultRateses = filteredWholesaleEclPdSnPCummulativeDefaultRateses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdSnPCummulativeDefaultRateses = from o in pagedAndFilteredWholesaleEclPdSnPCummulativeDefaultRateses
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdSnPCummulativeDefaultRatesForViewDto() {
							WholesaleEclPdSnPCummulativeDefaultRates = new WholesaleEclPdSnPCummulativeDefaultRatesDto
							{
                                Key = o.Key,
                                Rating = o.Rating,
                                Years = o.Years,
                                Value = o.Value,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdSnPCummulativeDefaultRateses.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdSnPCummulativeDefaultRatesForViewDto>(
                totalCount,
                await wholesaleEclPdSnPCummulativeDefaultRateses.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclPdSnPCummulativeDefaultRatesForEditOutput> GetWholesaleEclPdSnPCummulativeDefaultRatesForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdSnPCummulativeDefaultRates = await _wholesaleEclPdSnPCummulativeDefaultRatesRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdSnPCummulativeDefaultRatesForEditOutput {WholesaleEclPdSnPCummulativeDefaultRates = ObjectMapper.Map<CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto>(wholesaleEclPdSnPCummulativeDefaultRates)};

		    if (output.WholesaleEclPdSnPCummulativeDefaultRates.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdSnPCummulativeDefaultRates.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto input)
         {
            var wholesaleEclPdSnPCummulativeDefaultRates = ObjectMapper.Map<WholesaleEclPdSnPCummulativeDefaultRate>(input);

            await _wholesaleEclPdSnPCummulativeDefaultRatesRepository.InsertAsync(wholesaleEclPdSnPCummulativeDefaultRates);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto input)
         {
            var wholesaleEclPdSnPCummulativeDefaultRates = await _wholesaleEclPdSnPCummulativeDefaultRatesRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdSnPCummulativeDefaultRates);
         }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclPdSnPCummulativeDefaultRatesRepository.DeleteAsync(input.Id);
        }
    }
}