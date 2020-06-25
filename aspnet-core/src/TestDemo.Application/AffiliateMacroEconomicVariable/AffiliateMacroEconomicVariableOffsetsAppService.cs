using Abp.Organizations;
using TestDemo.EclShared;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.AffiliateMacroEconomicVariable.Exporting;
using TestDemo.AffiliateMacroEconomicVariable.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.AffiliateMacroEconomicVariable
{
    public class AffiliateMacroEconomicVariableOffsetsAppService : TestDemoAppServiceBase, IAffiliateMacroEconomicVariableOffsetsAppService
    {
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroEconomicVariableOffsetRepository;
        private readonly IAffiliateMacroEconomicVariableOffsetsExcelExporter _affiliateMacroEconomicVariableOffsetsExcelExporter;
        private readonly IRepository<OrganizationUnit, long> _lookup_organizationUnitRepository;
        private readonly IRepository<MacroeconomicVariable, int> _lookup_macroeconomicVariableRepository;


        public AffiliateMacroEconomicVariableOffsetsAppService(IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroEconomicVariableOffsetRepository, IAffiliateMacroEconomicVariableOffsetsExcelExporter affiliateMacroEconomicVariableOffsetsExcelExporter, IRepository<OrganizationUnit, long> lookup_organizationUnitRepository, IRepository<MacroeconomicVariable, int> lookup_macroeconomicVariableRepository)
        {
            _affiliateMacroEconomicVariableOffsetRepository = affiliateMacroEconomicVariableOffsetRepository;
            _affiliateMacroEconomicVariableOffsetsExcelExporter = affiliateMacroEconomicVariableOffsetsExcelExporter;
            _lookup_organizationUnitRepository = lookup_organizationUnitRepository;
            _lookup_macroeconomicVariableRepository = lookup_macroeconomicVariableRepository;

        }

        public async Task<PagedResultDto<GetAffiliateMacroEconomicVariableOffsetForViewDto>> GetAll(GetAllAffiliateMacroEconomicVariableOffsetsInput input)
        {

            var filteredAffiliateMacroEconomicVariableOffsets = _affiliateMacroEconomicVariableOffsetRepository.GetAll()
                        .Include(e => e.AffiliateFk)
                        .Include(e => e.MacroeconomicVariableFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => (e.AffiliateFk != null && e.AffiliateFk.DisplayName.ToLower().Contains(input.Filter.ToLower())) || (e.MacroeconomicVariableFk != null && e.MacroeconomicVariableFk.Name.ToLower().Contains(input.Filter.ToLower())))
                        .WhereIf(input.MinBackwardOffsetFilter != null, e => e.BackwardOffset >= input.MinBackwardOffsetFilter)
                        .WhereIf(input.MaxBackwardOffsetFilter != null, e => e.BackwardOffset <= input.MaxBackwardOffsetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.AffiliateFk != null && e.AffiliateFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MacroeconomicVariableNameFilter), e => e.MacroeconomicVariableFk != null && e.MacroeconomicVariableFk.Name == input.MacroeconomicVariableNameFilter);

            var pagedAndFilteredAffiliateMacroEconomicVariableOffsets = filteredAffiliateMacroEconomicVariableOffsets
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var affiliateMacroEconomicVariableOffsets = from o in pagedAndFilteredAffiliateMacroEconomicVariableOffsets
                                                        join o1 in _lookup_organizationUnitRepository.GetAll() on o.AffiliateId equals o1.Id into j1
                                                        from s1 in j1.DefaultIfEmpty()

                                                        join o2 in _lookup_macroeconomicVariableRepository.GetAll() on o.MacroeconomicVariableId equals o2.Id into j2
                                                        from s2 in j2.DefaultIfEmpty()

                                                        select new GetAffiliateMacroEconomicVariableOffsetForViewDto()
                                                        {
                                                            AffiliateMacroEconomicVariableOffset = new AffiliateMacroEconomicVariableOffsetDto
                                                            {
                                                                BackwardOffset = o.BackwardOffset,
                                                                Id = o.Id
                                                            },
                                                            OrganizationUnitDisplayName = s1 == null ? "" : s1.DisplayName.ToString(),
                                                            MacroeconomicVariableName = s2 == null ? "" : s2.Name.ToString()
                                                        };

            var totalCount = await filteredAffiliateMacroEconomicVariableOffsets.CountAsync();

            return new PagedResultDto<GetAffiliateMacroEconomicVariableOffsetForViewDto>(
                totalCount,
                await affiliateMacroEconomicVariableOffsets.ToListAsync()
            );
        }

        public async Task<GetAffiliateMacroEconomicVariableOffsetForViewDto> GetAffiliateMacroEconomicVariableOffsetForView(int id)
        {
            var affiliateMacroEconomicVariableOffset = await _affiliateMacroEconomicVariableOffsetRepository.GetAsync(id);

            var output = new GetAffiliateMacroEconomicVariableOffsetForViewDto { AffiliateMacroEconomicVariableOffset = ObjectMapper.Map<AffiliateMacroEconomicVariableOffsetDto>(affiliateMacroEconomicVariableOffset) };

            if (output.AffiliateMacroEconomicVariableOffset.AffiliateId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.AffiliateMacroEconomicVariableOffset.AffiliateId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit.DisplayName.ToString();
            }

            if (output.AffiliateMacroEconomicVariableOffset.MacroeconomicVariableId != null)
            {
                var _lookupMacroeconomicVariable = await _lookup_macroeconomicVariableRepository.FirstOrDefaultAsync((int)output.AffiliateMacroEconomicVariableOffset.MacroeconomicVariableId);
                output.MacroeconomicVariableName = _lookupMacroeconomicVariable.Name.ToString();
            }

            return output;
        }

        public async Task<GetAffiliateMacroEconomicVariableOffsetForEditOutput> GetAffiliateMacroEconomicVariableOffsetForEdit(EntityDto input)
        {
            var affiliateMacroEconomicVariableOffset = await _affiliateMacroEconomicVariableOffsetRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAffiliateMacroEconomicVariableOffsetForEditOutput { AffiliateMacroEconomicVariableOffset = ObjectMapper.Map<CreateOrEditAffiliateMacroEconomicVariableOffsetDto>(affiliateMacroEconomicVariableOffset) };

            if (output.AffiliateMacroEconomicVariableOffset.AffiliateId != null)
            {
                var _lookupOrganizationUnit = await _lookup_organizationUnitRepository.FirstOrDefaultAsync((long)output.AffiliateMacroEconomicVariableOffset.AffiliateId);
                output.OrganizationUnitDisplayName = _lookupOrganizationUnit.DisplayName.ToString();
            }

            if (output.AffiliateMacroEconomicVariableOffset.MacroeconomicVariableId != null)
            {
                var _lookupMacroeconomicVariable = await _lookup_macroeconomicVariableRepository.FirstOrDefaultAsync((int)output.AffiliateMacroEconomicVariableOffset.MacroeconomicVariableId);
                output.MacroeconomicVariableName = _lookupMacroeconomicVariable.Name.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAffiliateMacroEconomicVariableOffsetDto input)
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

        protected virtual async Task Create(CreateOrEditAffiliateMacroEconomicVariableOffsetDto input)
        {
            var affiliateMacroEconomicVariableOffset = ObjectMapper.Map<AffiliateMacroEconomicVariableOffset>(input);



            await _affiliateMacroEconomicVariableOffsetRepository.InsertAsync(affiliateMacroEconomicVariableOffset);
        }

        protected virtual async Task Update(CreateOrEditAffiliateMacroEconomicVariableOffsetDto input)
        {
            var affiliateMacroEconomicVariableOffset = await _affiliateMacroEconomicVariableOffsetRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, affiliateMacroEconomicVariableOffset);
        }

        public async Task Delete(EntityDto input)
        {
            await _affiliateMacroEconomicVariableOffsetRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetAffiliateMacroEconomicVariableOffsetsToExcel(GetAllAffiliateMacroEconomicVariableOffsetsForExcelInput input)
        {

            var filteredAffiliateMacroEconomicVariableOffsets = _affiliateMacroEconomicVariableOffsetRepository.GetAll()
                        .Include(e => e.AffiliateFk)
                        .Include(e => e.MacroeconomicVariableFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinBackwardOffsetFilter != null, e => e.BackwardOffset >= input.MinBackwardOffsetFilter)
                        .WhereIf(input.MaxBackwardOffsetFilter != null, e => e.BackwardOffset <= input.MaxBackwardOffsetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OrganizationUnitDisplayNameFilter), e => e.AffiliateFk != null && e.AffiliateFk.DisplayName == input.OrganizationUnitDisplayNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MacroeconomicVariableNameFilter), e => e.MacroeconomicVariableFk != null && e.MacroeconomicVariableFk.Name == input.MacroeconomicVariableNameFilter);

            var query = (from o in filteredAffiliateMacroEconomicVariableOffsets
                         join o1 in _lookup_organizationUnitRepository.GetAll() on o.AffiliateId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_macroeconomicVariableRepository.GetAll() on o.MacroeconomicVariableId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetAffiliateMacroEconomicVariableOffsetForViewDto()
                         {
                             AffiliateMacroEconomicVariableOffset = new AffiliateMacroEconomicVariableOffsetDto
                             {
                                 BackwardOffset = o.BackwardOffset,
                                 Id = o.Id
                             },
                             OrganizationUnitDisplayName = s1 == null ? "" : s1.DisplayName.ToString(),
                             MacroeconomicVariableName = s2 == null ? "" : s2.Name.ToString()
                         });


            var affiliateMacroEconomicVariableOffsetListDtos = await query.ToListAsync();

            return _affiliateMacroEconomicVariableOffsetsExcelExporter.ExportToFile(affiliateMacroEconomicVariableOffsetListDtos);
        }

        public async Task<PagedResultDto<AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_organizationUnitRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => (e.DisplayName != null ? e.DisplayName.ToString() : "").Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var organizationUnitList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto>();
            foreach (var organizationUnit in organizationUnitList)
            {
                lookupTableDtoList.Add(new AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto
                {
                    Id = organizationUnit.Id,
                    DisplayName = organizationUnit.DisplayName?.ToString()
                });
            }

            return new PagedResultDto<AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<PagedResultDto<AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableDto>> GetAllMacroeconomicVariableForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_macroeconomicVariableRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => (e.Name != null ? e.Name.ToString() : "").Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var macroeconomicVariableList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableDto>();
            foreach (var macroeconomicVariable in macroeconomicVariableList)
            {
                lookupTableDtoList.Add(new AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableDto
                {
                    Id = macroeconomicVariable.Id,
                    DisplayName = macroeconomicVariable.Name?.ToString()
                });
            }

            return new PagedResultDto<AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}