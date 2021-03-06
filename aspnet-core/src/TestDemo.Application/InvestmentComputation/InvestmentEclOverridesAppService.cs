﻿using TestDemo.InvestmentComputation;

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
using TestDemo.EclShared.Jobs;
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.InvestmentComputation.Dtos.GetAllForLookupTableInput;
using Abp.BackgroundJobs;

namespace TestDemo.InvestmentComputation
{
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
        private readonly IRepository<Affiliate, long> _affiliateRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public InvestmentEclOverridesAppService(
            IRepository<InvestmentEclOverride, Guid> investmentEclOverrideRepository,
            IRepository<InvestmentEclSicr, Guid> lookup_investmentEclSicrRepository,
            IRepository<InvestmentAssetBook, Guid> lookup_investmentAssetbookRepository,
            IRepository<InvestmentEclFinalResult, Guid> lookup_investmentEclFinalResultRepository,
            IRepository<InvestmentEclOverrideApproval, Guid> lookup_investmentEclOverrideRepository,
            IRepository<InvestmentEcl, Guid> lookup_eclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<Affiliate, long> affiliateRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IBackgroundJobManager backgroundJobManager
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
            _affiliateRepository = affiliateRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _backgroundJobManager = backgroundJobManager;
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
                                                 RecordId = o.InvestmentEclSicrId,
                                                 OverrideType = o.OverrideType
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

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredInvestmentEclApprovals = _lookup_investmentEclOverrideApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Where(e => e.InvestmentEclOverrideId == input.Id);

            var investmentEclApprovals = from o in filteredInvestmentEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = (Guid)o.InvestmentEclOverrideId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var eclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)eclOverride.CreatorUserId).FullName;
            string updatedBy = "";
            if (eclOverride.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)eclOverride.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await investmentEclApprovals.ToListAsync(),
                DateCreated = eclOverride.CreationTime,
                LastUpdated = eclOverride.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
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
                dto.OverrideType = overrideRecord.OverrideType;
                dto.Stage = overrideRecord.StageOverride;
                dto.Status = overrideRecord.Status;
                dto.ImpairmentOverride = overrideRecord.ImpairmentOverride;
            }

            return new GetInvestmentPreResultForOverrideOutput
            {
                AssetDescription = selectedRecord.AssetDescription,
                AssetType = selectedRecord.AssetType,
                CurrentRating = selectedRecord.CurrentCreditRating,
                Stage = selectedRecord.FinalStage,
                Impairment = preResult.Impairment,
                Outstanding_Balance = preResult.Exposure,
                EclOverrides = dto
            };
        }

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

        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
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
                Status = GeneralStatusEnum.Submitted,
                OverrideType = input.OverrideType
            });
            await SendSubmittedEmail(input.EclId);
        }

        public async Task UploadBulkOveride(List<ImportExcelInvestmentEclOverrideDto> uploadData, Guid eclId)
        {
            foreach (var input in uploadData)
            {
                var investmentEclSicr = _lookup_investmentEclSicrRepository.FirstOrDefault(i => i.EclId == eclId && i.AssetDescription.ToLower() == input.AssetDescription.ToLower());

                if (investmentEclSicr != null)
                {
                    var exist = await _investmentEclOverrideRepository.FirstOrDefaultAsync(e => e.InvestmentEclSicrId == investmentEclSicr.Id);

                    if (exist != null)
                    {
                        exist.ImpairmentOverride = input.ImpairmentOverride;
                        exist.OverrideComment = input.OverrideComment;
                        exist.StageOverride = input.StageOverride;
                        exist.Status = GeneralStatusEnum.Submitted;
                        exist.OverrideType = input.OverrideType;
                        await _investmentEclOverrideRepository.UpdateAsync(exist);
                    }
                    else
                    {
                        await _investmentEclOverrideRepository.InsertAsync(new InvestmentEclOverride
                        {
                            Id = new Guid(),
                            EclId = eclId,
                            ImpairmentOverride = input.ImpairmentOverride,
                            OverrideComment = input.OverrideComment,
                            StageOverride = input.StageOverride,
                            Status = GeneralStatusEnum.Submitted,
                            OverrideType = input.OverrideType,
                            InvestmentEclSicrId = investmentEclSicr.Id
                        });
                    }
                }

            }

            await SendSubmittedEmail(eclId);
        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
        protected virtual async Task Update(CreateOrEditEclOverrideDto input)
        {
            var investmentEclOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);
            //ObjectMapper.Map(input, investmentEclOverride);
            investmentEclOverride.OverrideComment = input.OverrideComment;
            investmentEclOverride.StageOverride = (int)input.Stage;
            investmentEclOverride.ImpairmentOverride = input.ImpairmentOverride;
            investmentEclOverride.Status = GeneralStatusEnum.Submitted;
            investmentEclOverride.OverrideType = input.OverrideType;

            await _investmentEclOverrideRepository.UpdateAsync(investmentEclOverride);
            if (input.Status == GeneralStatusEnum.Submitted)
            {
                await SendSubmittedEmail(investmentEclOverride.EclId);
            }
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclOverrideRepository.DeleteAsync(input.Id);
        }

        public async Task ApproveRejectAll(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {

            var values = _investmentEclOverrideRepository.GetAll().Where(w => w.EclId == input.EclId && (w.Status == GeneralStatusEnum.AwaitngAdditionApproval || w.Status == GeneralStatusEnum.Submitted)).ToList();
            foreach (var item in values)
            {
                item.Status = input.Status;
                await _investmentEclOverrideRepository.FirstOrDefaultAsync(item.Id);
                input.OverrideRecordId = item.Id;
                await ApproveReject(input);
            }

        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Override_Review)]
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

            //Validate Override threshold
            var ecl = await _lookup_eclRepository.FirstOrDefaultAsync(input.EclId);
            if (ecl != null)
            {
                var affiliate = await _affiliateRepository.FirstOrDefaultAsync(ecl.OrganizationUnitId);
                if (affiliate.OverrideThreshold != null)
                {
                    var selectedRecord = await _lookup_investmentEclSicrRepository.FirstOrDefaultAsync(input.EclSicrId);
                    if (selectedRecord != null)
                    {
                        var asset = await _lookup_investmentAssetbookRepository.FirstOrDefaultAsync(selectedRecord.RecordId);
                        if (asset != null)
                        {
                            if (asset.CarryingAmountIFRS > affiliate.OverrideThreshold)
                            {
                                output.Status = false;
                                output.Message = L("ApplyOverrideErrorThresholdLimit");
                                return output;
                            }
                        }
                    }

                }
            }

            //Validate cutoff date
            var cutOffDate = await SettingManager.GetSettingValueAsync<DateTime>(EclSettings.OverrideCutOffDate);
            if (cutOffDate.Date >= DateTime.Now.Date)
            {
                output.Status = true;
                output.Message = "";
            }
            else
            {
                output.Status = false;
                output.Message = L("ApplyOverrideErrorCutoffDatePast");
                return output;
            }


            //var reviewedOverride = await _investmentEclOverrideRepository.FirstOrDefaultAsync(x => x.InvestmentEclSicrId == input.EclSicrId && x.Status != GeneralStatusEnum.Submitted);

            //if (reviewedOverride != null)
            //{
            //    output.Status = false;
            //    output.Message = L("ApplyOverrideErrorRecordReviewed");
            //} else
            //{
            //    output.Status = true;
            //    output.Message = "";
            //}

            return output;
        }

        public async Task SendSubmittedEmail(Guid eclId)
        {
            var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId); 
            int frameworkId = (int)FrameworkEnum.Investments;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Investment ECL Override";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = ecl.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.EclOverrideSubmittedEmail
            });

        }

        public async Task SendAdditionalApprovalEmail(Guid eclId)
        {
            var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId);
            int frameworkId = (int)FrameworkEnum.Investments;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Investment ECL Override";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = ecl.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.EclOverrideAwaitingApprovalEmail
            });
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            var ecl = _lookup_eclRepository.FirstOrDefault((Guid)eclId); 
            int frameworkId = (int)FrameworkEnum.Investments;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Investment ECL Override";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = ecl.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.EclOverrideApprovedEmail
            });
        }
    }
}