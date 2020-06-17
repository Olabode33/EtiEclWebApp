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
using TestDemo.EclConfig;
using Abp.Configuration;
using TestDemo.Dto.Overrides;
using TestDemo.Investment;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;
using TestDemo.Authorization.Users;
using Abp.Organizations;

namespace TestDemo.InvestmentComputation
{
    [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides)]
    public class InvestmentEclOverridesAppService : TestDemoAppServiceBase, IInvestmentEclOverridesAppService
    {
        private readonly IRepository<InvestmentEclOverride, Guid> _investmentEclOverrideRepository;
        private readonly IRepository<InvestmentEclSicr, Guid> _lookup_investmentEclSicrRepository;
        private readonly IRepository<InvestmentAssetBook, Guid> _lookup_investmentAssetbookRepository;
        private readonly IRepository<InvestmentEclFinalResult, Guid> _lookup_investmentEclFinalResultRepository;
        private readonly IRepository<InvestmentEclOverrideApproval, Guid> _lookup_investmentEclOverrideApprovalRepository;
        private readonly IRepository<InvestmentEcl, Guid> _lookup_eclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;

        public InvestmentEclOverridesAppService(
            IRepository<InvestmentEclOverride, Guid> investmentEclOverrideRepository,
            IRepository<InvestmentEclSicr, Guid> lookup_investmentEclSicrRepository,
            IRepository<InvestmentAssetBook, Guid> lookup_investmentAssetbookRepository,
            IRepository<InvestmentEclFinalResult, Guid> lookup_investmentEclFinalResultRepository,
            IRepository<InvestmentEclOverrideApproval, Guid> lookup_investmentEclOverrideRepository,
            IRepository<InvestmentEcl, Guid> lookup_eclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env
            )
        {
            _investmentEclOverrideRepository = investmentEclOverrideRepository;
            _lookup_investmentEclSicrRepository = lookup_investmentEclSicrRepository;
            _lookup_investmentAssetbookRepository = lookup_investmentAssetbookRepository;
            _lookup_investmentEclFinalResultRepository = lookup_investmentEclFinalResultRepository;
            _lookup_investmentEclOverrideApprovalRepository = lookup_investmentEclOverrideRepository;
            _lookup_eclRepository = lookup_eclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
        }

        public async Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input)
        {
            //TODO: Add Permission for Override Approvals
            //TODO: Set default filter status based on user's permission / privileges (Approver's default to submitted & initiators default to all)
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredInvestmentEclOverrides = _investmentEclOverrideRepository.GetAll()
                        .Include(e => e.InvestmentEclSicrFk)
                        .Where(x => x.EclId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.OverrideComment.ToLower().Contains(input.Filter.ToLower()) || (e.InvestmentEclSicrFk != null && e.InvestmentEclSicrFk.AssetDescription.ToLower().Contains(input.Filter.ToLower())))
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter);
                        
            var pagedAndFilteredInvestmentEclOverrides = filteredInvestmentEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentEclOverrides = from o in pagedAndFilteredInvestmentEclOverrides
                                         join o1 in _lookup_investmentEclSicrRepository.GetAll() on o.InvestmentEclSicrId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         select new GetEclOverrideForViewDto()
                                         {
                                             EclOverride = new EclOverrideDto
                                             {
                                                 StageOverride = o.StageOverride,
                                                 ImpairmentOverride = o.ImpairmentOverride,
                                                 OverrideComment = o.OverrideComment,
                                                 Status = o.Status,
                                                 Id = o.Id,
                                                 EclId = o.EclId,
                                                 RecordId = o.InvestmentEclSicrId
                                             },
                                             ContractId = s1 == null ? "" : s1.AssetDescription.ToString(),
                                             AccountNumber = s1 == null ? "" : s1.AssetDescription,
                                             CustomerName = s1 == null ? "" : s1.AssetDescription
                                         };

            var totalCount = await filteredInvestmentEclOverrides.CountAsync();

            return new PagedResultDto<GetEclOverrideForViewDto>(
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
            var dto = new CreateOrEditEclOverrideDto() { RecordId = selectedRecord.RecordId, EclSicrId = selectedRecord.Id, EclId = selectedRecord.EclId };

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

            var output = new GetInvestmentEclOverrideForEditOutput { InvestmentEclOverride = ObjectMapper.Map<CreateOrEditEclOverrideDto>(investmentEclOverride) };

            if (output.InvestmentEclOverride.EclSicrId != null)
            {
                var _lookupInvestmentEclSicr = await _lookup_investmentEclSicrRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclOverride.EclSicrId);
                output.InvestmentEclSicrAssetDescription = _lookupInvestmentEclSicr.AssetDescription.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEclOverrideDto input)
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
        protected virtual async Task Create(CreateOrEditEclOverrideDto input)
        {
            await _investmentEclOverrideRepository.InsertAsync(new InvestmentEclOverride
            {
                Id = new Guid(),
                EclId = input.EclId,
                InvestmentEclSicrId = input.EclSicrId,
                OverrideComment = input.OverrideComment,
                StageOverride = input.Stage,
                ImpairmentOverride = input.ImpairmentOverride,
                Status = GeneralStatusEnum.Submitted
            });
            await SendSubmittedEmail(input.EclId);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Edit)]
        protected virtual async Task Update(CreateOrEditEclOverrideDto input)
        {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
            //ObjectMapper.Map(input, investmentEclOverride);
            investmentEclOverride.OverrideComment = input.OverrideComment;
            investmentEclOverride.StageOverride = (int)input.Stage;
            investmentEclOverride.ImpairmentOverride = input.ImpairmentOverride;
            investmentEclOverride.Status = input.Status;

            await _investmentEclOverrideRepository.UpdateAsync(investmentEclOverride);
            if (input.Status == GeneralStatusEnum.Submitted)
            {
                await SendSubmittedEmail(investmentEclOverride.EclId);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEclOverrides_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclOverrideRepository.DeleteAsync(input.Id);
        }

        public virtual async Task ApproveReject(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {
            var ecl = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.OverrideRecordId);

            await _lookup_investmentEclOverrideApprovalRepository.InsertAsync(new InvestmentEclOverrideApproval
            {
                InvestmentEclOverrideId = input.OverrideRecordId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _lookup_investmentEclOverrideApprovalRepository.GetAllListAsync(x => x.InvestmentEclOverrideId == input.OverrideRecordId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = GeneralStatusEnum.Approved;
                    await SendApprovedEmail(ecl.EclId);
                }
                else
                {
                    ecl.Status = GeneralStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail(ecl.EclId);
                }
            }
            else
            {
                ecl.Status = GeneralStatusEnum.Draft;
            }

            ObjectMapper.Map(ecl, ecl);
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


        protected async Task<EclShared.Dtos.ValidationMessageDto> ValidateForOverride(CreateOrEditEclOverrideDto input)
        {
            var output = new EclShared.Dtos.ValidationMessageDto();

            var reviewedOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(x => x.InvestmentEclSicrId == input.EclSicrId && x.Status != GeneralStatusEnum.Submitted);

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

        public async Task SendSubmittedEmail(Guid eclId)
        {
            var users = await UserManager.GetUsersInRoleAsync("Affiliate Reviewer");
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    int frameworkId = (int)FrameworkEnum.Investments;
                    var baseUrl = _appConfiguration["App:ClientRootAddress"];
                    var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                    var type = "Investment ECL Override";
                    var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId);
                    var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                    await _emailer.SendEmailSubmittedForApprovalAsync(user, type, ou.DisplayName, link);
                }
            }
        }

        public async Task SendAdditionalApprovalEmail(Guid eclId)
        {
            var users = await UserManager.GetUsersInRoleAsync("Affiliate Reviewer");
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    int frameworkId = (int)FrameworkEnum.Investments;
                    var baseUrl = _appConfiguration["App:ClientRootAddress"];
                    var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                    var type = "Investment ECL Override";
                    var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId);
                    var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                    await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Investments;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Investment ECL Override";
            var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}