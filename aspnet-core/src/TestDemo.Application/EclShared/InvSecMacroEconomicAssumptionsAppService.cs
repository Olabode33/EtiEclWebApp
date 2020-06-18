
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclShared
{
    public class InvSecMacroEconomicAssumptionsAppService : TestDemoAppServiceBase, IInvSecMacroEconomicAssumptionsAppService
    {
		 private readonly IRepository<InvSecMacroEconomicAssumption, Guid> _invSecMacroEconomicAssumptionRepository;
		 

		  public InvSecMacroEconomicAssumptionsAppService(IRepository<InvSecMacroEconomicAssumption, Guid> invSecMacroEconomicAssumptionRepository ) 
		  {
			_invSecMacroEconomicAssumptionRepository = invSecMacroEconomicAssumptionRepository;
			
		  }

		 public async Task<PagedResultDto<GetInvSecMacroEconomicAssumptionForViewDto>> GetAll(GetAllInvSecMacroEconomicAssumptionsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredInvSecMacroEconomicAssumptions = _invSecMacroEconomicAssumptionRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter))
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);

			var pagedAndFilteredInvSecMacroEconomicAssumptions = filteredInvSecMacroEconomicAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var invSecMacroEconomicAssumptions = from o in pagedAndFilteredInvSecMacroEconomicAssumptions
                         select new GetInvSecMacroEconomicAssumptionForViewDto() {
							InvSecMacroEconomicAssumption = new InvSecMacroEconomicAssumptionDto
							{
                                Month = o.Month,
                                BestValue = o.BestValue,
                                OptimisticValue = o.OptimisticValue,
                                DownturnValue = o.DownturnValue,
                                Status = o.Status,
                                OrganizationUnitId = o.OrganizationUnitId,
                                Id = o.Id
							}
						};

            var totalCount = await filteredInvSecMacroEconomicAssumptions.CountAsync();

            return new PagedResultDto<GetInvSecMacroEconomicAssumptionForViewDto>(
                totalCount,
                await invSecMacroEconomicAssumptions.ToListAsync()
            );
         }
		 
		 public async Task<GetInvSecMacroEconomicAssumptionForEditOutput> GetInvSecMacroEconomicAssumptionForEdit(EntityDto<Guid> input)
         {
            var invSecMacroEconomicAssumption = await _invSecMacroEconomicAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetInvSecMacroEconomicAssumptionForEditOutput {InvSecMacroEconomicAssumption = ObjectMapper.Map<CreateOrEditInvSecMacroEconomicAssumptionDto>(invSecMacroEconomicAssumption)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditInvSecMacroEconomicAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditInvSecMacroEconomicAssumptionDto input)
         {
            var invSecMacroEconomicAssumption = ObjectMapper.Map<InvSecMacroEconomicAssumption>(input);

			

            await _invSecMacroEconomicAssumptionRepository.InsertAsync(invSecMacroEconomicAssumption);
         }

		 protected virtual async Task Update(CreateOrEditInvSecMacroEconomicAssumptionDto input)
         {
            var invSecMacroEconomicAssumption = await _invSecMacroEconomicAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, invSecMacroEconomicAssumption);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _invSecMacroEconomicAssumptionRepository.DeleteAsync(input.Id);
         } 
    }
}