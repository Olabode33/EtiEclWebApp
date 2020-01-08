using TestDemo.Investment;

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

namespace TestDemo.InvestmentAssumption
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions)]
    public class InvestmentPdInputMacroEconomicAssumptionsAppService : TestDemoAppServiceBase, IInvestmentPdInputMacroEconomicAssumptionsAppService
    {
		 private readonly IRepository<InvestmentPdInputMacroEconomicAssumption, Guid> _investmentPdInputMacroEconomicAssumptionRepository;
		 private readonly IRepository<InvestmentEcl,Guid> _lookup_investmentEclRepository;
		 

		  public InvestmentPdInputMacroEconomicAssumptionsAppService(IRepository<InvestmentPdInputMacroEconomicAssumption, Guid> investmentPdInputMacroEconomicAssumptionRepository , IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository) 
		  {
			_investmentPdInputMacroEconomicAssumptionRepository = investmentPdInputMacroEconomicAssumptionRepository;
			_lookup_investmentEclRepository = lookup_investmentEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentPdInputMacroEconomicAssumptionForViewDto>> GetAll(GetAllInvestmentPdInputMacroEconomicAssumptionsInput input)
         {
			
			var filteredInvestmentPdInputMacroEconomicAssumptions = _investmentPdInputMacroEconomicAssumptionRepository.GetAll()
						.Include( e => e.InvestmentEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter));

			var pagedAndFilteredInvestmentPdInputMacroEconomicAssumptions = filteredInvestmentPdInputMacroEconomicAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentPdInputMacroEconomicAssumptions = from o in pagedAndFilteredInvestmentPdInputMacroEconomicAssumptions
                         join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentPdInputMacroEconomicAssumptionForViewDto() {
							InvestmentPdInputMacroEconomicAssumption = new InvestmentPdInputMacroEconomicAssumptionDto
							{
                                Month = o.Month,
                                BestValue = o.BestValue,
                                OptimisticValue = o.OptimisticValue,
                                DownturnValue = o.DownturnValue,
                                Status = o.Status,
                                Id = o.Id
							},
                         	InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
						};

            var totalCount = await filteredInvestmentPdInputMacroEconomicAssumptions.CountAsync();

            return new PagedResultDto<GetInvestmentPdInputMacroEconomicAssumptionForViewDto>(
                totalCount,
                await investmentPdInputMacroEconomicAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Edit)]
		 public async Task<GetInvestmentPdInputMacroEconomicAssumptionForEditOutput> GetInvestmentPdInputMacroEconomicAssumptionForEdit(EntityDto<Guid> input)
         {
            var investmentPdInputMacroEconomicAssumption = await _investmentPdInputMacroEconomicAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentPdInputMacroEconomicAssumptionForEditOutput {InvestmentPdInputMacroEconomicAssumption = ObjectMapper.Map<CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto>(investmentPdInputMacroEconomicAssumption)};

		    if (output.InvestmentPdInputMacroEconomicAssumption.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentPdInputMacroEconomicAssumption.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto input)
         {
            var investmentPdInputMacroEconomicAssumption = ObjectMapper.Map<InvestmentPdInputMacroEconomicAssumption>(input);

			

            await _investmentPdInputMacroEconomicAssumptionRepository.InsertAsync(investmentPdInputMacroEconomicAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto input)
         {
            var investmentPdInputMacroEconomicAssumption = await _investmentPdInputMacroEconomicAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentPdInputMacroEconomicAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentPdInputMacroEconomicAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions)]
         public async Task<PagedResultDto<InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ReportingDate.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto>();
			foreach(var investmentEcl in investmentEclList){
				lookupTableDtoList.Add(new InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto
				{
					Id = investmentEcl.Id.ToString(),
					DisplayName = ""
				});
			}

            return new PagedResultDto<InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}