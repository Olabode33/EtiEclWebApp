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
using TestDemo.ObeResults;

namespace TestDemo.ObeComputation
{
    public class ObeEclOverridesAppService : TestDemoAppServiceBase, IObeEclOverridesAppService
    {
        private readonly IRepository<ObeEclOverride, Guid> _obeEclOverrideRepository;
        private readonly IRepository<ObeEclDataLoanBook, Guid> _lookup_obeEclDataLoanBookRepository;
        private readonly IRepository<ObeEclOverrideApproval, Guid> _lookup_obeEclOverrideApprovalRepository;
        private readonly IRepository<ObeEclFramworkReportDetail, Guid> _lookup_obeEclFrameworkRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<Affiliate, long> _affiliateRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;


        public ObeEclOverridesAppService(
            IRepository<ObeEclOverride, Guid> obeEclOverrideRepository,
            IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository,
            IRepository<ObeEclFramworkReportDetail, Guid> lookup_obeEclFrameworkRepository,
            IRepository<ObeEclOverrideApproval, Guid> lookup_obeEclOverrideApprovalRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<Affiliate, long> affiliateRepository,
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
            _affiliateRepository = affiliateRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();

        }

        public async Task<PagedResultDto<GetEclOverrideForViewDto>> GetAll(GetAllEclOverrideInput input)
        {

            var filteredRetailEclOverrides = _obeEclOverrideRepository.GetAll()
                        .Where(x => x.ObeEclDataLoanBookId == input.EclId)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ContractId.Contains(input.Filter));

            var filteredFrameworkResult = _lookup_obeEclFrameworkRepository.GetAll()
                        .Where(x => x.ObeEclId == input.EclId);

            var pagedAndFilteredRetailEclOverrides = filteredRetailEclOverrides
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclOverrides = from o in pagedAndFilteredRetailEclOverrides
                                     join o1 in filteredFrameworkResult on o.ContractId equals o1.ContractNo into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     select new GetEclOverrideForViewDto()
                                     {
                                         EclOverride = new EclOverrideDto
                                         {
                                             StageOverride = o.Stage,
                                             TtrYears = o.TtrYears,
                                             OverrideComment = o.Reason,
                                             Status = o.Status,
                                             Id = o.Id,
                                             EclId = (Guid)o.ObeEclDataLoanBookId,
                                             RecordId = s1 == null ? o.Id : s1.Id,
                                             FSV_Cash = o.FSV_Cash,
                                             FSV_CommercialProperty = o.FSV_CommercialProperty,
                                             FSV_Debenture = o.FSV_Debenture,
                                             FSV_Inventory = o.FSV_Inventory,
                                             FSV_PlantAndEquipment = o.FSV_PlantAndEquipment,
                                             FSV_Receivables = o.FSV_Receivables,
                                             FSV_ResidentialProperty = o.FSV_ResidentialProperty,
                                             FSV_Shares = o.FSV_Shares,
                                             FSV_Vehicle = o.FSV_Vehicle,
                                             OverlaysPercentage = o.OverlaysPercentage,
                                             OverrideType = o.OverrideType
                                         },
                                         ContractId = o.ContractId,
                                         ContractNo = s1 == null ? "" : s1.ContractNo,
                                         AccountNo = s1 == null ? "" : s1.AccountNo,
                                         CustomerNo = s1 == null ? "" : s1.CustomerNo,
                                         Segment = s1 == null ? "" : s1.Segment,
                                         ProductType = s1 == null ? "" : s1.ProductType,
                                         Sector = s1 == null ? "" : s1.Sector
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

            return await filteredRecords.Where(x => x.ContractNo.ToLower().Contains(input.searchTerm.ToLower()))
                                            .Select(x => new NameValueDto
                                            {
                                                Value = x.Id.ToString(),
                                                Name = x.ContractNo
                                            }).ToListAsync();
        }

