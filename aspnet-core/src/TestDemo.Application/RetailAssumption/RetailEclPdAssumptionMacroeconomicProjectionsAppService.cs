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
    [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections)]
    public class RetailEclPdAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IRetailEclPdAssumptionMacroeconomicProjectionsAppService
    {
        private readonly IRepository<RetailEclPdAssumptionMacroeconomicProjection, Guid> _retailEclPdAssumptionMacroeconomicProjectionRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclPdAssumptionMacroeconomicProjectionsAppService(IRepository<RetailEclPdAssumptionMacroeconomicProjection, Guid> retailEclPdAssumptionMacroeconomicProjectionRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclPdAssumptionMacroeconomicProjectionRepository = retailEclPdAssumptionMacroeconomicProjectionRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<PagedResultDto<GetRetailEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllRetailEclPdAssumptionMacroeconomicProjectionsInput input)
        {

            var filteredRetailEclPdAssumptionMacroeconomicProjections = _retailEclPdAssumptionMacroeconomicProjectionRepository.GetAll()
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

            var pagedAndFilteredRetailEclPdAssumptionMacroeconomicProjections = filteredRetailEclPdAssumptionMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclPdAssumptionMacroeconomicProjections = from o in pagedAndFilteredRetailEclPdAssumptionMacroeconomicProjections
                                                                join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                                                                from s1 in j1.DefaultIfEmpty()

                                                                select new GetRetailEclPdAssumptionMacroeconomicProjectionForViewDto()
                                                                {
                                                                    RetailEclPdAssumptionMacroeconomicProjection = new RetailEclPdAssumptionMacroeconomicProjectionDto
                                                                    {
                                                                        Id = o.Id
                                                                    },
                                                                    RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                                                };

            var totalCount = await filteredRetailEclPdAssumptionMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumptionMacroeconomicProjectionForViewDto>(
                totalCount,
                await retailEclPdAssumptionMacroeconomicProjections.ToListAsync()
            );
        }

        public async Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclPdAssumptionMacroeconomicProjectionRepository.GetAll()
                                                              .Include(x => x.MacroeconomicVariable)
                                                              .Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new PdInputAssumptionMacroeconomicProjectionDto()
                                                              {
                                                                  AssumptionGroup = x.MacroeconomicVariableId,
                                                                  Key = x.Key,
                                                                  Date = x.Date,
                                                                  InputName = x.MacroeconomicVariable != null ? x.MacroeconomicVariable.Name : "",
                                                                  BestValue = x.BestValue,
                                                                  OptimisticValue = x.OptimisticValue,
                                                                  DownturnValue = x.DownturnValue,
                                                                  IsComputed = x.IsComputed,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Edit)]
        public async Task<GetRetailEclPdAssumptionMacroeconomicProjectionForEditOutput> GetRetailEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input)
        {
            var retailEclPdAssumptionMacroeconomicProjection = await _retailEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclPdAssumptionMacroeconomicProjectionForEditOutput { RetailEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto>(retailEclPdAssumptionMacroeconomicProjection) };

            if (output.RetailEclPdAssumptionMacroeconomicProjection.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumptionMacroeconomicProjection.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Create)]
        protected virtual async Task Create(CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto input)
        {
            var retailEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<RetailEclPdAssumptionMacroeconomicProjection>(input);



            await _retailEclPdAssumptionMacroeconomicProjectionRepository.InsertAsync(retailEclPdAssumptionMacroeconomicProjection);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto input)
        {
            var retailEclPdAssumptionMacroeconomicProjection = await _retailEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclPdAssumptionMacroeconomicProjection);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclPdAssumptionMacroeconomicProjectionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections)]
        public async Task<PagedResultDto<RetailEclPdAssumptionMacroeconomicProjectionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclPdAssumptionMacroeconomicProjectionRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclPdAssumptionMacroeconomicProjectionRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclPdAssumptionMacroeconomicProjectionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}