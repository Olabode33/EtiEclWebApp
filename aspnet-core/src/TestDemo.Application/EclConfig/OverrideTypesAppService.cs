

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
    [AbpAuthorize(AppPermissions.Pages_Configuration)]
    public class OverrideTypesAppService : TestDemoAppServiceBase, IOverrideTypesAppService
    {
        private readonly IRepository<OverrideType> _overrideTypeRepository;


        public OverrideTypesAppService(IRepository<OverrideType> overrideTypeRepository)
        {
            _overrideTypeRepository = overrideTypeRepository;

        }

        public async Task<PagedResultDto<GetOverrideTypeForViewDto>> GetAll(GetAllOverrideTypesInput input)
        {

            var filteredOverrideTypes = _overrideTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.Description.Contains(input.Filter));

            var pagedAndFilteredOverrideTypes = filteredOverrideTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var overrideTypes = from o in pagedAndFilteredOverrideTypes
                                select new GetOverrideTypeForViewDto()
                                {
                                    OverrideType = new OverrideTypeDto
                                    {
                                        Name = o.Name,
                                        Id = o.Id
                                    }
                                };

            var totalCount = await filteredOverrideTypes.CountAsync();

            return new PagedResultDto<GetOverrideTypeForViewDto>(
                totalCount,
                await overrideTypes.ToListAsync()
            );
        }

        public async Task<GetOverrideTypeForEditOutput> GetOverrideTypeForEdit(EntityDto input)
        {
            var overrideType = await _overrideTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetOverrideTypeForEditOutput { OverrideType = ObjectMapper.Map<CreateOrEditOverrideTypeDto>(overrideType) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditOverrideTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
        protected virtual async Task Create(CreateOrEditOverrideTypeDto input)
        {
            var overrideType = ObjectMapper.Map<OverrideType>(input);
            await _overrideTypeRepository.InsertAsync(overrideType);
        }

        [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
        protected virtual async Task Update(CreateOrEditOverrideTypeDto input)
        {
            var overrideType = await _overrideTypeRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, overrideType);
        }

        [AbpAuthorize(AppPermissions.Pages_Configuration_Update)]
        public async Task Delete(EntityDto input)
        {
            await _overrideTypeRepository.DeleteAsync(input.Id);
        }
    }
}