        public async Task<GetPreResultForOverrideNewOutput> GetEclRecordDetails(EntityDto<Guid> input)
        {
            var selectedRecord = await _lookup_obeEclFrameworkRepository.FirstOrDefaultAsync(input.Id);
            var overrideRecord = await _obeEclOverrideRepository.FirstOrDefaultAsync(x => x.ObeEclDataLoanBookId == selectedRecord.ObeEclId && x.ContractId == selectedRecord.ContractNo);
            
            var dto = new CreateOrEditEclOverrideNewDto() {
                ContractId = selectedRecord.ContractNo,
                EclId = (Guid)selectedRecord.ObeEclId
            };

            if (overrideRecord != null)
            {
                dto.EclId = (Guid)overrideRecord.ObeEclDataLoanBookId;
                dto.ContractId = overrideRecord.ContractId;
                dto.Id = overrideRecord.Id;
                dto.OverrideComment = overrideRecord.Reason;
                dto.Stage = overrideRecord.Stage;
                dto.Status = overrideRecord.Status;
                dto.TtrYears = overrideRecord.TtrYears;
                dto.FSV_Cash = overrideRecord.FSV_Cash;
                dto.FSV_CommercialProperty = overrideRecord.FSV_CommercialProperty;
                dto.FSV_Debenture = overrideRecord.FSV_Debenture;
                dto.FSV_Inventory = overrideRecord.FSV_Inventory;
                dto.FSV_PlantAndEquipment = overrideRecord.FSV_PlantAndEquipment;
                dto.FSV_Receivables = overrideRecord.FSV_Receivables;
                dto.FSV_ResidentialProperty = overrideRecord.FSV_ResidentialProperty;
                dto.FSV_Shares = overrideRecord.FSV_Shares;
                dto.FSV_Vehicle = overrideRecord.FSV_Vehicle;
                dto.OverlaysPercentage = overrideRecord.OverlaysPercentage;
                dto.OverrideType = overrideRecord.OverrideType;
                
            }

            return new GetPreResultForOverrideNewOutput()
            {
                ContractNo = selectedRecord.ContractNo,
                AccountNo = selectedRecord.AccountNo,
                CustomerNo = selectedRecord.CustomerNo,
                ProductType = selectedRecord.ProductType,
                Sector = selectedRecord.Sector,
                Segment = selectedRecord.Segment,
                Outstanding_Balance = selectedRecord.Outstanding_Balance,
                Stage = selectedRecord.Stage,
                Impairment = selectedRecord.Impairment_ModelOutput,
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

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredOverrideApprovals = _lookup_obeEclOverrideApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Where(e => e.EclOverrideId == input.Id);

            var overridesApprovals = from o in filteredOverrideApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = (Guid)o.EclOverrideId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var eclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)eclOverride.CreatorUserId).FullName;
            string updatedBy = "";
            if (eclOverride.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)eclOverride.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await overridesApprovals.ToListAsync(),
                DateCreated = eclOverride.CreationTime,
                LastUpdated = eclOverride.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        public async Task CreateOrEdit(CreateOrEditEclOverrideNewDto input)
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
        protected virtual async Task Create(CreateOrEditEclOverrideNewDto input)
        {
            await _obeEclOverrideRepository.InsertAsync(new ObeEclOverride
            {
                Id = new Guid(),
                ObeEclDataLoanBookId = input.EclId,
                ContractId = input.ContractId,
                Reason = input.OverrideComment,
                Stage = input.Stage,
                TtrYears = input.TtrYears,
                FSV_Cash = input.FSV_Cash,
                FSV_CommercialProperty = input.FSV_CommercialProperty,
                FSV_Debenture = input.FSV_Debenture,
                FSV_Inventory = input.FSV_Inventory,
                FSV_PlantAndEquipment = input.FSV_PlantAndEquipment,
                FSV_Receivables = input.FSV_Receivables,
                FSV_ResidentialProperty = input.FSV_ResidentialProperty,
                FSV_Shares = input.FSV_Shares,
                FSV_Vehicle = input.FSV_Vehicle,
                OverlaysPercentage = input.OverlaysPercentage,
                Status = GeneralStatusEnum.Submitted,
                OverrideType = input.OverrideType
            });
            await SendSubmittedEmail((Guid)input.EclId);
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Override)]
        protected virtual async Task Update(CreateOrEditEclOverrideNewDto input)
        {
            var eclOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync((Guid)input.Id);

            eclOverride.ObeEclDataLoanBookId = input.EclId;
            eclOverride.ContractId = input.ContractId;
            eclOverride.Reason = input.OverrideComment;
            eclOverride.Stage = input.Stage;
            eclOverride.TtrYears = input.TtrYears;
            eclOverride.FSV_Cash = input.FSV_Cash;
            eclOverride.FSV_CommercialProperty = input.FSV_CommercialProperty;
            eclOverride.FSV_Debenture = input.FSV_Debenture;
            eclOverride.FSV_Inventory = input.FSV_Inventory;
            eclOverride.FSV_PlantAndEquipment = input.FSV_PlantAndEquipment;
            eclOverride.FSV_Receivables = input.FSV_Receivables;
            eclOverride.FSV_ResidentialProperty = input.FSV_ResidentialProperty;
            eclOverride.FSV_Shares = input.FSV_Shares;
            eclOverride.FSV_Vehicle = input.FSV_Vehicle;
            eclOverride.OverlaysPercentage = input.OverlaysPercentage;
            eclOverride.Status = GeneralStatusEnum.Submitted;
            eclOverride.OverrideType = input.OverrideType;

            await _obeEclOverrideRepository.UpdateAsync(eclOverride);
            await SendSubmittedEmail((Guid)eclOverride.ObeEclDataLoanBookId);
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
                var eclApprovals = await _lookup_obeEclOverrideApprovalRepository.GetAllListAsync(x => x.EclOverrideId == input.OverrideRecordId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = GeneralStatusEnum.Approved;
                    await SendApprovedEmail((Guid)ecl.ObeEclDataLoanBookId);
                }
                else
                {
                    ecl.Status = GeneralStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail((Guid)ecl.ObeEclDataLoanBookId);
                }
            }
            else
            {
                ecl.Status = GeneralStatusEnum.Draft;
            }

