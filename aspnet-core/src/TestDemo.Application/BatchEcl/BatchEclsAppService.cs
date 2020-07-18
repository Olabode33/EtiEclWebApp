using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclInterfaces;
using Abp.Organizations;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs;
using Abp.BackgroundJobs;
using TestDemo.ObeAssumption;
using TestDemo.Dto.Ecls;
using TestDemo.Dto.Approvals;
using TestDemo.EclShared.Dtos;
using Abp.UI;
using TestDemo.Reports.Jobs;
using TestDemo.Reports;
using Abp.Runtime.Session;
using Abp.Configuration;
using TestDemo.EclConfig;
using TestDemo.EclLibrary.Jobs;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.Common.Exporting;
using TestDemo.Dto.Inputs;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;
using TestDemo.Calibration;
using TestDemo.BatchEcls.BatchEclInput;
using TestDemo.ObeInputs.Dtos;
using TestDemo.BatchEcls.Job;
using TestDemo.Wholesale;
using TestDemo.Retail;
using TestDemo.OBE;
using TestDemo.WholesaleInputs;
using TestDemo.RetailInputs;

namespace TestDemo.BatchEcls
{
    [AbpAuthorize(AppPermissions.Pages_EclView)]
    public class BatchEclsAppService : TestDemoAppServiceBase//, IEclsAppService
    {
        private readonly IRepository<BatchEcl, Guid> _batchEclRepository;
        private readonly IRepository<BatchEclApproval, Guid> _batchApprovalsRepository;
        private readonly IRepository<BatchEclUpload, Guid> _uploadRepository;
        private readonly IRepository<BatchEclDataLoanBook, Guid> _loanbookRepository;
        private readonly IRepository<BatchEclDataPaymentSchedule, Guid> _paymentScheduleRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;
        private readonly IRepository<AssumptionApproval, Guid> _assumptionsApprovalRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadRepository;
        private readonly IRepository<ObeEclUpload, Guid> _obeUploadRepository;

        private readonly IRepository<CalibrationEadBehaviouralTerm, Guid> _eadBehaviouralTermCalibrationRepository;
        private readonly IRepository<CalibrationEadCcfSummary, Guid> _eadCcfSummaryCalibrationRepository;
        private readonly IRepository<CalibrationLgdHairCut, Guid> _lgdHaircutCalibrationRepository;
        private readonly IRepository<CalibrationLgdRecoveryRate, Guid> _lgdRecoveryRateCalibrationRepository;
        private readonly IRepository<CalibrationPdCrDr, Guid> _pdcrdrCalibrationRepository;
        private readonly IRepository<MacroAnalysis> _macroCalibrationRepository;

        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;
        private readonly IEclLoanbookExporter _loanbookExporter;
        private readonly IEclDataPaymentScheduleExporter _paymentScheduleExporter;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IExcelReportGenerator _reportGenerator;


