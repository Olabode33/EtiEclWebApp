
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
    public class PdInputAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IPdInputAssumptionMacroeconomicProjectionsAppService
    {
        private readonly IRepository<PdInputAssumptionMacroeconomicProjection, Guid> _pdInputAssumptionMacroeconomicProjectionRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public PdInputAssumptionMacroeconomicProjectionsAppService(IRepository<PdInputAssumptionMacroeconomicProjection, Guid> pdInputAssumptionMacroeconomicProjectionRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputAssumptionMacroeconomicProjectionRepository = pdInputAssumptionMacroeconomicProjectionRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetPdInputAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllPdInputAssumptionMacroeconomicProjectionsInput input)
        {

            var filteredPdInputAssumptionMacroeconomicProjections = _pdInputAssumptionMacroeconomicProjectionRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

            var pagedAndFilteredPdInputAssumptionMacroeconomicProjections = filteredPdInputAssumptionMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pdInputAssumptionMacroeconomicProjections = from o in pagedAndFilteredPdInputAssumptionMacroeconomicProjections
                                                            select new GetPdInputAssumptionMacroeconomicProjectionForViewDto()
                                                            {
                                                                PdInputAssumptionMacroeconomicProjection = new PdInputAssumptionMacroeconomicProjectionDto
                                                                {
                                                                    Id = o.Id
                                                                }
                                                            };

            var totalCount = await filteredPdInputAssumptionMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionMacroeconomicProjectionForViewDto>(
                totalCount,
                await pdInputAssumptionMacroeconomicProjections.ToListAsync()
            );
        }

        public async Task<GetPdInputAssumptionMacroeconomicProjectionForEditOutput> GetPdInputAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input)
        {
            var pdInputAssumptionMacroeconomicProjection = await _pdInputAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPdInputAssumptionMacroeconomicProjectionForEditOutput { PdInputAssumptionMacroeconomicProjection = ObjectMapper.Map<CreateOrEditPdInputAssumptionMacroeconomicProjectionDto>(pdInputAssumptionMacroeconomicProjection) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
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

        protected virtual async Task Create(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
        {
            var pdInputAssumptionMacroeconomicProjection = ObjectMapper.Map<PdInputAssumptionMacroeconomicProjection>(input);



            await _pdInputAssumptionMacroeconomicProjectionRepository.InsertAsync(pdInputAssumptionMacroeconomicProjection);
        }

        protected virtual async Task Update(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input)
        {
            var pdInputAssumptionMacroeconomicProjection = await _pdInputAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, pdInputAssumptionMacroeconomicProjection);

            ObjectMapper.Map(input, pdInputAssumptionMacroeconomicProjection);
        }

        private async Task SumbitForApproval(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input, PdInputAssumptionMacroeconomicProjection assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.PdInputAssumption,
                AssumptionGroup = L("PdMacroeconomicProjection"),
                InputName = assumption.InputName + ": " + assumption.Date.ToShortDateString(),
                OldValue = "B: " + assumption.BestValue.ToString() + " | O: " + assumption.OptimisticValue.ToString() + " | D: " + assumption.DownturnValue.ToString(),
                NewValue = "B: " + input.BestValue.ToString() + " | O: " + input.OptimisticValue + " | D: " + input.DownturnValue.ToString(),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdMacroProjectionAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _pdInputAssumptionMacroeconomicProjectionRepository.DeleteAsync(input.Id);
        }
    }
}