            ObjectMapper.Map(ecl, ecl);
        }

        protected async Task<EclShared.Dtos.ValidationMessageDto> ValidateForOverride(CreateOrEditEclOverrideNewDto input)
        {
            var output = new EclShared.Dtos.ValidationMessageDto();

            //Validate Override threshold
            var ecl = await _obeEclRepository.FirstOrDefaultAsync(input.EclId);
            if (ecl != null)
            {
                var affiliate = await _affiliateRepository.FirstOrDefaultAsync(ecl.OrganizationUnitId);
                if (affiliate.OverrideThreshold != null)
                {
                    var selectedRecord = await _lookup_obeEclFrameworkRepository.FirstOrDefaultAsync(e => e.ObeEclId == input.EclId && e.ContractNo == input.ContractId);
                    if (selectedRecord != null)
                    {
                        if (selectedRecord.Outstanding_Balance > affiliate.OverrideThreshold)
                        {
                            output.Status = false;
                            output.Message = L("ApplyOverrideErrorThresholdLimit");
                            return output;
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

            //var reviewedOverride = await _obeEclOverrideRepository.FirstOrDefaultAsync(x => x.ContractId == input.ContractId && x.Status != GeneralStatusEnum.Submitted);

            //if (reviewedOverride != null)
            //{
            //    output.Status = false;
            //    output.Message = L("ApplyOverrideErrorRecordReviewed");
            //}
            //else
            //{
            //    output.Status = true;
            //    output.Message = "";
            //}

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