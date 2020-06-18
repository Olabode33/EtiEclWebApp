
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
    public class PdInputAssumptionNonInternalModelsAppService : TestDemoAppServiceBase, IPdInputAssumptionNonInternalModelsAppService
    {
        private readonly IRepository<PdInputAssumptionNonInternalModel, Guid> _pdInputAssumptionNonInternalModelRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public PdInputAssumptionNonInternalModelsAppService(IRepository<PdInputAssumptionNonInternalModel, Guid> pdInputAssumptionNonInternalModelRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputAssumptionNonInternalModelRepository = pdInputAssumptionNonInternalModelRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetPdInputAssumptionNonInternalModelForViewDto>> GetAll(GetAllPdInputAssumptionNonInternalModelsInput input)
        {

            var filteredPdInputAssumptionNonInternalModels = _pdInputAssumptionNonInternalModelRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

            var pagedAndFilteredPdInputAssumptionNonInternalModels = filteredPdInputAssumptionNonInternalModels
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pdInputAssumptionNonInternalModels = from o in pagedAndFilteredPdInputAssumptionNonInternalModels
                                                     select new GetPdInputAssumptionNonInternalModelForViewDto()
                                                     {
                                                         PdInputAssumptionNonInternalModel = new PdInputAssumptionNonInternalModelDto
                                                         {
                                                             Id = o.Id
                                                         }
                                                     };

            var totalCount = await filteredPdInputAssumptionNonInternalModels.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionNonInternalModelForViewDto>(
                totalCount,
                await pdInputAssumptionNonInternalModels.ToListAsync()
            );
        }

        public async Task<GetPdInputAssumptionNonInternalModelForEditOutput> GetPdInputAssumptionNonInternalModelForEdit(EntityDto<Guid> input)
        {
            var pdInputAssumptionNonInternalModel = await _pdInputAssumptionNonInternalModelRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPdInputAssumptionNonInternalModelForEditOutput { PdInputAssumptionNonInternalModel = ObjectMapper.Map<CreateOrEditPdInputAssumptionNonInternalModelDto>(pdInputAssumptionNonInternalModel) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPdInputAssumptionNonInternalModelDto input)
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
        
        protected virtual async Task Create(CreateOrEditPdInputAssumptionNonInternalModelDto input)
        {
            var pdInputAssumptionNonInternalModel = ObjectMapper.Map<PdInputAssumptionNonInternalModel>(input);



            await _pdInputAssumptionNonInternalModelRepository.InsertAsync(pdInputAssumptionNonInternalModel);
        }

        protected virtual async Task Update(CreateOrEditPdInputAssumptionNonInternalModelDto input)
        {
            var pdInputAssumptionNonInternalModel = await _pdInputAssumptionNonInternalModelRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, pdInputAssumptionNonInternalModel);

            ObjectMapper.Map(input, pdInputAssumptionNonInternalModel);
        }

        private async Task SumbitForApproval(CreateOrEditPdInputAssumptionNonInternalModelDto input, PdInputAssumptionNonInternalModel assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.PdInputAssumption,
                AssumptionGroup = L("PdMacroeconomicInput"),
                InputName = assumption.PdGroup + ": Month " + assumption.Month.ToString(),
                OldValue = assumption.MarginalDefaultRate.ToString(),
                NewValue = input.MarginalDefaultRate.ToString(),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdNonInternalAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _pdInputAssumptionNonInternalModelRepository.DeleteAsync(input.Id);
        }
    }
}