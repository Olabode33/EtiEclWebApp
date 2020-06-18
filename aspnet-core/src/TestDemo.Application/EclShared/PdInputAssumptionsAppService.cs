
using TestDemo.EclShared;
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
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclShared
{
    public class PdInputAssumptionsAppService : TestDemoAppServiceBase, IPdInputAssumptionsAppService
    {
        private readonly IRepository<PdInputAssumption, Guid> _pdInputAssumptionRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public PdInputAssumptionsAppService(IRepository<PdInputAssumption, Guid> pdInputAssumptionRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputAssumptionRepository = pdInputAssumptionRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetPdInputAssumptionForViewDto>> GetAll(GetAllPdInputAssumptionsInput input)
        {

            var filteredPdInputAssumptions = _pdInputAssumptionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

            var pagedAndFilteredPdInputAssumptions = filteredPdInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pdInputAssumptions = from o in pagedAndFilteredPdInputAssumptions
                                     select new GetPdInputAssumptionForViewDto()
                                     {
                                         PdInputAssumption = new PdInputAssumptionDto
                                         {
                                             Id = o.Id
                                         }
                                     };

            var totalCount = await filteredPdInputAssumptions.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionForViewDto>(
                totalCount,
                await pdInputAssumptions.ToListAsync()
            );
        }

        public async Task<GetPdInputAssumptionForEditOutput> GetPdInputAssumptionForEdit(EntityDto<Guid> input)
        {
            var pdInputAssumption = await _pdInputAssumptionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPdInputAssumptionForEditOutput { PdInputAssumption = ObjectMapper.Map<CreateOrEditPdInputAssumptionDto>(pdInputAssumption) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPdInputAssumptionDto input)
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

        protected virtual async Task Create(CreateOrEditPdInputAssumptionDto input)
        {
            var pdInputAssumption = ObjectMapper.Map<PdInputAssumption>(input);



            await _pdInputAssumptionRepository.InsertAsync(pdInputAssumption);
        }

        protected virtual async Task Update(CreateOrEditPdInputAssumptionDto input)
        {
            var pdInputAssumption = await _pdInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, pdInputAssumption);

            ObjectMapper.Map(input, pdInputAssumption);
        }

        private async Task SumbitForApproval(CreateOrEditPdInputAssumptionDto input, PdInputAssumption assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.PdInputAssumption,
                AssumptionGroup = L(assumption.PdGroup.ToString()),
                InputName = "Credit: " + assumption.InputName,
                OldValue = assumption.Value,
                NewValue = input.Value,
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdInputAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _pdInputAssumptionRepository.DeleteAsync(input.Id);
        }
    }
}