using TestDemo.Retail;

using TestDemo.EclShared;
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
    [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions)]
    public class RetailEclAssumptionsAppService : TestDemoAppServiceBase, IRetailEclAssumptionsAppService
    {
        private readonly IRepository<RetailEclAssumption, Guid> _retailEclAssumptionRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclAssumptionsAppService(IRepository<RetailEclAssumption, Guid> retailEclAssumptionRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclAssumptionRepository = retailEclAssumptionRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<PagedResultDto<GetRetailEclAssumptionForViewDto>> GetAll(GetAllRetailEclAssumptionsInput input)
        {
            var datatypeFilter = (DataTypeEnum)input.DatatypeFilter;
            var assumptionGroupFilter = (AssumptionGroupEnum)input.AssumptionGroupFilter;

            var filteredRetailEclAssumptions = _retailEclAssumptionRepository.GetAll()
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter), e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter), e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
                        .WhereIf(input.DatatypeFilter > -1, e => e.DataType == datatypeFilter)
                        .WhereIf(input.IsComputedFilter > -1, e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter)
                        .WhereIf(input.AssumptionGroupFilter > -1, e => e.AssumptionGroup == assumptionGroupFilter)
                        .WhereIf(input.RequiresGroupApprovalFilter > -1, e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter);

            var pagedAndFilteredRetailEclAssumptions = filteredRetailEclAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclAssumptions = from o in pagedAndFilteredRetailEclAssumptions
                                       join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                                       from s1 in j1.DefaultIfEmpty()

                                       select new GetRetailEclAssumptionForViewDto()
                                       {
                                           RetailEclAssumption = new RetailEclAssumptionDto
                                           {
                                               Key = o.Key,
                                               InputName = o.InputName,
                                               Value = o.Value,
                                               Datatype = o.DataType,
                                               IsComputed = o.IsComputed,
                                               AssumptionGroup = o.AssumptionGroup,
                                               RequiresGroupApproval = o.RequiresGroupApproval,
                                               Id = o.Id
                                           },
                                           RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                       };

            var totalCount = await filteredRetailEclAssumptions.CountAsync();

            return new PagedResultDto<GetRetailEclAssumptionForViewDto>(
                totalCount,
                await retailEclAssumptions.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions_Edit)]
        public async Task<GetRetailEclAssumptionForEditOutput> GetRetailEclAssumptionForEdit(EntityDto<Guid> input)
        {
            var retailEclAssumption = await _retailEclAssumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclAssumptionForEditOutput { RetailEclAssumption = ObjectMapper.Map<CreateOrEditRetailEclAssumptionDto>(retailEclAssumption) };

            if (output.RetailEclAssumption.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclAssumption.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task<List<AssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclAssumptionRepository.GetAll().Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new AssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.AssumptionGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  //Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        public async Task CreateOrEdit(CreateOrEditRetailEclAssumptionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions_Create)]
        protected virtual async Task Create(CreateOrEditRetailEclAssumptionDto input)
        {
            var retailEclAssumption = ObjectMapper.Map<RetailEclAssumption>(input);

            await _retailEclAssumptionRepository.InsertAsync(retailEclAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclAssumptionDto input)
        {
            var retailEclAssumption = await _retailEclAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclAssumptionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptions)]
        public async Task<PagedResultDto<RetailEclAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclAssumptionRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclAssumptionRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclAssumptionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}