        public BatchEclsAppService(
            IRepository<BatchEcl, Guid> batchEclRepository,
            IRepository<BatchEclApproval, Guid> batchApprovalsRepository,
            IRepository<BatchEclUpload, Guid> uploadRepository,
            IRepository<BatchEclDataLoanBook, Guid> loanbookRepository,
            IRepository<BatchEclDataPaymentSchedule, Guid> paymentScheduleRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
            IRepository<AssumptionApproval, Guid> assumptionsApprovalRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository,
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadRepository,
            IRepository<RetailEclUpload, Guid> retailUploadRepository,
            IRepository<ObeEclUpload, Guid> obeUploadRepository,

            IRepository<CalibrationEadBehaviouralTerm, Guid> eadBehaviouralTermCalibrationRepository,
            IRepository<CalibrationEadCcfSummary, Guid> eadCcfSummaryCalibrationRepository,
            IRepository<CalibrationLgdHairCut, Guid> lgdHaircutCalibrationRepository,
            IRepository<CalibrationLgdRecoveryRate, Guid> lgdRecoveryRateCalibrationRepository,
            IRepository<CalibrationPdCrDr, Guid> pdcrdrCalibrationRepository,
            IRepository<MacroAnalysis> macroCalibrationRepository,

            IBackgroundJobManager backgroundJobManager,
            IEclSharedAppService eclSharedAppService,
            IEclLoanbookExporter loanbookExporter,
            IEclDataPaymentScheduleExporter paymentScheduleExporter,
            IEclEngineEmailer emailer,
            IExcelReportGenerator reportGenerator,
            IHostingEnvironment env
            )
        {
            _batchEclRepository = batchEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;
            _assumptionsApprovalRepository = assumptionsApprovalRepository;
            _batchApprovalsRepository = batchApprovalsRepository;
            _obeEclRepository = obeEclRepository;
            _retailEclRepository = retailEclRepository;
            _wholesaleEclRepository = wholesaleEclRepository;
            _uploadRepository = uploadRepository;
            _loanbookRepository = loanbookRepository;
            _paymentScheduleRepository = paymentScheduleRepository;
            _wholesaleUploadRepository = wholesaleUploadRepository;
            _retailUploadRepository = retailUploadRepository;
            _obeUploadRepository = obeUploadRepository;

            _eadBehaviouralTermCalibrationRepository = eadBehaviouralTermCalibrationRepository;
            _eadCcfSummaryCalibrationRepository = eadCcfSummaryCalibrationRepository;
            _lgdHaircutCalibrationRepository = lgdHaircutCalibrationRepository;
            _lgdRecoveryRateCalibrationRepository = lgdRecoveryRateCalibrationRepository;
            _pdcrdrCalibrationRepository = pdcrdrCalibrationRepository;
            _macroCalibrationRepository = macroCalibrationRepository;

            _backgroundJobManager = backgroundJobManager;
            _eclSharedAppService = eclSharedAppService;
            _loanbookExporter = loanbookExporter;
            _paymentScheduleExporter = paymentScheduleExporter;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _reportGenerator = reportGenerator;
        }

        #region Uploads
        public async Task<List<GetBatchEclUploadForViewDto>> GetEclUploads(EntityDto<Guid> input)
        {
            var eclUploads = from o in _uploadRepository.GetAll().Where(x => x.BatchId == input.Id)
                             join o1 in _batchEclRepository.GetAll() on o.BatchId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join u in _lookup_userRepository.GetAll() on o.CreatorUserId equals u.Id into u1
                             from u2 in u1.DefaultIfEmpty()

                             select new GetBatchEclUploadForViewDto()
                             {
                                 EclUpload = new BatchEclUploadDto
                                 {
                                     DocType = o.DocType,
                                     UploadComment = o.UploadComment,
                                     Status = o.Status,
                                     EclId = (Guid)o.BatchId,
                                     Id = o.Id,
                                     FileUploaded = o.FileUploaded,
                                     AllJobs = o.AllJobs,
                                     CompletedJobs = o.CompletedJobs,
                                     CountObeData = o.CountObeData,
                                     CountRetailData = o.CountRetailData,
                                     CountWholesaleData = o.CountWholesaleData,
                                     CountTotalData = o.CountTotalData,
                                 },
                                 DateUploaded = o.CreationTime,
                                 UploadedBy = u2 == null ? "" : u2.FullName
                             };

            return await eclUploads.ToListAsync();
        }
        public async Task<Guid> CreateUploadRecord(CreateOrEditObeEclUploadDto input)
        {
            var eclUploadExist = await _uploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.BatchId == input.EclId);

            if (eclUploadExist != null)
            {
                await _uploadRepository.DeleteAsync(eclUploadExist.Id);
            }

            if (input.DocType == UploadDocTypeEnum.PaymentSchedule)
            {
                var loanbook = await _uploadRepository.FirstOrDefaultAsync(x => x.DocType == UploadDocTypeEnum.LoanBook && x.BatchId == input.EclId);
                if (loanbook != null)
                {
                    if (loanbook.Status != GeneralStatusEnum.Completed)
                    {
                        throw new UserFriendlyException(L("LoanbookInProcessForBatch"));
                    }
                } 
                else
                {
                    throw new UserFriendlyException(L("NoLoanbookForBatch"));
                }
            }

