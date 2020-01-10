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
	[AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions)]
    public class InvestmentEclLgdInputAssumptionsAppService : TestDemoAppServiceBase, IInvestmentEclLgdInputAssumptionsAppService
    {
		 private readonly IRepository<InvestmentEclLgdInputAssumption, Guid> _investmentEclLgdInputAssumptionRepository;
		 private readonly IRepository<InvestmentEcl,Guid> _lookup_investmentEclRepository;
		 

		  public InvestmentEclLgdInputAssumptionsAppService(IRepository<InvestmentEclLgdInputAssumption, Guid> investmentEclLgdInputAssumptionRepository , IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository) 
		  {
			_investmentEclLgdInputAssumptionRepository = investmentEclLgdInputAssumptionRepository;
			_lookup_investmentEclRepository = lookup_investmentEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclLgdInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclLgdInputAssumptionsInput input)
         {
			
			var filteredInvestmentEclLgdInputAssumptions = _investmentEclLgdInputAssumptionRepository.GetAll()
						.Include( e => e.InvestmentEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredInvestmentEclLgdInputAssumptions = filteredInvestmentEclLgdInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclLgdInputAssumptions = from o in pagedAndFilteredInvestmentEclLgdInputAssumptions
                         join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclLgdInputAssumptionForViewDto() {
							InvestmentEclLgdInputAssumption = new InvestmentEclLgdInputAssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                Id = o.Id
							},
                         	InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
						};

            var totalCount = await filteredInvestmentEclLgdInputAssumptions.CountAsync();

            return new PagedResultDto<GetInvestmentEclLgdInputAssumptionForViewDto>(
                totalCount,
                await investmentEclLgdInputAssumptions.ToListAsync()
            );
         }

        public async Task<List<LgdAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _investmentEclLgdInputAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == input.Id)
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
                                                                  //Status = x.s,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Edit)]
		 public async Task<GetInvestmentEclLgdInputAssumptionForEditOutput> GetInvestmentEclLgdInputAssumptionForEdit(EntityDto<Guid> input)
         {
            var investmentEclLgdInputAssumption = await _investmentEclLgdInputAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclLgdInputAssumptionForEditOutput {InvestmentEclLgdInputAssumption = ObjectMapper.Map<CreateOrEditInvestmentEclLgdInputAssumptionDto>(investmentEclLgdInputAssumption)};

		    if (output.InvestmentEclLgdInputAssumption.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclLgdInputAssumption.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclLgdInputAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentEclLgdInputAssumptionDto input)
         {
            var investmentEclLgdInputAssumption = ObjectMapper.Map<InvestmentEclLgdInputAssumption>(input);

			

            await _investmentEclLgdInputAssumptionRepository.InsertAsync(investmentEclLgdInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclLgdInputAssumptionDto input)
         {
            var investmentEclLgdInputAssumption = await _investmentEclLgdInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclLgdInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclLgdInputAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclLgdInputAssumptions)]
         public async Task<PagedResultDto<InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ReportingDate.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto>();
			foreach(var investmentEcl in investmentEclList){
				lookupTableDtoList.Add(new InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto
				{
					Id = investmentEcl.Id.ToString(),
					DisplayName = ""
				});
			}

            return new PagedResultDto<InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}