

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
    public class PdInputSnPCummulativeDefaultRatesAppService : TestDemoAppServiceBase, IPdInputSnPCummulativeDefaultRatesAppService
    {
        private readonly IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> _pdInputSnPCummulativeDefaultRateRepository;
        private readonly IAssumptionApprovalsAppService _assumptionApprovalsAppService;


        public PdInputSnPCummulativeDefaultRatesAppService(IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> pdInputSnPCummulativeDefaultRateRepository,
            IAssumptionApprovalsAppService assumptionApprovalsAppService)
        {
            _pdInputSnPCummulativeDefaultRateRepository = pdInputSnPCummulativeDefaultRateRepository;
            _assumptionApprovalsAppService = assumptionApprovalsAppService;
        }

        public async Task<PagedResultDto<GetPdInputSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllPdInputSnPCummulativeDefaultRatesInput input)
        {

            var filteredPdInputSnPCummulativeDefaultRates = _pdInputSnPCummulativeDefaultRateRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RatingFilter), e => e.Rating.ToLower() == input.RatingFilter.ToLower().Trim());

            var pagedAndFilteredPdInputSnPCummulativeDefaultRates = filteredPdInputSnPCummulativeDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var pdInputSnPCummulativeDefaultRates = from o in pagedAndFilteredPdInputSnPCummulativeDefaultRates
                                                    select new GetPdInputSnPCummulativeDefaultRateForViewDto()
                                                    {
                                                        PdInputSnPCummulativeDefaultRate = new PdInputSnPCummulativeDefaultRateDto
                                                        {
                                                            Rating = o.Rating,
                                                            Years = o.Years,
                                                            Value = o.Value,
                                                            Id = o.Id
                                                        }
                                                    };

            var totalCount = await filteredPdInputSnPCummulativeDefaultRates.CountAsync();

            return new PagedResultDto<GetPdInputSnPCummulativeDefaultRateForViewDto>(
                totalCount,
                await pdInputSnPCummulativeDefaultRates.ToListAsync()
            );
        }

        public async Task<GetPdInputSnPCummulativeDefaultRateForEditOutput> GetPdInputSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input)
        {
            var pdInputSnPCummulativeDefaultRate = await _pdInputSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPdInputSnPCummulativeDefaultRateForEditOutput { PdInputSnPCummulativeDefaultRate = ObjectMapper.Map<CreateOrEditPdInputSnPCummulativeDefaultRateDto>(pdInputSnPCummulativeDefaultRate) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPdInputSnPCummulativeDefaultRateDto input)
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

        protected virtual async Task Create(CreateOrEditPdInputSnPCummulativeDefaultRateDto input)
        {
            var pdInputSnPCummulativeDefaultRate = ObjectMapper.Map<PdInputAssumptionSnPCummulativeDefaultRate>(input);

            await SumbitForApproval(input, pdInputSnPCummulativeDefaultRate);

            await _pdInputSnPCummulativeDefaultRateRepository.InsertAsync(pdInputSnPCummulativeDefaultRate);
        }

        protected virtual async Task Update(CreateOrEditPdInputSnPCummulativeDefaultRateDto input)
        {
            var pdInputSnPCummulativeDefaultRate = await _pdInputSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);



            ObjectMapper.Map(input, pdInputSnPCummulativeDefaultRate);
        }

        private async Task SumbitForApproval(CreateOrEditPdInputSnPCummulativeDefaultRateDto input, PdInputAssumptionSnPCummulativeDefaultRate assumption)
        {
            await _assumptionApprovalsAppService.CreateOrEdit(new CreateOrEditAssumptionApprovalDto()
            {
                OrganizationUnitId = assumption.OrganizationUnitId,
                Framework = assumption.Framework,
                AssumptionType = AssumptionTypeEnum.LgdInputAssumption,
                AssumptionGroup = L("PdInputSnPCummulativeDefaultRates"),
                InputName = "Rating: " + assumption.Rating + " Year: " + assumption.Years,
                OldValue = assumption.Value.ToString(),
                NewValue = input.Value.ToString(),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdSnpAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _pdInputSnPCummulativeDefaultRateRepository.DeleteAsync(input.Id);
        }
    }
}