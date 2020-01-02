using TestDemo.Retail;

using TestDemo.EclShared;
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
    [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions)]
    public class RetailEclPdAssumptionsAppService : TestDemoAppServiceBase, IRetailEclPdAssumptionsAppService
    {
        private readonly IRepository<RetailEclPdAssumption, Guid> _retailEclPdAssumptionRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclPdAssumptionsAppService(IRepository<RetailEclPdAssumption, Guid> retailEclPdAssumptionRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclPdAssumptionRepository = retailEclPdAssumptionRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<PagedResultDto<GetRetailEclPdAssumptionForViewDto>> GetAll(GetAllRetailEclPdAssumptionsInput input)
        {

            var filteredRetailEclPdAssumptions = _retailEclPdAssumptionRepository.GetAll()
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

            var pagedAndFilteredRetailEclPdAssumptions = filteredRetailEclPdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclPdAssumptions = from o in pagedAndFilteredRetailEclPdAssumptions
                                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         select new GetRetailEclPdAssumptionForViewDto()
                                         {
                                             RetailEclPdAssumption = new RetailEclPdAssumptionDto
                                             {
                                                 Id = o.Id
                                             },
                                             RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
                                         };

            var totalCount = await filteredRetailEclPdAssumptions.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumptionForViewDto>(
                totalCount,
                await retailEclPdAssumptions.ToListAsync()
            );
        }

        public async Task<List<PdInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclPdAssumptionRepository.GetAll().Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new PdInputAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.PdGroup,
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

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions_Edit)]
        public async Task<GetRetailEclPdAssumptionForEditOutput> GetRetailEclPdAssumptionForEdit(EntityDto<Guid> input)
        {
            var retailEclPdAssumption = await _retailEclPdAssumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclPdAssumptionForEditOutput { RetailEclPdAssumption = ObjectMapper.Map<CreateOrEditRetailEclPdAssumptionDto>(retailEclPdAssumption) };

            if (output.RetailEclPdAssumption.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumption.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions_Create)]
        protected virtual async Task Create(CreateOrEditRetailEclPdAssumptionDto input)
        {
            var retailEclPdAssumption = ObjectMapper.Map<RetailEclPdAssumption>(input);



            await _retailEclPdAssumptionRepository.InsertAsync(retailEclPdAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclPdAssumptionDto input)
        {
            var retailEclPdAssumption = await _retailEclPdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclPdAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclPdAssumptionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptions)]
        public async Task<PagedResultDto<RetailEclPdAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclPdAssumptionRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclPdAssumptionRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclPdAssumptionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}