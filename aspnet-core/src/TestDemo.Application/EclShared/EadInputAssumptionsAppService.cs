
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
    public class EadInputAssumptionsAppService : TestDemoAppServiceBase, IEadInputAssumptionsAppService
    {
        private readonly IRepository<EadInputAssumption, Guid> _eadInputAssumptionRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public EadInputAssumptionsAppService(IRepository<EadInputAssumption, Guid> eadInputAssumptionRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _eadInputAssumptionRepository = eadInputAssumptionRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetEadInputAssumptionForViewDto>> GetAll(GetAllEadInputAssumptionsInput input)
        {

            var filteredEadInputAssumptions = _eadInputAssumptionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter), e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

            var pagedAndFilteredEadInputAssumptions = filteredEadInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var eadInputAssumptions = from o in pagedAndFilteredEadInputAssumptions
                                      select new GetEadInputAssumptionForViewDto()
                                      {
                                          EadInputAssumption = new EadInputAssumptionDto
                                          {
                                              InputName = o.InputName,
                                              Value = o.Value,
                                              Id = o.Id
                                          }
                                      };

            var totalCount = await filteredEadInputAssumptions.CountAsync();

            return new PagedResultDto<GetEadInputAssumptionForViewDto>(
                totalCount,
                await eadInputAssumptions.ToListAsync()
            );
        }

        public async Task<GetEadInputAssumptionForEditOutput> GetEadInputAssumptionForEdit(EntityDto<Guid> input)
        {
            var eadInputAssumption = await _eadInputAssumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEadInputAssumptionForEditOutput { EadInputAssumption = ObjectMapper.Map<CreateOrEditEadInputAssumptionDto>(eadInputAssumption) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEadInputAssumptionDto input)
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

        protected virtual async Task Create(CreateOrEditEadInputAssumptionDto input)
        {
            var eadInputAssumption = ObjectMapper.Map<EadInputAssumption>(input);


            ////if (AbpSession.TenantId != null)
            ////{
            ////	eadInputAssumption.TenantId = (int?) AbpSession.TenantId;
            ////}


            await _eadInputAssumptionRepository.InsertAsync(eadInputAssumption);
        }

        protected virtual async Task Update(CreateOrEditEadInputAssumptionDto input)
        {
            var eadInputAssumption = await _eadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, eadInputAssumption);

            ObjectMapper.Map(input, eadInputAssumption);
        }

        private async Task SumbitForApproval(CreateOrEditEadInputAssumptionDto input, EadInputAssumption assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.EadInputAssumption,
                AssumptionGroup = L(assumption.EadGroup.ToString()),
                InputName = assumption.InputName,
                OldValue = assumption.Value,
                NewValue = input.Value,
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.EadInputAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _eadInputAssumptionRepository.DeleteAsync(input.Id);
        }

        public async Task UpdateStatus(UpdateAssumptionStatusDto input)
        {
            var assumption = await _eadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
            assumption.Status = input.Status;
            ObjectMapper.Map(assumption, assumption);
        }
    }
}