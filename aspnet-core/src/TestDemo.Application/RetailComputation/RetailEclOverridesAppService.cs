using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
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
using TestDemo.EclConfig;
using Abp.Configuration;
using TestDemo.Retail;
using TestDemo.Authorization.Users;
using Abp.Organizations;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;

namespace TestDemo.RetailComputation
{
    public class RetailEclOverridesAppService : TestDemoAppServiceBase, IRetailEclOverridesAppService
    {
        private readonly IRepository<RetailEclOverride, Guid> _retailEclOverrideRepository;
        private readonly IRepository<RetailEclDataLoanBook, Guid> _lookup_retailEclDataLoanBookRepository;
        private readonly IRepository<RetailEclOverrideApproval, Guid> _lookup_retailEclOverrideApprovalRepository;
        private readonly IRepository<RetailEclFrameworkFinal, Guid> _lookup_retailEclFrameworkRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;


        public RetailEclOverridesAppService(
            IRepository<RetailEclOverride, Guid> retailEclOverrideRepository,
            IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository,
            IRepository<RetailEclFrameworkFinal, Guid> lookup_retailEclFrameworkRepository,
            IRepository<RetailEclOverrideApproval, Guid> lookup_retailEclOverrideApprovalRepository, 
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env)
        {
            _retailEclOverrideRepository = retailEclOverrideRepository;
            _lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
            _lookup_retailEclOverrideApprovalRepository = lookup_retailEclOverrideApprovalRepository;
            _lookup_retailEclFrameworkRepository = lookup_retailEclFrameworkRepository;
            _retailEclRepository = retailEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
        }

        public async Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input)
        {

            var filteredRetailEclOverrides = _retailEclOverrideRepository.GetAll()
                        .Where(x => x.RetailEclId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ContractId.Contains(input.Filter));

            var filteredFrameworkResult = _lookup_retailEclFrameworkRepository.GetAll()
                        .Where(x => x.RetailEclId == input.EclId);

            var pagedAndFilteredRetailEclOverrides = filteredRetailEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclOverrides = from o in pagedAndFilteredRetailEclOverrides
                                     join o1 in filteredFrameworkResult on o.ContractId equals o1.ContractId  into j1
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
                                             EclId = (Guid)o.RetailEclId,
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
            var filteredRecords = _lookup_retailEclFrameworkRepository.GetAll().Where(x => x.RetailEclId == input.EclId);

            var query = from o in filteredRecords
                        join o1 in _lookup_retailEclDataLoanBookRepository.GetAll() on o.ContractId equals o1.ContractId into j1
                        from s1 in j1.DefaultIfEmpty()

                        select new GetRetailEclOverrideForViewDto()
                        {
                            RetailEclOverride = new RetailEclOverrideDto
                            {
                                ContractId = o.ContractId,
                                Id = o.Id
                            },
                            RetailEclDataLoanBookCustomerName = s1 == null ? "" : s1.CustomerName.ToString()
                        };

            return await query.Where(x => x.RetailEclDataLoanBookCustomerName.ToLower().Contains(input.searchTerm.ToLower()))
                                            .Select(x => new NameValueDto
                                            {
                                                Value = x.RetailEclOverride.Id.ToString(),
                                                Name = x.RetailEclDataLoanBookCustomerName
                                            }).ToListAsync();
        }

        public async Task<GetPreResultForOverrideOutput> GetEclRecordDetails(EntityDto<Guid> input)
        {
            var selectedRecord = await _lookup_retailEclFrameworkRepository.FirstOrDefaultAsync(input.Id);
            var overrideRecord = await _retailEclOverrideRepository.FirstOrDefaultAsync(x => x.RetailEclId == selectedRecord.RetailEclId && x.ContractId == selectedRecord.ContractId);
            var contract = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync(x => x.RetailEclUploadId == selectedRecord.RetailEclId && x.ContractId == selectedRecord.ContractId);

            var dto = new InvestmentComputation.Dtos.CreateOrEditEclOverrideDto() { ContractId = selectedRecord.ContractId, EclSicrId = selectedRecord.Id, EclId = selectedRecord.RetailEclId };

            if (overrideRecord != null)
            {
                dto.EclId = (Guid)overrideRecord.RetailEclId;
                dto.Id = overrideRecord.Id;
                dto.OverrideComment = overrideRecord.OverrideComment;
                dto.Stage = overrideRecord.StageOverride;
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


        public async Task<GetRetailEclOverrideForEditOutput> GetRetailEclOverrideForEdit(EntityDto<Guid> input)
        {
            var retailEclOverride = await _retailEclOverrideRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclOverrideForEditOutput { RetailEclOverride = ObjectMapper.Map<CreateOrEditRetailEclOverrideDto>(retailEclOverride) };

            if (output.RetailEclOverride.RetailEclDataLoanBookId != null)
            {
                var _lookupRetailEclDataLoanBook = await _lookup_retailEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.RetailEclOverride.RetailEclDataLoanBookId);
                output.RetailEclDataLoanBookCustomerName = _lookupRetailEclDataLoanBook.CustomerName.ToString();
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
            await _retailEclOverrideRepository.InsertAsync(new RetailEclOverride
            {
                Id = new Guid(),
                RetailEclId = input.EclId,
                ContractId = input.ContractId,
                OverrideComment = input.OverrideComment,
                StageOverride = input.Stage,
                ImpairmentOverride = input.ImpairmentOverride,
                Status = GeneralStatusEnum.Submitted
            });
            await SendSubmittedEmail((Guid)input.EclId);
        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
        protected virtual async Task Update(CreateOrEditEclOverrideDto input)
        {
            var eclOverride = await _retailEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);

            eclOverride.RetailEclId = input.EclId;
            eclOverride.ContractId = input.ContractId;
            eclOverride.OverrideComment = input.OverrideComment;
            eclOverride.StageOverride = input.Stage;
            eclOverride.ImpairmentOverride = input.ImpairmentOverride;
            eclOverride.Status = GeneralStatusEnum.Submitted;

            await _retailEclOverrideRepository.UpdateAsync(eclOverride);
            await SendSubmittedEmail((Guid)eclOverride.RetailEclId);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclOverrideRepository.DeleteAsync(input.Id);
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Override_Review)]
        public virtual async Task ApproveReject(EclShared.Dtos.ReviewEclOverrideInputDto input)
        {
            var ecl = await _retailEclOverrideRepository.FirstOrDefaultAsync((Guid)input.OverrideRecordId);

            await _lookup_retailEclOverrideApprovalRepository.InsertAsync(new RetailEclOverrideApproval
            {
                EclOverrideId = input.OverrideRecordId,
                RetailEclId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _lookup_retailEclOverrideApprovalRepository.GetAllListAsync(x => x.RetailEclId == input.OverrideRecordId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = GeneralStatusEnum.Approved;
                    await SendApprovedEmail((Guid)ecl.RetailEclId);
                }
                else
                {
                    ecl.Status = GeneralStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail((Guid)ecl.RetailEclId);
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

            var reviewedOverride = await _retailEclOverrideRepository.FirstOrDefaultAsync(x => x.ContractId == input.ContractId && x.Status != GeneralStatusEnum.Submitted);

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
                var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);

                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Retail;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Retail ECL Override";
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
                var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Retail;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Retail ECL Override";
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Retail;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Retail ECL Override";
            var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}