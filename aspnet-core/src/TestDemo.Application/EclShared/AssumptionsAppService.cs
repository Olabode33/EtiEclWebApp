
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
    public class AssumptionsAppService : TestDemoAppServiceBase, IAssumptionsAppService
    {
        private readonly IRepository<Assumption, Guid> _assumptionRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;

        public AssumptionsAppService(
            IRepository<Assumption, Guid> assumptionRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService
            )
        {
            _assumptionRepository = assumptionRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetAssumptionForViewDto>> GetAll(GetAllAssumptionsInput input)
        {

            var filteredAssumptions = _assumptionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter), e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

            var pagedAndFilteredAssumptions = filteredAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var assumptions = from o in pagedAndFilteredAssumptions
                              select new GetAssumptionForViewDto()
                              {
                                  Assumption = new AssumptionDto
                                  {
                                      InputName = o.InputName,
                                      Value = o.Value,
                                      Id = o.Id
                                  }
                              };

            var totalCount = await filteredAssumptions.CountAsync();

            return new PagedResultDto<GetAssumptionForViewDto>(
                totalCount,
                await assumptions.ToListAsync()
            );
        }

        public async Task<GetAssumptionForEditOutput> GetAssumptionForEdit(EntityDto<Guid> input)
        {
            var assumption = await _assumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAssumptionForEditOutput { Assumption = ObjectMapper.Map<CreateOrEditAssumptionDto>(assumption) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAssumptionDto input)
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

        protected virtual async Task Create(CreateOrEditAssumptionDto input)
        {
            var assumption = ObjectMapper.Map<Assumption>(input);



            await _assumptionRepository.InsertAsync(assumption);
        }

        protected virtual async Task Update(CreateOrEditAssumptionDto input)
        {
            var assumption = await _assumptionRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, assumption);

            ObjectMapper.Map(input, assumption);
        }

        private async Task SumbitForApproval(CreateOrEditAssumptionDto input, Assumption assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.General,
                AssumptionGroup = L(assumption.AssumptionGroup.ToString()),
                InputName = assumption.InputName,
                OldValue = assumption.Value,
                NewValue = input.Value,
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.Assumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _assumptionRepository.DeleteAsync(input.Id);
        }

        public async Task UpdateStatus(UpdateAssumptionStatusDto input)
        {
            var assumption = await _assumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
            assumption.Status = input.Status;
            ObjectMapper.Map(assumption, assumption);
        }
    }
}