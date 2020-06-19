using TestDemo.ObeInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Dto.Overrides;
using Abp.UI;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.EclShared;
using Abp.Configuration;
using TestDemo.EclConfig;
using TestDemo.OBE;
using TestDemo.Authorization.Users;
using Abp.Organizations;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;

namespace TestDemo.ObeComputation
{
    public class ObeEclOverridesAppService : TestDemoAppServiceBase, IObeEclOverridesAppService
    {
        private readonly IRepository<ObeEclOverride, Guid> _obeEclOverrideRepository;
        private readonly IRepository<ObeEclDataLoanBook, Guid> _lookup_obeEclDataLoanBookRepository;
        private readonly IRepository<ObeEclOverrideApproval, Guid> _lookup_obeEclOverrideApprovalRepository;
        private readonly IRepository<ObeEclFrameworkFinal, Guid> _lookup_obeEclFrameworkRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;


        public ObeEclOverridesAppService(
            IRepository<ObeEclOverride, Guid> obeEclOverrideRepository,
            IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository,
            IRepository<ObeEclFrameworkFinal, Guid> lookup_obeEclFrameworkRepository,
            IRepository<ObeEclOverrideApproval, Guid> lookup_obeEclOverrideApprovalRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env)
        {
            _obeEclOverrideRepository = obeEclOverrideRepository;
            _lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
            _lookup_obeEclOverrideApprovalRepository = lookup_obeEclOverrideApprovalRepository;
            _lookup_obeEclFrameworkRepository = lookup_obeEclFrameworkRepository;
            _obeEclRepository = obeEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();

        }

        public async Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input)
        {

            var filteredRetailEclOverrides = _obeEclOverrideRepository.GetAll()
                        .Where(x => x.ObeEclId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ContractId.Contains(input.Filter));

            var filteredFrameworkResult = _lookup_obeEclFrameworkRepository.GetAll()
                        .Where(x => x.ObeEclId == input.EclId);

            var pagedAndFilteredRetailEclOverrides = filteredRetailEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclOverrides = from o in pagedAndFilteredRetailEclOverrides
                                     join o1 in filteredFrameworkResult on o.ContractId equals o1.ContractId into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     select new GetEclOverrideForViewDto()
                                     {
                                         EclOverride = new EclOverrideDto
                                         {
                                             StageOverride = o.Stage,
                                             ImpairmentOverride = o.TtrYears,
                                             OverrideComment = o.Reason,
                                             Status = o.Status,
                                             Id = o.Id,
                                             EclId = (Guid)o.ObeEclId,
                                             RecordId = s1 == null ? o.Id : s1.Id
                                         },
                                         ContractId = o.ContractId,
                                         //AccountNumber = s1 == null ? "" : s1.AssetDescription,
                                         //CustomerName = s1 == null ? "" : s1.AssetDescription
                                     };

            var totalCount = await filteredRetailEclOverrides.CountAsync();

            return new PagedResultDto<GetEclOverrideForViewDto>(
                totalCount,
                await retailEclOverrides.ToListAsync()
            );
        }

        public async Task<List<NameValueDto>> SearchResult(EclShared.Dtos.GetRecordForOverrideInputDto input)
        {
            var filteredRecords = _lookup_obeEclFrameworkRepository.GetAll().Where(x => x.ObeEclId == input.EclId);

            var query = from o in filteredRecords
                        join o1 in _lookup_obeEclDataLoanBookRepository.GetAll() on o.ContractId equals o1.ContractId into j1
                        from s1 in j1.DefaultIfEmpty()

                        select new GetObeEclOverrideForViewDto()
                        {
                            ObeEclOverride = new ObeEclOverrideDto
                            {
                                ContractId = o.ContractId,
                                Id = o.Id
                            },
                            ObeEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
                        };

            return await query.Where(x => x.ObeEclDataLoanBookCustomerName.ToLower().Contains(input.searchTerm.ToLower()))
                                            .Select(x => new NameValueDto
                                            {
                                                Value = x.ObeEclOverride.Id.ToString(),
                                                Name = x.ObeEclDataLoanBookCustomerName
                                            }).ToListAsync();
        }

