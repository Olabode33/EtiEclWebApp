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
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.InvestmentAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.InvestmentAssumption
{
	[AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates)]
    public class InvestmentEclPdFitchDefaultRatesAppService : TestDemoAppServiceBase, IInvestmentEclPdFitchDefaultRatesAppService
    {
		 private readonly IRepository<InvestmentEclPdFitchDefaultRate, Guid> _investmentEclPdFitchDefaultRateRepository;
		 private readonly IRepository<InvestmentEcl,Guid> _lookup_investmentEclRepository;
		 

		  public InvestmentEclPdFitchDefaultRatesAppService(IRepository<InvestmentEclPdFitchDefaultRate, Guid> investmentEclPdFitchDefaultRateRepository , IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository) 
		  {
			_investmentEclPdFitchDefaultRateRepository = investmentEclPdFitchDefaultRateRepository;
			_lookup_investmentEclRepository = lookup_investmentEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclPdFitchDefaultRateForViewDto>> GetAll(GetAllInvestmentEclPdFitchDefaultRatesInput input)
         {
			
			var filteredInvestmentEclPdFitchDefaultRates = _investmentEclPdFitchDefaultRateRepository.GetAll()
						.Include( e => e.InvestmentEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter));

			var pagedAndFilteredInvestmentEclPdFitchDefaultRates = filteredInvestmentEclPdFitchDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclPdFitchDefaultRates = from o in pagedAndFilteredInvestmentEclPdFitchDefaultRates
                         join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclPdFitchDefaultRateForViewDto() {
							InvestmentEclPdFitchDefaultRate = new InvestmentEclPdFitchDefaultRateDto
							{
                                Rating = o.Rating,
                                Year = o.Year,
                                Value = o.Value,
                                Status = o.Status,
                                Id = o.Id
							},
                         	InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
						};

            var totalCount = await filteredInvestmentEclPdFitchDefaultRates.CountAsync();

            return new PagedResultDto<GetInvestmentEclPdFitchDefaultRateForViewDto>(
                totalCount,
                await investmentEclPdFitchDefaultRates.ToListAsync()
            );
         }

        public async Task<List<InvSecFitchCummulativeDefaultRateDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _investmentEclPdFitchDefaultRateRepository.GetAll().Where(x => x.InvestmentEclId == input.Id)
                                                              .Select(x => new InvSecFitchCummulativeDefaultRateDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Rating = x.Rating,
                                                                  Years = x.Year,
                                                                  Value = x.Value,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Edit)]
		 public async Task<GetInvestmentEclPdFitchDefaultRateForEditOutput> GetInvestmentEclPdFitchDefaultRateForEdit(EntityDto<Guid> input)
         {
            var investmentEclPdFitchDefaultRate = await _investmentEclPdFitchDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclPdFitchDefaultRateForEditOutput {InvestmentEclPdFitchDefaultRate = ObjectMapper.Map<CreateOrEditInvestmentEclPdFitchDefaultRateDto>(investmentEclPdFitchDefaultRate)};

		    if (output.InvestmentEclPdFitchDefaultRate.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclPdFitchDefaultRate.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclPdFitchDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditInvestmentEclPdFitchDefaultRateDto input)
         {
            var investmentEclPdFitchDefaultRate = ObjectMapper.Map<InvestmentEclPdFitchDefaultRate>(input);

			

            await _investmentEclPdFitchDefaultRateRepository.InsertAsync(investmentEclPdFitchDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditInvestmentEclPdFitchDefaultRateDto input)
         {
            var investmentEclPdFitchDefaultRate = await _investmentEclPdFitchDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclPdFitchDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclPdFitchDefaultRateRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates)]
         public async Task<PagedResultDto<InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ReportingDate.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto>();
			foreach(var investmentEcl in investmentEclList){
				lookupTableDtoList.Add(new InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto
				{
					Id = investmentEcl.Id.ToString(),
					DisplayName = ""
				});
			}

            return new PagedResultDto<InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}