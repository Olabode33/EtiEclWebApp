﻿
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
    public class PdInputAssumptionNplIndexesAppService : TestDemoAppServiceBase, IPdInputAssumptionNplIndexesAppService
    {
        private readonly IRepository<PdInputAssumptionNplIndex, Guid> _pdInputAssumptionNplIndexRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public PdInputAssumptionNplIndexesAppService(IRepository<PdInputAssumptionNplIndex, Guid> pdInputAssumptionNplIndexRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputAssumptionNplIndexRepository = pdInputAssumptionNplIndexRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetPdInputAssumptionNplIndexForViewDto>> GetAll(GetAllPdInputAssumptionNplIndexesInput input)
        {

            var filteredPdInputAssumptionNplIndexes = _pdInputAssumptionNplIndexRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter));

            var pagedAndFilteredPdInputAssumptionNplIndexes = filteredPdInputAssumptionNplIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pdInputAssumptionNplIndexes = from o in pagedAndFilteredPdInputAssumptionNplIndexes
                                              select new GetPdInputAssumptionNplIndexForViewDto()
                                              {
                                                  PdInputAssumptionNplIndex = new PdInputAssumptionNplIndexDto
                                                  {
                                                      Id = o.Id
                                                  }
                                              };

            var totalCount = await filteredPdInputAssumptionNplIndexes.CountAsync();

            return new PagedResultDto<GetPdInputAssumptionNplIndexForViewDto>(
                totalCount,
                await pdInputAssumptionNplIndexes.ToListAsync()
            );
        }

        public async Task<GetPdInputAssumptionNplIndexForEditOutput> GetPdInputAssumptionNplIndexForEdit(EntityDto<Guid> input)
        {
            var pdInputAssumptionNplIndex = await _pdInputAssumptionNplIndexRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPdInputAssumptionNplIndexForEditOutput { PdInputAssumptionNplIndex = ObjectMapper.Map<CreateOrEditPdInputAssumptionNplIndexDto>(pdInputAssumptionNplIndex) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPdInputAssumptionNplIndexDto input)
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

        protected virtual async Task Create(CreateOrEditPdInputAssumptionNplIndexDto input)
        {
            var pdInputAssumptionNplIndex = ObjectMapper.Map<PdInputAssumptionNplIndex>(input);



            await _pdInputAssumptionNplIndexRepository.InsertAsync(pdInputAssumptionNplIndex);
        }

        protected virtual async Task Update(CreateOrEditPdInputAssumptionNplIndexDto input)
        {
            var pdInputAssumptionNplIndex = await _pdInputAssumptionNplIndexRepository.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, pdInputAssumptionNplIndex);

            ObjectMapper.Map(input, pdInputAssumptionNplIndex);
        }

        private async Task SumbitForApproval(CreateOrEditPdInputAssumptionNplIndexDto input, PdInputAssumptionNplIndex assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.PdInputAssumption,
                AssumptionGroup = L("PdInputAssumptionNplIndex"),
                InputName = assumption.Date.ToShortDateString(),
                OldValue = assumption.Actual.ToString(),
                NewValue = input.Actual.ToString(),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdNplIndexAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _pdInputAssumptionNplIndexRepository.DeleteAsync(input.Id);
        }
    }
}