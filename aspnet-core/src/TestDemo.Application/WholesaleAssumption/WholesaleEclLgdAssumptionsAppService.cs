using TestDemo.Wholesale;

using TestDemo.EclShared;
using TestDemo.EclShared;

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
    public class WholesaleEclLgdAssumptionsAppService : TestDemoAppServiceBase, IWholesaleEclLgdAssumptionsAppService
    {
		 private readonly IRepository<WholesaleEclLgdAssumption, Guid> _wholesaleEclLgdAssumptionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclLgdAssumptionsAppService(IRepository<WholesaleEclLgdAssumption, Guid> wholesaleEclLgdAssumptionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclLgdAssumptionRepository = wholesaleEclLgdAssumptionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

        public async Task<List<LgdAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _wholesaleEclLgdAssumptionRepository.GetAll().Where(x => x.WholesaleEclId == input.Id)
                                                              .Select(x => new LgdAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.LgdGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        public async Task<PagedResultDto<GetWholesaleEclLgdAssumptionForViewDto>> GetAll(GetAllWholesaleEclLgdAssumptionsInput input)
         {
			
			var filteredWholesaleEclLgdAssumptions = _wholesaleEclLgdAssumptionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclLgdAssumptions = filteredWholesaleEclLgdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclLgdAssumptions = from o in pagedAndFilteredWholesaleEclLgdAssumptions
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclLgdAssumptionForViewDto() {
							WholesaleEclLgdAssumption = new WholesaleEclLgdAssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclLgdAssumptions.CountAsync();

            return new PagedResultDto<GetWholesaleEclLgdAssumptionForViewDto>(
                totalCount,
                await wholesaleEclLgdAssumptions.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclLgdAssumptionForEditOutput> GetWholesaleEclLgdAssumptionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclLgdAssumption = await _wholesaleEclLgdAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclLgdAssumptionForEditOutput {WholesaleEclLgdAssumption = ObjectMapper.Map<CreateOrEditWholesaleEclLgdAssumptionDto>(wholesaleEclLgdAssumption)};

		    if (output.WholesaleEclLgdAssumption.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclLgdAssumption.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

        public async Task CreateOrEdit(CreateOrEditWholesaleEclLgdAssumptionDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }
         
		 protected virtual async Task Create(CreateOrEditWholesaleEclLgdAssumptionDto input)
         {
            var wholesaleEclLgdAssumption = ObjectMapper.Map<WholesaleEclLgdAssumption>(input);

            await _wholesaleEclLgdAssumptionRepository.InsertAsync(wholesaleEclLgdAssumption);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclLgdAssumptionDto input)
         {
            var wholesaleEclLgdAssumption = await _wholesaleEclLgdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclLgdAssumption);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclLgdAssumptionRepository.DeleteAsync(input.Id);
         } 
    }
}