
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
    public class LgdAssumptionUnsecuredRecoveriesAppService : TestDemoAppServiceBase, ILgdAssumptionUnsecuredRecoveriesAppService
    {
        private readonly IRepository<LgdInputAssumption, Guid> _lgdAssumptionUnsecuredRecoveryRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public LgdAssumptionUnsecuredRecoveriesAppService(IRepository<LgdInputAssumption, Guid> lgdAssumptionUnsecuredRecoveryRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _lgdAssumptionUnsecuredRecoveryRepository = lgdAssumptionUnsecuredRecoveryRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetLgdAssumptionUnsecuredRecoveryForViewDto>> GetAll(GetAllLgdAssumptionUnsecuredRecoveriesInput input)
        {

            var filteredLgdAssumptionUnsecuredRecoveries = _lgdAssumptionUnsecuredRecoveryRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter), e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

            var pagedAndFilteredLgdAssumptionUnsecuredRecoveries = filteredLgdAssumptionUnsecuredRecoveries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var lgdAssumptionUnsecuredRecoveries = from o in pagedAndFilteredLgdAssumptionUnsecuredRecoveries
                                                   select new GetLgdAssumptionUnsecuredRecoveryForViewDto()
                                                   {
                                                       LgdAssumptionUnsecuredRecovery = new LgdAssumptionDto
                                                       {
                                                           InputName = o.InputName,
                                                           Value = o.Value,
                                                           Id = o.Id
                                                       }
                                                   };

            var totalCount = await filteredLgdAssumptionUnsecuredRecoveries.CountAsync();

            return new PagedResultDto<GetLgdAssumptionUnsecuredRecoveryForViewDto>(
                totalCount,
                await lgdAssumptionUnsecuredRecoveries.ToListAsync()
            );
        }

        public async Task<GetLgdAssumptionUnsecuredRecoveryForEditOutput> GetLgdAssumptionUnsecuredRecoveryForEdit(EntityDto<Guid> input)
        {
            var lgdAssumptionUnsecuredRecovery = await _lgdAssumptionUnsecuredRecoveryRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetLgdAssumptionUnsecuredRecoveryForEditOutput { LgdAssumptionUnsecuredRecovery = ObjectMapper.Map<CreateOrEditLgdAssumptionUnsecuredRecoveryDto>(lgdAssumptionUnsecuredRecovery) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
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

        protected virtual async Task Create(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
        {
            var lgdAssumptionUnsecuredRecovery = ObjectMapper.Map<LgdInputAssumption>(input);



            await _lgdAssumptionUnsecuredRecoveryRepository.InsertAsync(lgdAssumptionUnsecuredRecovery);
        }

        protected virtual async Task Update(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input)
        {
            var lgdAssumptionUnsecuredRecovery = await _lgdAssumptionUnsecuredRecoveryRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, lgdAssumptionUnsecuredRecovery);

            ObjectMapper.Map(input, lgdAssumptionUnsecuredRecovery);
        }

        private async Task SumbitForApproval(CreateOrEditLgdAssumptionUnsecuredRecoveryDto input, LgdInputAssumption assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.LgdInputAssumption,
                AssumptionGroup = L(assumption.LgdGroup.ToString()),
                InputName = assumption.InputName,
                OldValue = assumption.Value,
                NewValue = input.Value,
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.LgdInputAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _lgdAssumptionUnsecuredRecoveryRepository.DeleteAsync(input.Id);
        }

        public async Task UpdateStatus(UpdateAssumptionStatusDto input)
        {
            var assumption = await _lgdAssumptionUnsecuredRecoveryRepository.FirstOrDefaultAsync((Guid)input.Id);
            assumption.Status = input.Status;
            ObjectMapper.Map(assumption, assumption);
        }
    }
}