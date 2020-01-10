using TestDemo.Investment;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.InvestmentAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.InvestmentAssumption
{
    [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions)]
    public class InvestmentEclEadInputAssumptionsAppService : TestDemoAppServiceBase, IInvestmentEclEadInputAssumptionsAppService
    {
        private readonly IRepository<InvestmentEclEadInputAssumption, Guid> _investmentEclEadInputAssumptionRepository;
        private readonly IRepository<InvestmentEcl, Guid> _lookup_investmentEclRepository;


        public InvestmentEclEadInputAssumptionsAppService(IRepository<InvestmentEclEadInputAssumption, Guid> investmentEclEadInputAssumptionRepository, IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository)
        {
            _investmentEclEadInputAssumptionRepository = investmentEclEadInputAssumptionRepository;
            _lookup_investmentEclRepository = lookup_investmentEclRepository;

        }

        public async Task<PagedResultDto<GetInvestmentEclEadInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclEadInputAssumptionsInput input)
        {

            var filteredInvestmentEclEadInputAssumptions = _investmentEclEadInputAssumptionRepository.GetAll()
                        .Include(e => e.InvestmentEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

            var pagedAndFilteredInvestmentEclEadInputAssumptions = filteredInvestmentEclEadInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentEclEadInputAssumptions = from o in pagedAndFilteredInvestmentEclEadInputAssumptions
                                                   join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                                                   from s1 in j1.DefaultIfEmpty()

                                                   select new GetInvestmentEclEadInputAssumptionForViewDto()
                                                   {
                                                       InvestmentEclEadInputAssumption = new InvestmentEclEadInputAssumptionDto
                                                       {
                                                           Key = o.Key,
                                                           InputName = o.InputName,
                                                           Value = o.Value,
                                                           Id = o.Id
                                                       },
                                                       InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
                                                   };

            var totalCount = await filteredInvestmentEclEadInputAssumptions.CountAsync();

            return new PagedResultDto<GetInvestmentEclEadInputAssumptionForViewDto>(
                totalCount,
                await investmentEclEadInputAssumptions.ToListAsync()
            );
        }

        public async Task<List<EadInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _investmentEclEadInputAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == input.Id)
                                                              .Select(x => new EadInputAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.EadGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  //Status = x.s,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Edit)]
        public async Task<GetInvestmentEclEadInputAssumptionForEditOutput> GetInvestmentEclEadInputAssumptionForEdit(EntityDto<Guid> input)
        {
            var investmentEclEadInputAssumption = await _investmentEclEadInputAssumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvestmentEclEadInputAssumptionForEditOutput { InvestmentEclEadInputAssumption = ObjectMapper.Map<CreateOrEditInvestmentEclEadInputAssumptionDto>(investmentEclEadInputAssumption) };

            if (output.InvestmentEclEadInputAssumption.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclEadInputAssumption.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvestmentEclEadInputAssumptionDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Create)]
        protected virtual async Task Create(CreateOrEditInvestmentEclEadInputAssumptionDto input)
        {
            var investmentEclEadInputAssumption = ObjectMapper.Map<InvestmentEclEadInputAssumption>(input);



            await _investmentEclEadInputAssumptionRepository.InsertAsync(investmentEclEadInputAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Edit)]
        protected virtual async Task Update(CreateOrEditInvestmentEclEadInputAssumptionDto input)
        {
            var investmentEclEadInputAssumption = await _investmentEclEadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, investmentEclEadInputAssumption);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclEadInputAssumptionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclEadInputAssumptions)]
        public async Task<PagedResultDto<InvestmentEclEadInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ReportingDate.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentEclEadInputAssumptionInvestmentEclLookupTableDto>();
            foreach (var investmentEcl in investmentEclList)
            {
                lookupTableDtoList.Add(new InvestmentEclEadInputAssumptionInvestmentEclLookupTableDto
                {
                    Id = investmentEcl.Id.ToString(),
                    DisplayName = ""
                });
            }

            return new PagedResultDto<InvestmentEclEadInputAssumptionInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}