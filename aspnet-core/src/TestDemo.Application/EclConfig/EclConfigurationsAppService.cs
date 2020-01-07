
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclConfig.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclConfig
{
	[AbpAuthorize(AppPermissions.Pages_EclConfigurations)]
    public class EclConfigurationsAppService : TestDemoAppServiceBase, IEclConfigurationsAppService
    {
		 private readonly IRepository<EclConfiguration> _eclConfigurationRepository;
		 

		  public EclConfigurationsAppService(IRepository<EclConfiguration> eclConfigurationRepository ) 
		  {
			_eclConfigurationRepository = eclConfigurationRepository;
			
		  }

		 public async Task<PagedResultDto<GetEclConfigurationForViewDto>> GetAll(GetAllEclConfigurationsInput input)
         {
			var dataTypeFilter = (DataTypeEnum) input.DataTypeFilter;
			
			var filteredEclConfigurations = _eclConfigurationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PropertyKey.Contains(input.Filter) || e.DisplayName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(input.DataTypeFilter > -1, e => e.DataType == dataTypeFilter);

			var pagedAndFilteredEclConfigurations = filteredEclConfigurations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var eclConfigurations = from o in pagedAndFilteredEclConfigurations
                         select new GetEclConfigurationForViewDto() {
							EclConfiguration = new EclConfigurationDto
							{
                                DisplayName = o.DisplayName,
                                Value = o.Value,
                                DataType = o.DataType,
                                Id = o.Id
							}
						};

            var totalCount = await filteredEclConfigurations.CountAsync();

            return new PagedResultDto<GetEclConfigurationForViewDto>(
                totalCount,
                await eclConfigurations.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_EclConfigurations_Edit)]
		 public async Task<GetEclConfigurationForEditOutput> GetEclConfigurationForEdit(EntityDto input)
         {
            var eclConfiguration = await _eclConfigurationRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetEclConfigurationForEditOutput {EclConfiguration = ObjectMapper.Map<CreateOrEditEclConfigurationDto>(eclConfiguration)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditEclConfigurationDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_EclConfigurations_Create)]
		 protected virtual async Task Create(CreateOrEditEclConfigurationDto input)
         {
            var eclConfiguration = ObjectMapper.Map<EclConfiguration>(input);

			

            await _eclConfigurationRepository.InsertAsync(eclConfiguration);
         }

		 [AbpAuthorize(AppPermissions.Pages_EclConfigurations_Edit)]
		 protected virtual async Task Update(CreateOrEditEclConfigurationDto input)
         {
            var eclConfiguration = await _eclConfigurationRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, eclConfiguration);
         }

		 [AbpAuthorize(AppPermissions.Pages_EclConfigurations_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _eclConfigurationRepository.DeleteAsync(input.Id);
         } 
    }
}