        public async Task<GetPreResultForOverrideOutput> GetEclRecordDetails(EntityDto<Guid> input)
        {
            var selectedRecord = await _lookup_obeEclFrameworkRepository.FirstOrDefaultAsync(input.Id);
            var overrideRecord = await _obeEclOverrideRepository.FirstOrDefaultAsync(x => x.ObeEclId == selectedRecord.ObeEclId && x.ContractId == selectedRecord.ContractId);
            var contract = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync(x => x.ObeEclUploadId == selectedRecord.ObeEclId && x.ContractId == selectedRecord.ContractId);

            var dto = new InvestmentComputation.Dtos.CreateOrEditEclOverrideDto() { ContractId = selectedRecord.ContractId, EclSicrId = selectedRecord.Id, EclId = selectedRecord.ObeEclId };

            if (overrideRecord != null)
            {
                dto.EclId = (Guid)overrideRecord.ObeEclId;
                dto.Id = overrideRecord.Id;
                dto.OverrideComment = overrideRecord.Reason;
                dto.Stage = overrideRecord.Stage;
                dto.Status = overrideRecord.Status;
            }

            return new GetPreResultForOverrideOutput
            {
                ContractId = selectedRecord.ContractId,
                AccountNumber = contract.AccountNo,
                AccountName = contract.CustomerName,
                Stage = selectedRecord.Stage,
                Impairment = selectedRecord.FinalEclValue,
                EclOverrides = dto
            };
        }

        public async Task<GetObeEclOverrideForEditOutput> GetObeEclOverrideForEdit(EntityDto<Guid> input)
        {
            var obeEclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetObeEclOverrideForEditOutput { ObeEclOverride = ObjectMapper.Map<CreateOrEditObeEclOverrideDto>(obeEclOverride) };

            if (output.ObeEclOverride.ObeEclDataLoanBookId != null)
            {
                var _lookupObeEclDataLoanBook = await _lookup_obeEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.ObeEclOverride.ObeEclDataLoanBookId);
                output.ObeEclDataLoanBookCustomerName = _lookupObeEclDataLoanBook.CustomerName.ToString();
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
            }
            else
            {
                throw new UserFriendlyException(validation.Message);
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
        protected virtual async Task Create(CreateOrEditEclOverrideDto input)
        {
            await _obeEclOverrideRepository.InsertAsync(new ObeEclOverride
            {
                Id = new Guid(),
                ObeEclId = input.EclId,
                ContractId = input.ContractId,
                Reason = input.OverrideComment,
                Stage = input.Stage,
                TtrYears = input.ImpairmentOverride,
                Status = GeneralStatusEnum.Submitted
            });
            await SendSubmittedEmail((Guid)input.EclId);
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
        protected virtual async Task Update(CreateOrEditEclOverrideDto input)
        {
            var eclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);

            eclOverride.ObeEclId = input.EclId;
            eclOverride.ContractId = input.ContractId;
            eclOverride.Reason = input.OverrideComment;
            eclOverride.Stage = input.Stage;
            eclOverride.TtrYears = input.ImpairmentOverride;
            eclOverride.Status = GeneralStatusEnum.Submitted;

            await _obeEclOverrideRepository.UpdateAsync(eclOverride);
            await SendSubmittedEmail((Guid)eclOverride.ObeEclId);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _obeEclOverrideRepository.DeleteAsync(input.Id);
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Override_Review)]
        public virtual async Task ApproveReject(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {
            var ecl = await _obeEclOverrideRepository.FirstOrDefaultAsync((Guid)input.OverrideRecordId);

            await _lookup_obeEclOverrideApprovalRepository.InsertAsync(new ObeEclOverrideApproval
            {
                EclOverrideId = input.OverrideRecordId,
                ObeEclId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _lookup_obeEclOverrideApprovalRepository.GetAllListAsync(x => x.ObeEclId == input.OverrideRecordId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = GeneralStatusEnum.Approved;
                    await SendApprovedEmail((Guid)ecl.ObeEclId);
                }
                else
                {
                    ecl.Status = GeneralStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail((Guid)ecl.ObeEclId);
                }
            }
            else
            {
                ecl.Status = GeneralStatusEnum.Draft;
            }

            ObjectMapper.Map(ecl, ecl);
        }

        protected async Task<EclShared.Dtos.ValidationMessageDto> ValidateForOverride(CreateOrEditEclOverrideDto input)
        {
            var output = new EclShared.Dtos.ValidationMessageDto();


            //Validate cutoff date
            var cutOffDate = await SettingManager.GetSettingValueAsync<DateTime>(EclSettings.OverrideCutOffDate);
            if (cutOffDate.Date <= DateTime.Now.Date)
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

            var reviewedOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync(x => x.ContractId == input.ContractId && x.Status != GeneralStatusEnum.Submitted);

            if (reviewedOverride != null)
            {
                output.Status = false;
                output.Message = L("ApplyOverrideErrorRecordReviewed");
            }
            else
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
                var ecl = _obeEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.OBE;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "OBE ECL Override";
                        await _emailer.SendEmailSubmittedForApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendAdditionalApprovalEmail(Guid eclId)
        {
            var users = await UserManager.GetUsersInRoleAsync("Affiliate Reviewer");
            if (users.Count > 0)
            {
                var ecl = _obeEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.OBE;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "OBE ECL Override";
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.OBE;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "OBE ECL Override";
            var ecl = _obeEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}