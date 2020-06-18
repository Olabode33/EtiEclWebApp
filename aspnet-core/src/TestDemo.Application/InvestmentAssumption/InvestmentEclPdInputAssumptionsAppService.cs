using TestDemo.Investment;

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
    public class InvestmentEclPdInputAssumptionsAppService : TestDemoAppServiceBase, IInvestmentEclPdInputAssumptionsAppService
    {
		 private readonly IRepository<InvestmentEclPdInputAssumption, Guid> _investmentEclPdInputAssumptionRepository;
		 private readonly IRepository<InvestmentEcl,Guid> _lookup_investmentEclRepository;
		 

		  public InvestmentEclPdInputAssumptionsAppService(IRepository<InvestmentEclPdInputAssumption, Guid> investmentEclPdInputAssumptionRepository , IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository) 
		  {
			_investmentEclPdInputAssumptionRepository = investmentEclPdInputAssumptionRepository;
			_lookup_investmentEclRepository = lookup_investmentEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetInvestmentEclPdInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclPdInputAssumptionsInput input)
         {
			
			var filteredInvestmentEclPdInputAssumptions = _investmentEclPdInputAssumptionRepository.GetAll()
						.Include( e => e.InvestmentEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredInvestmentEclPdInputAssumptions = filteredInvestmentEclPdInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var investmentEclPdInputAssumptions = from o in pagedAndFilteredInvestmentEclPdInputAssumptions
                         join o1 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetInvestmentEclPdInputAssumptionForViewDto() {
							InvestmentEclPdInputAssumption = new InvestmentEclPdInputAssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                Status = o.Status,
                                Id = o.Id
							},
                         	InvestmentEclReportingDate = s1 == null ? "" : s1.ReportingDate.ToString()
						};

            var totalCount = await filteredInvestmentEclPdInputAssumptions.CountAsync();

            return new PagedResultDto<GetInvestmentEclPdInputAssumptionForViewDto>(
                totalCount,
                await investmentEclPdInputAssumptions.ToListAsync()
            );
         }

        public async Task<List<PdInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _investmentEclPdInputAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == input.Id)
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
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

		 public async Task<GetInvestmentEclPdInputAssumptionForEditOutput> GetInvestmentEclPdInputAssumptionForEdit(EntityDto<Guid> input)
         {
            var investmentEclPdInputAssumption = await _investmentEclPdInputAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvestmentEclPdInputAssumptionForEditOutput {InvestmentEclPdInputAssumption = ObjectMapper.Map<CreateOrEditInvestmentEclPdInputAssumptionDto>(investmentEclPdInputAssumption)};

		    if (output.InvestmentEclPdInputAssumption.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclPdInputAssumption.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvestmentEclPdInputAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditInvestmentEclPdInputAssumptionDto input)
         {
            var investmentEclPdInputAssumption = ObjectMapper.Map<InvestmentEclPdInputAssumption>(input);

			

            await _investmentEclPdInputAssumptionRepository.InsertAsync(investmentEclPdInputAssumption);
         }

		 protected virtual async Task Update(CreateOrEditInvestmentEclPdInputAssumptionDto input)
         {
            var investmentEclPdInputAssumption = await _investmentEclPdInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, investmentEclPdInputAssumption);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _investmentEclPdInputAssumptionRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<InvestmentEclPdInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.ReportingDate.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<InvestmentEclPdInputAssumptionInvestmentEclLookupTableDto>();
			foreach(var investmentEcl in investmentEclList){
				lookupTableDtoList.Add(new InvestmentEclPdInputAssumptionInvestmentEclLookupTableDto
				{
					Id = investmentEcl.Id.ToString(),
					DisplayName = ""
				});
			}

            return new PagedResultDto<InvestmentEclPdInputAssumptionInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}