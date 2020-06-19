
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
using TestDemo.CalibrationResult;
using TestDemo.Calibration.Exporting;

namespace TestDemo.EclShared
{
    public class PdInputAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IPdInputAssumptionMacroeconomicProjectionsAppService
    {
        private readonly IRepository<PdInputAssumptionMacroeconomicProjection, Guid> _pdInputAssumptionMacroeconomicProjectionRepository;
        private readonly IRepository<MacroResult_SelectedMacroEconomicVariables> _selectedMacroVariablesRepository;
        private readonly IRepository<MacroeconomicVariable> _macroVariablesRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;
        private readonly IMacroProjectionDataTemplateExporter _templateExporter;


        public PdInputAssumptionMacroeconomicProjectionsAppService(
            IRepository<PdInputAssumptionMacroeconomicProjection, Guid> pdInputAssumptionMacroeconomicProjectionRepository,
            IRepository<MacroResult_SelectedMacroEconomicVariables> selectedMacroVariablesRepository,
            IRepository<MacroeconomicVariable> macroVariablesRepository,
            IMacroProjectionDataTemplateExporter templateeExporter,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputAssumptionMacroeconomicProjectionRepository = pdInputAssumptionMacroeconomicProjectionRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
            _selectedMacroVariablesRepository = selectedMacroVariablesRepository;
            _macroVariablesRepository = macroVariablesRepository;
            _templateExporter = templateeExporter;
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

        public async Task<FileDto> GetProjectionTemplate(EntityDto input)
        {
            var filter = _selectedMacroVariablesRepository.GetAll().Where(e => e.AffiliateId == input.Id);
            var query = from s in filter
                        join m in _macroVariablesRepository.GetAll() on s.MacroeconomicVariableId equals m.Id into m1
                        from m2 in m1.DefaultIfEmpty()
                        select m2 == null ? "" : m2.Name;

            var items = await query.OrderBy(e => e).ToListAsync();

            return _templateExporter.ExportProjectionTemplateToFile(items);
        }
    }
}