            var id = await _uploadRepository.InsertAndGetIdAsync(new BatchEclUpload
            {
                BatchId = (Guid)input.EclId,
                DocType = input.DocType,
                Status = input.Status
            });
            await CreateWholesaleUploadSummaryForSub((Guid)input.EclId, input);
            await CreateRetailUploadSummaryForSub((Guid)input.EclId, input);
            await CreateObeUploadSummaryForSub((Guid)input.EclId, input);
            return id;
        }
        public async Task DeleteUploadRecord(EntityDto<Guid> input)
        {
            await _uploadRepository.DeleteAsync(input.Id);
        }
        private async Task CreateWholesaleUploadSummaryForSub(Guid batchId, CreateOrEditObeEclUploadDto input)
        {
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var eclUploadExist = await _wholesaleUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.WholesaleEclId == ecl.Id);

            if (eclUploadExist != null)
            {
                await _wholesaleUploadRepository.DeleteAsync(eclUploadExist.Id);
            }

            var id = await _wholesaleUploadRepository.InsertAndGetIdAsync(new WholesaleEclUpload {
                WholesaleEclId = ecl.Id,
                DocType = input.DocType,
                Status = GeneralStatusEnum.Completed
            });
        }
        private async Task CreateRetailUploadSummaryForSub(Guid batchId, CreateOrEditObeEclUploadDto input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var eclUploadExist = await _retailUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.RetailEclId == ecl.Id);

            if (eclUploadExist != null)
            {
                await _retailUploadRepository.DeleteAsync(eclUploadExist.Id);
            }

            var id = await _retailUploadRepository.InsertAndGetIdAsync(new RetailEclUpload {
                RetailEclId = ecl.Id,
                DocType = input.DocType,
                Status = GeneralStatusEnum.Completed
            });
        }
        private async Task CreateObeUploadSummaryForSub(Guid batchId, CreateOrEditObeEclUploadDto input)
        {
            var ecl = await _obeEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var eclUploadExist = await _obeUploadRepository.FirstOrDefaultAsync(x => x.DocType == input.DocType && x.ObeEclId == ecl.Id);

            if (eclUploadExist != null)
            {
                await _obeUploadRepository.DeleteAsync(eclUploadExist.Id);
            }

            var id = await _obeUploadRepository.InsertAndGetIdAsync(new ObeEclUpload {
                ObeEclId = ecl.Id,
                DocType = input.DocType,
                Status = GeneralStatusEnum.Completed
            });
        }
        #endregion

        #region Audit info
        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredObeEclApprovals = _batchApprovalsRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.BatchEclFk)
                        .Where(e => e.BatchEclId == input.Id);

            var obeEclApprovals = from o in filteredObeEclApprovals
                                  join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  select new EclApprovalAuditInfoDto()
                                  {
                                      EclId = (Guid)o.BatchEclId,
                                      ReviewedDate = o.CreationTime,
                                      Status = o.Status,
                                      ReviewComment = o.ReviewComment,
                                      ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                  };

            var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)ecl.CreatorUserId).FullName;
            string updatedBy = "";
            if (ecl.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)ecl.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await obeEclApprovals.ToListAsync(),
                DateCreated = ecl.CreationTime,
                LastUpdated = ecl.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        #endregion


        public async Task CreateOrEdit(CreateOrEditEclDto input)
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

        protected virtual async Task Create(CreateOrEditEclDto input)
        {
            var obeEcl = ObjectMapper.Map<BatchEcl>(input);
            await _batchEclRepository.InsertAsync(obeEcl);
        }

        protected virtual async Task Update(CreateOrEditEclDto input)
        {
            var obeEcl = await _batchEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            obeEcl.ReportingDate = input.ReportingDate;
            await _batchEclRepository.UpdateAsync(obeEcl);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _batchEclRepository.DeleteAsync(input.Id);
        }

        public async Task<GetBatchEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input)
        {
            var batch = await _batchEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBatchEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditEclDto>(batch) };
            if (batch.CreatorUserId != null)
            {
                var _creatorUser = await _lookup_userRepository.FirstOrDefaultAsync((long)batch.CreatorUserId);
                output.CreatedByUserName = _creatorUser.FullName.ToString();
            }

            if (batch.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((long)batch.OrganizationUnitId);
                output.Country = ou.DisplayName;
            }

            if (output.EclDto.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.EclDto.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.FullName.ToString();
            }

            output.SubEcls = await GetSubEcls(batch.Id);

            return output;
        }

        private async Task<List<GetAllEclForWorkspaceDto>> GetSubEcls(Guid batchId)
        {
            var subEcls = (from w in _wholesaleEclRepository.GetAll().Where(e => e.BatchId == batchId)
                           join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id
                           join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                           from u2 in u1.DefaultIfEmpty()
                           select new GetAllEclForWorkspaceDto()
                           {
                               Framework = FrameworkEnum.Wholesale,
                               CreatedByUserName = u2 == null ? "" : u2.FullName,
                               DateCreated = w.CreationTime,
                               ReportingDate = w.ReportingDate,
                               OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                               Status = w.Status,
                               Id = w.Id
                           }
                          ).Union(
                            from w in _retailEclRepository.GetAll().Where(e => e.BatchId == batchId)
                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id
                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceDto()
                            {
                                Framework = FrameworkEnum.Retail,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id
                            }
                          ).Union(
                            from w in _obeEclRepository.GetAll().Where(e => e.BatchId == batchId)
                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id
                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceDto()
                            {
                                Framework = FrameworkEnum.OBE,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id
                            }
                          );

            var subsEclList = await subEcls.ToListAsync();

            await UpdateBatchStatus(batchId, subEcls);

            return subsEclList;
        }

        private async Task UpdateBatchStatus(Guid batchId, IQueryable<GetAllEclForWorkspaceDto> subEcls)
        {
            var batch = await _batchEclRepository.FirstOrDefaultAsync(batchId);
            if (subEcls.All(x => x.Status == EclStatusEnum.Completed))
            {
                batch.Status = EclStatusEnum.Completed;
            }
            else if (subEcls.All(x => x.Status == EclStatusEnum.PostOverrideComplete || x.Status == EclStatusEnum.Completed))
            {
                batch.Status = EclStatusEnum.PostOverrideComplete;
            }
            else if (subEcls.All(x => x.Status == EclStatusEnum.PreOverrideComplete || x.Status == EclStatusEnum.PostOverrideComplete || x.Status == EclStatusEnum.Completed))
            {
                batch.Status = EclStatusEnum.PreOverrideComplete;
            }
            await _batchEclRepository.UpdateAsync(batch);
        }

        [AbpAuthorize(AppPermissions.Pages_Workspace_CreateEcl)]
        public async Task<Guid> CreateEclAndAssumption(CreateOrEditEclDto input)
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            AffiliateAssumption affiliateAssumption = new AffiliateAssumption();
            long ouId = -1;

            if (userSubsidiaries.Count > 0)
            {
                ouId = userSubsidiaries[0].Id;
                affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == ouId);
            }
            else
            {
                if (input.OrganizationUnitId != null)
                {
                    ouId = (long)input.OrganizationUnitId;
                    affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == input.OrganizationUnitId);
                }
                else
                {

                    throw new UserFriendlyException(L("UserDoesNotBelongToAnyAffiliateError"));
                }
            }

            if (affiliateAssumption != null)
            {
                await ValidateForCreation(ouId);

                Guid eclId = await CreateAndGetId(ouId, input.ReportingDate);
                await _backgroundJobManager.EnqueueAsync<CreateRetailEclJob, CreateSubEclJobArgs>(new CreateSubEclJobArgs()
                {
                    BatchId = eclId,
                    Framework = FrameworkEnum.Retail,
                    UserId = (long)AbpSession.UserId
                });
                await _backgroundJobManager.EnqueueAsync<CreateWholesaleEclJob, CreateSubEclJobArgs>(new CreateSubEclJobArgs()
                {
                    BatchId = eclId,
                    Framework = FrameworkEnum.Wholesale,
                    UserId = (long)AbpSession.UserId
                });
                await _backgroundJobManager.EnqueueAsync<CreateObeEclJob, CreateSubEclJobArgs>(new CreateSubEclJobArgs()
                {
                    BatchId = eclId,
                    Framework = FrameworkEnum.OBE,
                    UserId = (long)AbpSession.UserId
                });
                return eclId;
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }

        protected virtual async Task<Guid> CreateAndGetId(long ouId, DateTime reportDate)
        {
            var affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == ouId);

            if (affiliateAssumption != null)
            {

                Guid id = await _batchEclRepository.InsertAndGetIdAsync(new BatchEcl()
                {
                    ReportingDate = reportDate,
                    OrganizationUnitId = ouId,
                    Status = EclStatusEnum.LoadingAssumptions
                });
                //affiliateAssumption.LastObeReportingDate = reportDate;
                //await _affiliateAssumptionRepository.UpdateAsync(affiliateAssumption);
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        
        [AbpAuthorize(AppPermissions.Pages_EclView_Submit)]
        public async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var ecl = await _batchEclRepository.FirstOrDefaultAsync((Guid)input.Id);
                ecl.Status = EclStatusEnum.Submitted;
                ObjectMapper.Map(ecl, ecl);
                await SendSubmittedEmail(input.Id);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Review)]
        public async Task ApproveReject(CreateOrEditEclApprovalDto input)
        {
            var ecl = await _batchEclRepository.FirstOrDefaultAsync((Guid)input.EclId);

            await _batchApprovalsRepository.InsertAsync(new BatchEclApproval
            {
                BatchEclId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status,
                OrganizationUnitId = ecl.OrganizationUnitId
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _batchApprovalsRepository.GetAllListAsync(x => x.BatchEclId == input.EclId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    ecl.Status = EclStatusEnum.Approved;
                    await SendApprovedEmail(ecl.Id);
                }
                else
                {
                    ecl.Status = EclStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail(ecl.Id);
                }
            }
            else
            {
                ecl.Status = EclStatusEnum.Draft;
            }

            ObjectMapper.Map(ecl, ecl);
        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Run)]
        public async Task RunEcl(EntityDto<Guid> input)
        {
            var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);
            if (ecl.Status == EclStatusEnum.Approved)
            {
                //ecl.Status = EclStatusEnum.Running;
                //await _batchEclRepository.UpdateAsync(ecl);
                await RunSubEcls(ecl.Id);
            }
            else
            {
                throw new UserFriendlyException(L("EclMustBeApprovedBeforeRunning"));
            }
        }

        private async Task RunSubEcls(Guid batchId)
        {
            var uploads = await _uploadRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var w = await _wholesaleEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var r = await _retailEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
            var o = await _obeEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);

            if (w != null && uploads.CountWholesaleData > 0)
            {
                w.Status = EclStatusEnum.Running;
                await _wholesaleEclRepository.UpdateAsync(w);
            }
            if (r != null && uploads.CountRetailData > 0)
            {
                r.Status = EclStatusEnum.Running;
                await _retailEclRepository.UpdateAsync(r);
            }
            if (o != null && uploads.CountObeData > 0)
            {
                o.Status = EclStatusEnum.Running;
                await _obeEclRepository.UpdateAsync(o);
            }

        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Run)]
        public async Task RunPostEcl(EntityDto<Guid> input)
        {
            var validation = await ValidateForPostRun(input.Id);
            if (validation.Status)
            {
                var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);
                ecl.Status = EclStatusEnum.QueuePostOverride;
                await _batchEclRepository.UpdateAsync(ecl);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public async Task GenerateReport(EntityDto<Guid> input)
        {
            //var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);

            //if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed || ecl.Status == EclStatusEnum.Closed)
            //{
            //    await _backgroundJobManager.EnqueueAsync<GenerateEclReportJob, GenerateReportJobArgs>(new GenerateReportJobArgs()
            //    {
            //        eclId = input.Id,
            //        eclType = EclType.Obe,
            //        userIdentifier = AbpSession.ToUserIdentifier()
            //    });
            //}
            //else
            //{
            //    throw new UserFriendlyException(L("GenerateReportErrorEclNotRun"));
            //}
        }

        public async Task<FileDto> DownloadReport(EntityDto<Guid> input)
        {
            return null;
            //var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);

            //if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed || ecl.Status == EclStatusEnum.Closed)
            //{
            //    return _reportGenerator.DownloadExcelReport(new GenerateReportJobArgs()
            //    {
            //        eclId = input.Id,
            //        eclType = EclType.Obe,
            //        userIdentifier = AbpSession.ToUserIdentifier()
            //    });
            //}
            //else
            //{
            //    throw new UserFriendlyException(L("GenerateReportErrorEclNotRun"));
            //}
        }

        [AbpAuthorize(AppPermissions.Pages_EclView_Close)]
        public async Task CloseEcl(EntityDto<Guid> input)
        {
            //var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);

            //if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed)
            //{
            //    await _backgroundJobManager.EnqueueAsync<CloseEclJob, RunEclJobArgs>(new RunEclJobArgs()
            //    {
            //        EclId = input.Id,
            //        EclType = EclType.Obe,
            //        UserIdentifier = AbpSession.ToUserIdentifier()
            //    });
            //}
            //else
            //{
            //    throw new UserFriendlyException(L("CloseEcltErrorEclNotRun"));
            //}
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Reopen)]
        public async Task ReopenEcl(EntityDto<Guid> input)
        {
            //var ecl = await _batchEclRepository.FirstOrDefaultAsync(input.Id);

            //if (ecl.Status == EclStatusEnum.Closed)
            //{
            //    await _backgroundJobManager.EnqueueAsync<ReopenEclJob, RunEclJobArgs>(new RunEclJobArgs()
            //    {
            //        EclId = input.Id,
            //        EclType = EclType.Obe,
            //        UserIdentifier = AbpSession.ToUserIdentifier()
            //    });
            //}
            //else
            //{
            //    throw new UserFriendlyException(L("ReopenEcltErrorEclNotRun"));
            //}
        }

        public async Task<FileDto> ExportLoanBookToExcel(EntityDto<Guid> input)
        {
            var items = await _loanbookRepository.GetAll().Where(x => x.BatchId == input.Id)
                                                         .Select(x => ObjectMapper.Map<EclDataLoanBookDto>(x))
                                                         .ToListAsync();

            return _loanbookExporter.ExportToFile(items);
        }

        public async Task<FileDto> ExportPaymentScheduleToExcel(EntityDto<Guid> input)
        {
            var items = await _paymentScheduleRepository.GetAll().Where(x => x.BatchId == input.Id)
                                                         .Select(x => ObjectMapper.Map<EclDataPaymentScheduleDto>(x))
                                                         .ToListAsync();

            return _paymentScheduleExporter.ExportToFile(items);
        }

        protected async Task ValidateForCreation(long ouId)
        {
            var submittedAssumptions = await _assumptionsApprovalRepository.CountAsync(x => x.OrganizationUnitId == ouId && (x.Status == GeneralStatusEnum.Submitted || x.Status == GeneralStatusEnum.AwaitngAdditionApproval));
            if (submittedAssumptions > 0)
            {
                throw new UserFriendlyException(L("SubmittedAssumptionsYetToBeApproved"));
            }

            //Behavioural Term Check
            var appliedBehaviouralTerm = await _eadBehaviouralTermCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl 
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)  
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedBehaviouralTerm <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("CalibrationEadBehaviouralTerm")));
            }
            //CCF Check
            var appliedccf = await _eadCcfSummaryCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedccf <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("CalibrationEadCcfSummary")));
            }
            //Haircut
            var appliedhaircut = await _lgdHaircutCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedhaircut <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("CalibrationLgdHairCut")));
            }
            //Recovery rate check
            var appliedRecoveryRate = await _lgdRecoveryRateCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedRecoveryRate <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("CalibrationLgdRecoveryRate")));
            }
            //PdCrDr check
            var appliedPdCrDr = await _pdcrdrCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedPdCrDr <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("CalibrationPdCrDr")));
            }
            //Behavioural Term Check
            var appliedmacro = await _macroCalibrationRepository.CountAsync(e => e.OrganizationUnitId == ouId 
                                                                                                     && e.Status == CalibrationStatusEnum.AppliedToEcl
                                                                                                     && ((e.ModelType == FrameworkEnum.OBE && e.ModelType == FrameworkEnum.Retail && e.ModelType == FrameworkEnum.Wholesale)
                                                                                                            || e.ModelType == FrameworkEnum.All));
            if (appliedmacro <= 0)
            {
                throw new UserFriendlyException(L("NoAppliedCalibrationForAffiliate", L("MacroAnalysis")));
            }
        }
        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(Guid eclId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _uploadRepository.GetAllListAsync(x => x.BatchId == eclId);
            if (uploads.Count > 0)
            {
                var hasLoanBook = uploads.Any(x => x.DocType == UploadDocTypeEnum.LoanBook);
                var hasPaymentSchedule = uploads.Any(x => x.DocType == UploadDocTypeEnum.PaymentSchedule);
                var notCompleted = uploads.Any(x => x.Status != GeneralStatusEnum.Completed);

                if (!notCompleted && hasPaymentSchedule && hasLoanBook)
                {
                    output.Status = true;
                    output.Message = "";
                }
                else
                {
                    output.Status = false;
                    output.Message = (notCompleted == true ? L("UploadInProgressError") : "") + (!hasLoanBook ? L("LoanBookNotUploadedForEcl") : "") + (!hasPaymentSchedule ? L("PaymentScheduleNotUploadedForEcl") : "");
                }
            }
            else
            {
                output.Status = false;
                output.Message = L("NoUploadedRecordFoundForEcl");
            }

            return output;
        }
        protected virtual async Task<ValidationMessageDto> ValidateForPostRun(Guid eclId)
        {
            ValidationMessageDto output = new ValidationMessageDto();
            //Check if Ecl has overrides
            //var overrides = await _obeOverridesRepository.GetAllListAsync(x => x.ObeEclDataLoanBookId == eclId);
            //if (overrides.Count > 0)
            //{
            //    var submitted = overrides.Any(x => x.Status == GeneralStatusEnum.Submitted || x.Status == GeneralStatusEnum.AwaitngAdditionApproval);
            //    output.Status = !submitted;
            //    output.Message = submitted == true ? L("PostRunErrorYetToReviewSubmittedOverrides") : "";
            //}
            //else
            //{
            //    output.Status = false;
            //    output.Message = L("NoOverrideRecordFoundForEcl");
            //}

            return output;
        }

        public async Task SendSubmittedEmail(Guid eclId)
        {
            var ecl = _batchEclRepository.FirstOrDefault((Guid)eclId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            var users = await _lookup_userRepository.GetAllListAsync();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId) && await UserManager.IsGrantedAsync(user.Id, AppPermissions.Pages_EclView_Review))
                    {
                        int frameworkId = (int)FrameworkEnum.Batch;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Batch ECL";
                        await _emailer.SendEmailSubmittedForApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendAdditionalApprovalEmail(Guid eclId)
        {
            var ecl = _batchEclRepository.FirstOrDefault((Guid)eclId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            //PermissionChecker.IsGrantedAsync()
            var users = await _lookup_userRepository.GetAllListAsync();
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId) && await UserManager.IsGrantedAsync(user.Id, AppPermissions.Pages_EclView_Review))
                    {
                        int frameworkId = (int)FrameworkEnum.Batch;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Batch ECL";
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Batch;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Batch ECL";
            var ecl = _batchEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }

    }
}