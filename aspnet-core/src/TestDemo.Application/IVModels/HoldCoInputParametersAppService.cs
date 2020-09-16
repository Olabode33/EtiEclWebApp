

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.IVModels.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.IVModels
{
	//[AbpAuthorize(AppPermissions.Pages_HoldCoInputParameters)]
    public class HoldCoInputParametersAppService : TestDemoAppServiceBase, IHoldCoInputParametersAppService
    {
		 private readonly IRepository<HoldCoInputParameter, Guid> _holdCoInputParameterRepository;
		 

		  public HoldCoInputParametersAppService(IRepository<HoldCoInputParameter, Guid> holdCoInputParameterRepository ) 
		  {
			_holdCoInputParameterRepository = holdCoInputParameterRepository;
			
		  }

		 public async Task<PagedResultDto<GetHoldCoInputParameterForViewDto>> GetAll(GetAllHoldCoInputParametersInput input)
         {
			
			var filteredHoldCoInputParameters = _holdCoInputParameterRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.AssumedRating.Contains(input.Filter) || e.DefaultLoanRating.Contains(input.Filter));

			var pagedAndFilteredHoldCoInputParameters = filteredHoldCoInputParameters
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var holdCoInputParameters = from o in pagedAndFilteredHoldCoInputParameters
                         select new GetHoldCoInputParameterForViewDto() {
							HoldCoInputParameter = new HoldCoInputParameterDto
							{
                                ValuationDate = o.ValuationDate,
                                Optimistic = o.Optimistic,
                                BestEstimate = o.BestEstimate,
                                Downturn = o.Downturn,
                                AssumedRating = o.AssumedRating,
                                DefaultLoanRating = o.DefaultLoanRating,
                                RecoveryRate = o.RecoveryRate,
                                AssumedStartDate = o.AssumedStartDate,
                                AssumedMaturityDate = o.AssumedMaturityDate,
                                Id = o.Id
							}
						};

            var totalCount = await filteredHoldCoInputParameters.CountAsync();

            return new PagedResultDto<GetHoldCoInputParameterForViewDto>(
                totalCount,
                await holdCoInputParameters.ToListAsync()
            );
         }
		 
		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInputParameters_Edit)]
		 public async Task<GetHoldCoInputParameterForEditOutput> GetHoldCoInputParameterForEdit(EntityDto<Guid> input)
         {
            var holdCoInputParameter = await _holdCoInputParameterRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHoldCoInputParameterForEditOutput {HoldCoInputParameter = ObjectMapper.Map<CreateOrEditHoldCoInputParameterDto>(holdCoInputParameter)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHoldCoInputParameterDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInputParameters_Create)]
		 protected virtual async Task Create(CreateOrEditHoldCoInputParameterDto input)
         {
            var holdCoInputParameter = ObjectMapper.Map<HoldCoInputParameter>(input);

			

            await _holdCoInputParameterRepository.InsertAsync(holdCoInputParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInputParameters_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoInputParameterDto input)
         {
            var holdCoInputParameter = await _holdCoInputParameterRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, holdCoInputParameter);
         }

		 //[AbpAuthorize(AppPermissions.Pages_HoldCoInputParameters_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _holdCoInputParameterRepository.DeleteAsync(input.Id);
         } 
    }
}