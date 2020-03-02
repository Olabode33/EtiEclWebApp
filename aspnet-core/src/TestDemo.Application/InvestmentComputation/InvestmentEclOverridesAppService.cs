using TestDemo.InvestmentComputation;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.InvestmentInputs;
using Abp.UI;

namespace TestDemo.InvestmentComputation
{
    [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides)]
    public class InvestmentEclOverridesAppService : TestDemoAppServiceBase, IInvestmentEclOverridesAppService
    {
        private readonly IRepository<InvestmentEclOverride, Guid> _investmentEclOverrideRepository;
        private readonly IRepository<InvestmentEclSicr, Guid> _lookup_investmentEclSicrRepository;
        private readonly IRepository<InvestmentAssetBook, Guid> _lookup_investmentAssetbookRepository;
        private readonly IRepository<InvestmentEclFinalResult, Guid> _lookup_investmentEclFinalResultRepository;

        private readonly IInvestmentEclOverrideApprovalsAppService _invsecEclOverrideApprovalsAppService;


        public InvestmentEclOverridesAppService(
            IRepository<InvestmentEclOverride, Guid> investmentEclOverrideRepository,
            IRepository<InvestmentEclSicr, Guid> lookup_investmentEclSicrRepository,
            IRepository<InvestmentAssetBook, Guid> lookup_investmentAssetbookRepository,
            IRepository<InvestmentEclFinalResult, Guid> lookup_investmentEclFinalResultRepository,
            IInvestmentEclOverrideApprovalsAppService invsecEclOverrideApprovalsAppService
            )
        {
            _investmentEclOverrideRepository = investmentEclOverrideRepository;
            _lookup_investmentEclSicrRepository = lookup_investmentEclSicrRepository;
            _lookup_investmentAssetbookRepository = lookup_investmentAssetbookRepository;
            _lookup_investmentEclFinalResultRepository = lookup_investmentEclFinalResultRepository;
            _invsecEclOverrideApprovalsAppService = invsecEclOverrideApprovalsAppService;
        }

        public async Task<PagedResultDto<GetInvestmentEclOverrideForViewDto>> GetAll(GetAllInvestmentEclOverridesInput input)
        {
            //TODO: Add Permission for Override Approvals
            //TODO: Set default filter status based on user's permission / privileges (Approver's default to submitted & initiators default to all)
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredInvestmentEclOverrides = _investmentEclOverrideRepository.GetAll()
                        .Include(e => e.InvestmentEclSicrFk)
                        .Where(x => x.EclId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.OverrideComment.ToLower().Contains(input.Filter.ToLower()) || (e.InvestmentEclSicrFk != null && e.InvestmentEclSicrFk.AssetDescription.ToLower().Contains(input.Filter.ToLower())))
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);
                        //.WhereIf(!string.IsNullOrWhiteSpace(input.InvestmentEclSicrAssetDescriptionFilter), e => e.InvestmentEclSicrFk != null && e.InvestmentEclSicrFk.AssetDescription == input.InvestmentEclSicrAssetDescriptionFilter);

            var pagedAndFilteredInvestmentEclOverrides = filteredInvestmentEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentEclOverrides = from o in pagedAndFilteredInvestmentEclOverrides
                                         join o1 in _lookup_investmentEclSicrRepository.GetAll() on o.InvestmentEclSicrId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         select new GetInvestmentEclOverrideForViewDto()
                                         {
                                             InvestmentEclOverride = new InvestmentEclOverrideDto
                                             {
                                                 StageOverride = o.StageOverride,
                                                 ImpairmentOverride = o.ImpairmentOverride,
                                                 OverrideComment = o.OverrideComment,
                                                 Status = o.Status,
                                                 Id = o.Id,
                                                 EclId = o.EclId,
                                                 InvestmentEclSicrId = o.InvestmentEclSicrId
                                             },
                                             InvestmentEclSicrAssetDescription = s1 == null ? "" : s1.AssetDescription.ToString()
                                         };

            var totalCount = await filteredInvestmentEclOverrides.CountAsync();

            return new PagedResultDto<GetInvestmentEclOverrideForViewDto>(
                totalCount,
                await investmentEclOverrides.ToListAsync()
            );
        }

        public async Task<List<NameValueDto>> SearchResult(EclShared.Dtos.GetRecordForOverrideInputDto input)
        {
            var filteredInvestmentEclSicr = _lookup_investmentEclSicrRepository.GetAll().Where(x => x.EclId == input.EclId);

            //var pagedAndFilteredInvestmentEclOverrides = filteredInvestmentEclOverrides
            //    .OrderBy(input.Sorting ?? "id asc")
            //    .PageBy(input);

            return await _lookup_investmentEclSicrRepository.GetAll()
                                                            .Where(x => x.EclId == input.EclId && (x.AssetDescription.ToLower().Contains(input.searchTerm.ToLower())))
                                                            .Select(x => new NameValueDto
                                                            {
                                                                Value = x.Id.ToString(),
                                                                Name = x.AssetDescription
                                                            }).ToListAsync();
        }

        public async Task<GetInvestmentPreResultForOverrideOutput> GetEclRecordDetails(EntityDto<Guid> input)
        {
            var selectedRecord = await _lookup_investmentEclSicrRepository.FirstOrDefaultAsync(input.Id);
            var overrideRecord = await _investmentEclOverrideRepository.FirstOrDefaultAsync(x => x.InvestmentEclSicrId == input.Id && x.EclId == selectedRecord.EclId);
            var preResult = await _lookup_investmentEclFinalResultRepository.FirstOrDefaultAsync(x => x.RecordId == selectedRecord.RecordId);
            var dto = new CreateOrEditInvestmentEclOverrideDto() { RecordId = selectedRecord.RecordId, InvestmentEclSicrId = selectedRecord.Id, EclId = selectedRecord.EclId };

            if (overrideRecord != null)
            {
                dto.EclId = overrideRecord.EclId;
                dto.Id = overrideRecord.Id;
                dto.OverrideComment = overrideRecord.OverrideComment;
                dto.Stage = overrideRecord.StageOverride;
                dto.Status = overrideRecord.Status;
            }

            return new GetInvestmentPreResultForOverrideOutput
            {
                AssetDescription = selectedRecord.AssetDescription,
                AssetType = selectedRecord.AssetType,
                CurrentRating = selectedRecord.CurrentCreditRating,
                Stage = selectedRecord.FinalStage,
                Impairment = preResult.Impairment,
                EclOverrides = dto
            };
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Edit)]
        public async Task<GetInvestmentEclOverrideForEditOutput> GetInvestmentEclOverrideForEdit(EntityDto<Guid> input)
        {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvestmentEclOverrideForEditOutput { InvestmentEclOverride = ObjectMapper.Map<CreateOrEditInvestmentEclOverrideDto>(investmentEclOverride) };

            if (output.InvestmentEclOverride.InvestmentEclSicrId != null)
            {
                var _lookupInvestmentEclSicr = await _lookup_investmentEclSicrRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclOverride.InvestmentEclSicrId);
                output.InvestmentEclSicrAssetDescription = _lookupInvestmentEclSicr.AssetDescription.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvestmentEclOverrideDto input)
        {
            var validation = await ValidateForOverride(input);
            if (validation.Status)
            {
                if (input.Id == null)
                {
                    await Create(input);
                }
                else
                {
                    await Update(input);
                }
            } else
            {
                throw new UserFriendlyException(validation.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Create)]
        protected virtual async Task Create(CreateOrEditInvestmentEclOverrideDto input)
        {
            await _investmentEclOverrideRepository.InsertAsync(new InvestmentEclOverride
            {
                Id = new Guid(),
                EclId = input.EclId,
                InvestmentEclSicrId = input.InvestmentEclSicrId,
                OverrideComment = input.OverrideComment,
                StageOverride = input.Stage,
                ImpairmentOverride = input.ImpairmentOverride,
                Status = GeneralStatusEnum.Submitted
            });
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Edit)]
        protected virtual async Task Update(CreateOrEditInvestmentEclOverrideDto input)
        {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
            //ObjectMapper.Map(input, investmentEclOverride);
            investmentEclOverride.OverrideComment = input.OverrideComment;
            investmentEclOverride.StageOverride = (int)input.Stage;
            investmentEclOverride.ImpairmentOverride = input.ImpairmentOverride;
            investmentEclOverride.Status = input.Status;

            await _investmentEclOverrideRepository.UpdateAsync(investmentEclOverride);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclOverrideRepository.DeleteAsync(input.Id);
        }

        public virtual async Task ApproveReject(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {
            var ecl = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.OverrideRecordId);
            ecl.Status = input.Status;
            ObjectMapper.Map(ecl, ecl);

            var approvalDto = new CreateOrEditInvestmentEclOverrideApprovalDto()
            {
                InvestmentEclOverrideId = input.OverrideRecordId,
                ReviewComment = input.ReviewComment,
                Status = input.Status,
                ReviewDate = DateTime.Now,
                ReviewedByUserId = AbpSession.UserId
            };
            await _invsecEclOverrideApprovalsAppService.CreateOrEdit(approvalDto);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides)]
        public async Task<PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>> GetAllInvestmentEclSicrForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_investmentEclSicrRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.AssetDescription.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var investmentEclSicrList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>();
            foreach (var investmentEclSicr in investmentEclSicrList)
            {
                lookupTableDtoList.Add(new InvestmentEclOverrideInvestmentEclSicrLookupTableDto
                {
                    Id = investmentEclSicr.Id.ToString(),
                    DisplayName = investmentEclSicr.AssetDescription?.ToString()
                });
            }

            return new PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }


        protected async Task<EclShared.Dtos.ValidationMessageDto> ValidateForOverride(CreateOrEditInvestmentEclOverrideDto input)
        {
            var output = new EclShared.Dtos.ValidationMessageDto();

            var reviewedOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(x => x.InvestmentEclSicrId == input.InvestmentEclSicrId && x.Status != GeneralStatusEnum.Submitted);

            if (reviewedOverride != null)
            {
                output.Status = false;
                output.Message = L("ApplyOverrideErrorRecordReviewed");
            } else
            {
                output.Status = true;
                output.Message = "";
            }

            return output;
        }
    }
}