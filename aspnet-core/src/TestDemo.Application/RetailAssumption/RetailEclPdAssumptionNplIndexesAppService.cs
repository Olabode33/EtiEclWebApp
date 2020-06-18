using TestDemo.Retail;

using TestDemo.EclShared;

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
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.RetailAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.RetailAssumption
{
    public class RetailEclPdAssumptionNplIndexesAppService : TestDemoAppServiceBase, IRetailEclPdAssumptionNplIndexesAppService
    {
        private readonly IRepository<RetailEclPdAssumptionNplIndex, Guid> _retailEclPdAssumptionNplIndexRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclPdAssumptionNplIndexesAppService(IRepository<RetailEclPdAssumptionNplIndex, Guid> retailEclPdAssumptionNplIndexRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclPdAssumptionNplIndexRepository = retailEclPdAssumptionNplIndexRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<PagedResultDto<GetRetailEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllRetailEclPdAssumptionNplIndexesInput input)
        {

            var filteredRetailEclPdAssumptionNplIndexes = _retailEclPdAssumptionNplIndexRepository.GetAll()
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter));

            var pagedAndFilteredRetailEclPdAssumptionNplIndexes = filteredRetailEclPdAssumptionNplIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclPdAssumptionNplIndexes = from o in pagedAndFilteredRetailEclPdAssumptionNplIndexes
                                                  join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                                                  from s1 in j1.DefaultIfEmpty()

                                                  select new GetRetailEclPdAssumptionNplIndexForViewDto()
                                                  {
                                                      RetailEclPdAssumptionNplIndex = new RetailEclPdAssumptionNplIndexDto
                                                      {
                                                          Id = o.Id
                                                      },
                                                      RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                                  };

            var totalCount = await filteredRetailEclPdAssumptionNplIndexes.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumptionNplIndexForViewDto>(
                totalCount,
                await retailEclPdAssumptionNplIndexes.ToListAsync()
            );
        }

        public async Task<List<PdInputAssumptionNplIndexDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclPdAssumptionNplIndexRepository.GetAll()
                                                              .Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new PdInputAssumptionNplIndexDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Date = x.Date,
                                                                  Actual = x.Actual,
                                                                  Standardised = x.Standardised,
                                                                  EtiNplSeries = x.EtiNplSeries,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();
        }


        public async Task<GetRetailEclPdAssumptionNplIndexForEditOutput> GetRetailEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input)
        {
            var retailEclPdAssumptionNplIndex = await _retailEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclPdAssumptionNplIndexForEditOutput { RetailEclPdAssumptionNplIndex = ObjectMapper.Map<CreateOrEditRetailEclPdAssumptionNplIndexDto>(retailEclPdAssumptionNplIndex) };

            if (output.RetailEclPdAssumptionNplIndex.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumptionNplIndex.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionNplIndexDto input)
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

        protected virtual async Task Create(CreateOrEditRetailEclPdAssumptionNplIndexDto input)
        {
            var retailEclPdAssumptionNplIndex = ObjectMapper.Map<RetailEclPdAssumptionNplIndex>(input);



            await _retailEclPdAssumptionNplIndexRepository.InsertAsync(retailEclPdAssumptionNplIndex);
        }

        protected virtual async Task Update(CreateOrEditRetailEclPdAssumptionNplIndexDto input)
        {
            var retailEclPdAssumptionNplIndex = await _retailEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclPdAssumptionNplIndex);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclPdAssumptionNplIndexRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<RetailEclPdAssumptionNplIndexRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclPdAssumptionNplIndexRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclPdAssumptionNplIndexRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclPdAssumptionNplIndexRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}