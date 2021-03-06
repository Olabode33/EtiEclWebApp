﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.EclShared.Dtos;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;
using Microsoft.EntityFrameworkCore;
using Abp.Organizations;
using Abp.Authorization;
using TestDemo.Authorization;
using TestDemo.Investment;
using TestDemo.InvestmentComputation;
using Abp.UI;
using TestDemo.AffiliateMacroEconomicVariable;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using TestDemo.RetailComputation;
using TestDemo.WholesaleComputation;
using TestDemo.ObeComputation;
using TestDemo.WholesaleResults;
using TestDemo.RetailResults;
using TestDemo.ObeResults;
using TestDemo.BatchEcls;

namespace TestDemo.EclShared
{
    public class EclSharedAppService : TestDemoAppServiceBase, IEclSharedAppService
    {
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentclRepository;
        private readonly IRepository<BatchEcl, Guid> _batchEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptions;
        private readonly IRepository<AssumptionApproval, Guid> _assumptionsApprovalRepository;
        private readonly IRepository<Assumption, Guid> _frameworkAssumptionRepository;
        private readonly IRepository<EadInputAssumption, Guid> _eadAssumptionRepository;
        private readonly IRepository<LgdInputAssumption, Guid> _lgdAssumptionRepository;
        private readonly IRepository<PdInputAssumption, Guid> _pdAssumptionRepository;
        private readonly IRepository<PdInputAssumptionMacroeconomicInput, Guid> _pdAssumptionMacroEcoInputRepository;
        private readonly IRepository<PdInputAssumptionMacroeconomicProjection, Guid> _pdAssumptionMacroecoProjectionRepository;
        private readonly IRepository<PdInputAssumptionNonInternalModel, Guid> _pdAssumptionNonInternalModelRepository;
        private readonly IRepository<PdInputAssumptionNplIndex, Guid> _pdAssumptionNplIndexRepository;
        private readonly IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> _pdSnPCummulativeAssumptionRepository;
        private readonly IRepository<InvSecFitchCummulativeDefaultRate, Guid> _invsecFitchCummulativeAssumptionRepository;
        private readonly IRepository<InvSecMacroEconomicAssumption, Guid> _invsecMacroEcoAssumptionRepository;
        private readonly IRepository<InvestmentEclOverrideApproval, Guid> _investmentOverrideApprovalRepository;
        private readonly IRepository<RetailEclOverrideApproval, Guid> _retailOverrideApprovalRepository;
        private readonly IRepository<WholesaleEclOverrideApproval, Guid> _wholesaleOverrideApprovalRepository;
        private readonly IRepository<ObeEclOverrideApproval, Guid> _obeOverrideApprovalRepository;
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroVariableRepository;
        //Final Result Tables
        private readonly IRepository<InvestmentEclFinalResult, Guid> _investmentFinalEclResult;
        private readonly IRepository<InvestmentEclFinalPostOverrideResult, Guid> _investmentFinalEclOverrideResult;
        private readonly IRepository<WholesaleEclFramworkReportDetail, Guid> _wholesaleFinalEclResult;
        private readonly IRepository<RetailEclFramworkReportDetail, Guid> _retailFinalEclResult;
        private readonly IRepository<ObeEclFramworkReportDetail, Guid> _obeFinalEclResult;
        private readonly UserManager _userManager;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public EclSharedAppService(
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository, 
            IRepository<RetailEcl, Guid> retailEclRepository, 
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<InvestmentEcl, Guid> investmentclRepository,
            IRepository<BatchEcl, Guid> batchEclRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<Assumption, Guid> frameworkAssumptionRepository,
            IRepository<EadInputAssumption, Guid> eadAssumptionRepository,
            IRepository<LgdInputAssumption, Guid> lgdAssumptionRepository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptions,
            IRepository<AssumptionApproval, Guid> assumptionsApprovalRepository,
            IRepository<PdInputAssumption, Guid> pdAssumptionRepository,
            IRepository<PdInputAssumptionMacroeconomicInput, Guid> pdAssumptionMacroEcoInputRepository,
            IRepository<PdInputAssumptionMacroeconomicProjection, Guid> pdAssumptionMacroecoProjectionRepository,
            IRepository<PdInputAssumptionNonInternalModel, Guid> pdAssumptionNonInternalModelRepository,
            IRepository<PdInputAssumptionNplIndex, Guid> pdAssumptionNplIndexRepository,
            IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> pdSnPCummulativeAssumptionRepository,
            IRepository<InvSecFitchCummulativeDefaultRate, Guid> invsecFitchCummulativeAssumptionRepository,
            IRepository<InvSecMacroEconomicAssumption, Guid> invsecMacroEcoAssumptionRepository,
            IRepository<InvestmentEclOverrideApproval, Guid> investmentOverrideApprovalRepository,
            IRepository<RetailEclOverrideApproval, Guid> retailOverrideApprovalRepository,
            IRepository<WholesaleEclOverrideApproval, Guid> wholesaleOverrideApprovalRepository,
            IRepository<ObeEclOverrideApproval, Guid> obeOverrideApprovalRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IRepository<InvestmentEclFinalPostOverrideResult, Guid> investmentFinalEclOverrideResult,
            IRepository<InvestmentEclFinalResult, Guid> investmentFinalEclResult,
            IRepository<WholesaleEclFramworkReportDetail, Guid> wholesaleFinalEclResult,
            IRepository<RetailEclFramworkReportDetail, Guid> retailFinalEclResult,
            IRepository<ObeEclFramworkReportDetail, Guid> obeFinalEclResult,
            IBackgroundJobManager backgroundJobManager,
        UserManager userManager)
        {
            _wholesaleEclRepository = wholesaleEclRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentclRepository = investmentclRepository;
            _batchEclRepository = batchEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptions = affiliateAssumptions;
            _assumptionsApprovalRepository = assumptionsApprovalRepository;
            _userManager = userManager;
            _frameworkAssumptionRepository = frameworkAssumptionRepository;
            _eadAssumptionRepository = eadAssumptionRepository;
            _lgdAssumptionRepository = lgdAssumptionRepository;
            _pdAssumptionRepository = pdAssumptionRepository;
            _pdAssumptionMacroEcoInputRepository = pdAssumptionMacroEcoInputRepository;
            _pdAssumptionMacroecoProjectionRepository = pdAssumptionMacroecoProjectionRepository;
            _pdAssumptionNonInternalModelRepository = pdAssumptionNonInternalModelRepository;
            _pdAssumptionNplIndexRepository = pdAssumptionNplIndexRepository;
            _pdSnPCummulativeAssumptionRepository = pdSnPCummulativeAssumptionRepository;
            _invsecMacroEcoAssumptionRepository = invsecMacroEcoAssumptionRepository;
            _invsecFitchCummulativeAssumptionRepository = invsecFitchCummulativeAssumptionRepository;
            _investmentOverrideApprovalRepository = investmentOverrideApprovalRepository;
            _retailOverrideApprovalRepository = retailOverrideApprovalRepository;
            _wholesaleOverrideApprovalRepository = wholesaleOverrideApprovalRepository;
            _obeOverrideApprovalRepository = obeOverrideApprovalRepository;
            _affiliateMacroVariableRepository = affiliateMacroVariableRepository; 
            _investmentFinalEclResult = investmentFinalEclResult;
            _investmentFinalEclOverrideResult = investmentFinalEclOverrideResult;
            _wholesaleFinalEclResult = wholesaleFinalEclResult;
            _retailFinalEclResult = retailFinalEclResult;
            _obeFinalEclResult = obeFinalEclResult;
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<GetWorkspaceSummaryDataOutput> GetWorkspaceSummaryData()
        {
            GetWorkspaceSummaryDataOutput output = new GetWorkspaceSummaryDataOutput();

            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            if (userOrganizationUnitIds.Count() > 0)
            {

                output.AffiliateAssumptionNotUpdatedCount = await _affiliateAssumptions.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).CountAsync(x => (DateTime.Now.Date - x.LastAssumptionUpdate.Date).TotalDays > 30);
                output.AffiliateAssumptionYetToBeApprovedCount = await _assumptionsApprovalRepository.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                var iEcls = await _investmentclRepository.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
                var wEcls = await _wholesaleEclRepository.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
                var rEcls = await _retailEclRepository.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
                var oEcls = await _obeEclRepository.GetAll().Where(e => userOrganizationUnitIds.Contains(e.OrganizationUnitId)).Select(e => e.Id).ToListAsync();

                output.InvestmentSubmittedOverrideCount = await _investmentOverrideApprovalRepository.GetAll().Where(e => e.InvestmentEclOverrideId != null && iEcls.Contains((Guid)e.InvestmentEclOverrideId)).CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.WholesaleSubmittedOverrideCount = await _wholesaleOverrideApprovalRepository.GetAll().Where(e => wEcls.Contains(e.WholesaleEclId)).CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.RetailSubmittedOverrideCount = await _retailOverrideApprovalRepository.GetAll().Where(e => rEcls.Contains(e.RetailEclId)).CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.ObeSubmittedOverrideCount = await _obeOverrideApprovalRepository.GetAll().Where(e => oEcls.Contains(e.ObeEclId)).CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
            }
            else
            {
                output.AffiliateAssumptionNotUpdatedCount = await _affiliateAssumptions.CountAsync(x => (DateTime.Now.Date - x.LastAssumptionUpdate.Date).TotalDays > 30);
                output.AffiliateAssumptionYetToBeApprovedCount = await _assumptionsApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.InvestmentSubmittedOverrideCount = await _investmentOverrideApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.WholesaleSubmittedOverrideCount = await _wholesaleOverrideApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.RetailSubmittedOverrideCount = await _retailOverrideApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
                output.ObeSubmittedOverrideCount = await _obeOverrideApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
            }

            var draftEcls = await GetAllEclForWorkspace(new GetAllEclForWorkspaceInput { AffiliateId = 0, Portfolio = -1, Status = (int)EclStatusEnum.Draft });
            var submittedEcls = await GetAllEclForWorkspace(new GetAllEclForWorkspaceInput { AffiliateId = 0, Portfolio = -1, Status = (int)EclStatusEnum.Submitted });

            output.DraftEclCount = draftEcls.TotalCount;
            output.SubmittedEclCount = submittedEcls.TotalCount;

            return output;            
        }

        public async Task<List<GetAllEclForWorkspaceSummaryDto>> GetAllEclSummaryForWorkspace(GetAllEclForWorkspaceInput input)
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var portfolioFilter = (FrameworkEnum)input.Portfolio;
            var statusFilter = (EclStatusEnum)input.Status;


            var allEcl = (from w in _wholesaleEclRepository.GetAll().Where(e => e.BatchId == null).WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                          join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                          join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                          from u2 in u1.DefaultIfEmpty()
                          select new GetAllEclForWorkspaceSummaryDto()
                          {
                              Framework = FrameworkEnum.Wholesale,
                              CreatedByUserName = u2 == null ? "" : u2.FullName,
                              DateCreated = w.CreationTime,
                              ReportingDate = w.ReportingDate,
                              OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                              Status = w.Status,
                              Id = w.Id,
                              LastUpdated = w.LastModificationTime,
                              IsSingleBatch = w.IsSingleBatch
                          }
                          ).Union(
                            from w in _retailEclRepository.GetAll().Where(e => e.BatchId == null).WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceSummaryDto()
                            {
                                Framework = FrameworkEnum.Retail,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                LastUpdated = w.LastModificationTime,
                                IsSingleBatch = w.IsSingleBatch
                            }
                          ).Union(
                            from w in _obeEclRepository.GetAll().Where(e => e.BatchId == null).WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceSummaryDto()
                            {
                                Framework = FrameworkEnum.OBE,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                LastUpdated = w.LastModificationTime,
                                IsSingleBatch = w.IsSingleBatch
                            }
                          ).Union(
                            from w in _investmentclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceSummaryDto()
                            {
                                Framework = FrameworkEnum.Investments,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                LastUpdated = w.LastModificationTime,
                                IsSingleBatch = false
                            }
                          ).Union(
                            from w in _batchEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceSummaryDto()
                            {
                                Framework = FrameworkEnum.Batch,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                LastUpdated = w.LastModificationTime,
                                IsSingleBatch = false
                            }
                          );

            var pagedEcls = allEcl
                            .OrderBy("lastUpdated desc")
                            .Take(10);

            var totalCount = await allEcl.CountAsync();

            return await pagedEcls.ToListAsync();
        }

        public async Task<GetWorkspaceImpairmentSummaryDto> GetWorkspaceImpairmentSummary()
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var w_ecl = await _wholesaleEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
            var wholesale_results = await _wholesaleFinalEclResult.GetAll().Where(e => w_ecl.Contains((Guid)e.WholesaleEclId)).ToListAsync();

            var r_ecl = await _retailEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
            var retail_results = await _retailFinalEclResult.GetAll().Where(e => r_ecl.Contains((Guid)e.RetailEclId)).ToListAsync();

            var o_ecl = await _obeEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
            var obe_results = await _obeFinalEclResult.GetAll().Where(e => o_ecl.Contains((Guid)e.ObeEclId)).ToListAsync();

            var i_ecl = await _investmentclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId)).Select(e => e.Id).ToListAsync();
            var investment_results = await _investmentFinalEclResult.GetAll().Where(e => i_ecl.Contains((Guid)e.EclId)).ToListAsync();
            var investment_override_results = await _investmentFinalEclOverrideResult.GetAll().Where(e => i_ecl.Contains((Guid)e.EclId)).ToListAsync();


            double? wholesaleExposure = wholesale_results.Sum(e => e.Outstanding_Balance);
            double? wholesalePreOverride = wholesale_results.Sum(e => e.Impairment_ModelOutput);
            double? wholesalePostOverride = wholesale_results.Sum(e => e.Overrides_Impairment_Manual);

            double? retailExposure = retail_results.Sum(e => e.Outstanding_Balance);
            double? retailPreOverride = retail_results.Sum(e => e.Impairment_ModelOutput);
            double? retailPostOverride = retail_results.Sum(e => e.Overrides_Impairment_Manual);

            double? obeExposure = obe_results.Sum(e => e.Outstanding_Balance);
            double? obePreOverride = obe_results.Sum(e => e.Impairment_ModelOutput);
            double? obePostOverride = obe_results.Sum(e => e.Overrides_Impairment_Manual);

            double? investmentExposure = investment_results.Sum(x => x.Exposure);
            double? investmentPreOverride = investment_results.Sum(x => x.Impairment);
            double? investmentPostOverride = investment_results.Sum(x => x.Impairment);

            return new GetWorkspaceImpairmentSummaryDto
            {
                TotalExposure = wholesaleExposure + retailExposure + obeExposure + investmentExposure,
                TotalPreOverride = wholesalePreOverride + retailPreOverride + obePreOverride + investmentPreOverride,
                TotalPostOverride = wholesalePostOverride + retailPostOverride + obePostOverride + investmentPostOverride,
                WholesaleExposure = wholesaleExposure,
                WholesalePreOverride = wholesalePreOverride,
                WholesalePostOverride = wholesalePostOverride,
                RetailExposure = retailExposure,
                RetailPreOverride = retailPreOverride,
                RetailPostOverride = retailPostOverride,
                ObeExposure = obeExposure,
                ObePreOverride = obePreOverride,
                ObePostOverride = obePostOverride,
                InvestmentExposure = investmentExposure,
                InvestmentPreOverride = investmentPreOverride,
                InvestmentPostOverride = investmentPostOverride
            };
        }


        public async Task<PagedResultDto<GetAllEclForWorkspaceDto>> GetAllEclForWorkspace(GetAllEclForWorkspaceInput input)
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var portfolioFilter = (FrameworkEnum)input.Portfolio;
            var statusFilter = (EclStatusEnum)input.Status;


            var allEcl = (from w in _wholesaleEclRepository.GetAll().Where(e => e.BatchId == null)
                                                           .WhereIf(userOrganizationUnitIds.Count() > 0,  x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                           .WhereIf(userOrganizationUnitIds.Count() <= 0 && input.AffiliateId > -1, e => e.OrganizationUnitId == input.AffiliateId)

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
                                  Id = w.Id,
                                  IsSingleBatch = w.IsSingleBatch
                          }
                          ).Union(
                            from w in _retailEclRepository.GetAll().Where(e => e.BatchId == null)
                                                          .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                          .WhereIf(userOrganizationUnitIds.Count() <= 0 && input.AffiliateId > -1, e => e.OrganizationUnitId == input.AffiliateId)

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
                                Id = w.Id,
                                IsSingleBatch = w.IsSingleBatch
                            }
                          ).Union(
                            from w in _obeEclRepository.GetAll().Where(e => e.BatchId == null)
                                                       .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                       .WhereIf(userOrganizationUnitIds.Count() <= 0 && input.AffiliateId > -1, e => e.OrganizationUnitId == input.AffiliateId)

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
                                Id = w.Id,
                                IsSingleBatch = w.IsSingleBatch
                            }
                          ).Union(
                            from w in _investmentclRepository.GetAll()
                                                             .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                             .WhereIf(userOrganizationUnitIds.Count() <= 0 && input.AffiliateId > -1, e => e.OrganizationUnitId == input.AffiliateId)

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceDto()
                            {
                                Framework = FrameworkEnum.Investments,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                IsSingleBatch = false
                            }
                          ).Union(
                            from w in _batchEclRepository.GetAll()
                                                             .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                             .WhereIf(userOrganizationUnitIds.Count() <= 0 && input.AffiliateId > -1, e => e.OrganizationUnitId == input.AffiliateId)

                            join ou in _organizationUnitRepository.GetAll() on w.OrganizationUnitId equals ou.Id

                            join u in _lookup_userRepository.GetAll() on w.CreatorUserId equals u.Id into u1
                            from u2 in u1.DefaultIfEmpty()
                            select new GetAllEclForWorkspaceDto()
                            {
                                Framework = FrameworkEnum.Batch,
                                CreatedByUserName = u2 == null ? "" : u2.FullName,
                                DateCreated = w.CreationTime,
                                ReportingDate = w.ReportingDate,
                                OrganisationUnitName = ou == null ? "" : ou.DisplayName,
                                Status = w.Status,
                                Id = w.Id,
                                IsSingleBatch = false
                            }
                          )
                          //.WhereIf(string.IsNullOrWhiteSpace(input.Filter),  x => x.OrganisationUnitName.ToLower().Contains(input.Filter.ToLower()) || x.CreatedByUserName.ToLower().Contains(input.Filter.ToLower()))
                          .WhereIf(input.Portfolio > -1, x => x.Framework == portfolioFilter)
                          .WhereIf(input.Status > -1, x => x.Status == statusFilter);

            var pagedEcls = allEcl
                            .OrderBy(input.Sorting ?? "dateCreated desc")
                            .PageBy(input);

            var totalCount = await allEcl.CountAsync();

            return new PagedResultDto<GetAllEclForWorkspaceDto>(
                totalCount,
                await pagedEcls.ToListAsync()
            );
        }

        public async Task<List<AssumptionDto>> GetFrameworkAssumptionSnapshot(FrameworkEnum framework)
        {
            var assumptions = await _frameworkAssumptionRepository.GetAll()
                                                                .Where(x => x.Framework == framework)
                                                                .Select(x => new AssumptionDto
                                                                {
                                                                    AssumptionGroup = x.AssumptionGroup,
                                                                    Key = x.Key,
                                                                    InputName = x.InputName,
                                                                    Value = x.Value,
                                                                    DataType = x.DataType,
                                                                    IsComputed = x.IsComputed,
                                                                    RequiresGroupApproval = x.RequiresGroupApproval,
                                                                    Id = x.Id
                                                                }).ToListAsync();

            return assumptions;
        }

        public async Task<List<EadInputAssumptionDto>> GetEadInputAssumptionSnapshot(FrameworkEnum framework)
        {
            var assumptions = await _eadAssumptionRepository.GetAll()
                                                            .Where(x => x.Framework == framework)
                                                            .Select(x => new EadInputAssumptionDto
                                                            {
                                                                AssumptionGroup = x.EadGroup,
                                                                Key = x.Key,
                                                                InputName = x.InputName,
                                                                Value = x.Value,
                                                                DataType = x.Datatype,
                                                                IsComputed = x.IsComputed,
                                                                RequiresGroupApproval = x.RequiresGroupApproval,
                                                                Id = x.Id
                                                            }).ToListAsync();

            return assumptions;
        }

        public async Task<List<LgdAssumptionDto>> GetLgdInputAssumptionSnapshot(FrameworkEnum framework)
        {
            var assumptions = await _lgdAssumptionRepository.GetAll()
                                                                .Where(x => x.Framework == framework)
                                                                .Select(x => new LgdAssumptionDto
                                                                {
                                                                    AssumptionGroup = x.LgdGroup,
                                                                    Key = x.Key,
                                                                    InputName = x.InputName,
                                                                    Value = x.Value,
                                                                    DataType = x.DataType,
                                                                    IsComputed = x.IsComputed,
                                                                    RequiresGroupApproval = x.RequiresGroupApproval,
                                                                    Id = x.Id
                                                                }).ToListAsync();

            return assumptions;
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionsUpdate)]
        public async Task<PagedResultDto<GetAllAffiliateAssumptionDto>> GetAllAffiliateAssumption(GetAllForLookupTableInput input)
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var submittedAssumptions = await _assumptionsApprovalRepository.GetAll().Where(x => x.Status == GeneralStatusEnum.Submitted || x.Status == GeneralStatusEnum.AwaitngAdditionApproval).ToListAsync();
            var affiliates = _affiliateAssumptions.GetAll().Include(x => x.OrganizationUnitFk)
                                                  .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                                                  .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => (e.OrganizationUnitFk != null ? e.OrganizationUnitFk.DisplayName.ToLower().Contains(input.Filter.ToLower()) : false))
                                                    .Select(x => new GetAllAffiliateAssumptionDto
                                                    {
                                                        Id = x.Id,
                                                        OrganizationUnitId = x.OrganizationUnitFk == null ? 0 : x.OrganizationUnitFk.Id,
                                                        AffiliateName = x.OrganizationUnitFk == null ? "" : x.OrganizationUnitFk.DisplayName,
                                                        LastAssumptionUpdate = x.LastAssumptionUpdate,
                                                        LastWholesaleReportingDate = x.LastWholesaleReportingDate,
                                                        LastRetailReportingDate = x.LastRetailReportingDate,
                                                        LastObeReportingDate = x.LastObeReportingDate,
                                                        LastSecuritiesReportingDate = x.LastSecuritiesReportingDate,
                                                        RequiresAttention = (DateTime.Now.Date - x.LastAssumptionUpdate.Date).TotalDays > 30,
                                                        HasSubmittedAssumptions = submittedAssumptions.Any(y => y.OrganizationUnitId == x.OrganizationUnitId),
                                                        Status = x.Status
                                                    });

            var pagedEcls = affiliates
                            .OrderBy(input.Sorting ?? "requiresAttention desc")
                            .PageBy(input);

            var totalCount = await affiliates.CountAsync();

            return new PagedResultDto<GetAllAffiliateAssumptionDto>(
                totalCount,
                await pagedEcls.ToListAsync()
            );
        }

        public async Task<List<AssumptionDto>> GetAffiliateFrameworkAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _frameworkAssumptionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new AssumptionDto()
                               {
                                   AssumptionGroup = a.AssumptionGroup,
                                   Key = a.Key,
                                   InputName = a.InputName,
                                   Value = a.Value,
                                   DataType = a.DataType,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<EadInputAssumptionDto>> GetAffiliateEadAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _eadAssumptionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new EadInputAssumptionDto()
                               {
                                   AssumptionGroup = a.EadGroup,
                                   Key = a.Key,
                                   InputName = a.InputName,
                                   Value = a.Value,
                                   DataType = a.Datatype,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<LgdAssumptionDto>> GetAffiliateLgdAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _lgdAssumptionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new LgdAssumptionDto()
                               {
                                   AssumptionGroup = a.LgdGroup,
                                   Key = a.Key,
                                   InputName = a.InputName,
                                   Value = a.Value,
                                   DataType = a.DataType,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputAssumptionDto>> GetAffiliatePdAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdAssumptionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputAssumptionDto()
                               {
                                   AssumptionGroup = a.PdGroup,
                                   Key = a.Key,
                                   InputName = a.InputName,
                                   Value = a.Value,
                                   DataType = a.DataType,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputAssumptionMacroeconomicInputDto>> GetAffiliatePdMacroeconomicInputAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdAssumptionMacroEcoInputRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputAssumptionMacroeconomicInputDto
                               {
                                   AssumptionGroup = a.MacroeconomicVariableId,
                                   Key = a.Key,
                                   InputName = a.InputName,
                                   MacroeconomicVariable = a.MacroeconomicVariable == null ? "" : a.MacroeconomicVariable.Name,
                                   Value = a.Value,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetAffiliatePdMacroeconomicProjectionAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdAssumptionMacroecoProjectionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputAssumptionMacroeconomicProjectionDto
                               {
                                   AssumptionGroup = a.MacroeconomicVariableId,
                                   Key = a.Key,
                                   Date = a.Date,
                                   InputName = a.MacroeconomicVariable != null ? a.MacroeconomicVariable.Name : "",
                                   BestValue = a.BestValue,
                                   OptimisticValue = a.OptimisticValue,
                                   DownturnValue = a.DownturnValue,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputAssumptionNonInternalModelDto>> GetAffiliatePdNonInternalModelAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdAssumptionNonInternalModelRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputAssumptionNonInternalModelDto
                               {
                                   Key = a.Key,
                                   PdGroup = a.PdGroup,
                                   Month = a.Month,
                                   MarginalDefaultRate = a.MarginalDefaultRate,
                                   CummulativeSurvival = a.CummulativeSurvival,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputAssumptionNplIndexDto>> GetAffiliatePdNplIndexAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdAssumptionNplIndexRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputAssumptionNplIndexDto
                               {
                                   Key = a.Key,
                                   Date = a.Date,
                                   Actual = a.Actual,
                                   Standardised = a.Standardised,
                                   EtiNplSeries = a.EtiNplSeries,
                                   IsComputed = a.IsComputed,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<PdInputSnPCummulativeDefaultRateDto>> GetAffiliatePdSnpCummulativeAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _pdSnPCummulativeAssumptionRepository.GetAll().Where(x => x.Framework == input.Framework && x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new PdInputSnPCummulativeDefaultRateDto
                               {
                                   Key = a.Key,
                                   Rating = a.Rating,
                                   Years = a.Years,
                                   Value = a.Value,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<InvSecFitchCummulativeDefaultRateDto>> GetAffiliateInvSecPdFitchCummulativeAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _invsecFitchCummulativeAssumptionRepository.GetAll().Where(x => x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new InvSecFitchCummulativeDefaultRateDto
                               {
                                   Key = a.Key,
                                   Rating = a.Rating,
                                   Years = a.Year,
                                   Value = a.Value,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<List<InvSecMacroEconomicAssumptionDto>> GetAffiliateInvSecPdMacroEcoAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = (from a in _invsecMacroEcoAssumptionRepository.GetAll().Where(x => x.OrganizationUnitId == input.AffiliateOuId)
                               join u in _lookup_userRepository.GetAll() on a.LastModifierUserId equals u.Id into u1
                               from u2 in u1.DefaultIfEmpty()

                               select new InvSecMacroEconomicAssumptionDto
                               {
                                   Key = a.Key,
                                   Month = a.Month,
                                   BestValue = a.BestValue,
                                   OptimisticValue = a.OptimisticValue,
                                   DownturnValue = a.DownturnValue,
                                   CanAffiliateEdit = a.CanAffiliateEdit,
                                   IsComputed = false,
                                   RequiresGroupApproval = a.RequiresGroupApproval,
                                   OrganizationUnitId = a.OrganizationUnitId,
                                   Status = a.Status,
                                   LastUpdated = a.LastModificationTime,
                                   LastUpdatedBy = u2 == null ? "" : u2.FullName,
                                   Id = a.Id
                               }).ToListAsync();

            return await assumptions;
        }

        public async Task<GetAllPdAssumptionsDto> GetAllPdAssumptionsForAffiliate(GetAffiliateAssumptionInputDto input)
        {
            GetAllPdAssumptionsDto output = new GetAllPdAssumptionsDto();
            output.PdInputAssumption = await GetAffiliatePdAssumption(input);
            output.PdInputAssumptionMacroeconomicInput = await GetAffiliatePdMacroeconomicInputAssumption(input);
            output.PdInputAssumptionMacroeconomicProjections = await GetAffiliatePdMacroeconomicProjectionAssumption(input);
            output.PdInputAssumptionNonInternalModels = await GetAffiliatePdNonInternalModelAssumption(input);
            output.PdInputAssumptionNplIndex = await GetAffiliatePdNplIndexAssumption(input);
            output.PdInputSnPCummulativeDefaultRate = await GetAffiliatePdSnpCummulativeAssumption(input);

            return output;
        }

        public async Task<GetAllInvSecPdAssumptionsDto> GetAllInvSecPdAssumptionsForAffiliate(GetAffiliateAssumptionInputDto input)
        {
            GetAllInvSecPdAssumptionsDto output = new GetAllInvSecPdAssumptionsDto();
            output.PdInputAssumption = await GetAffiliatePdAssumption(input);
            output.PdInputAssumptionMacroeconomic = await GetAffiliateInvSecPdMacroEcoAssumption(input);
            output.PdInputFitchCummulativeDefaultRate = await GetAffiliateInvSecPdFitchCummulativeAssumption(input);

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionsUpdate)]
        public async Task UpdateAffiliateAssumption(CreateOrEditAffiliateAssumptionsDto input)
        {
            var assumption = await _affiliateAssumptions.FirstOrDefaultAsync((Guid)input.Id);

            await SumbitForApproval(input, assumption);

            ObjectMapper.Map(input, assumption);
        }

        public async Task<CreateOrEditAffiliateAssumptionsDto> GetAffiliateAssumptionForEdit(long input)
        {
            var assumption = await _affiliateAssumptions.FirstOrDefaultAsync(x => x.OrganizationUnitId == input);
            return new CreateOrEditAffiliateAssumptionsDto()
            {
                Id = assumption.Id,
                LastAssumptionUpdate = assumption.LastAssumptionUpdate,
                LastObeReportingDate = assumption.LastObeReportingDate,
                LastRetailReportingDate = assumption.LastRetailReportingDate,
                LastSecuritiesReportingDate = assumption.LastSecuritiesReportingDate,
                LastWholesaleReportingDate = assumption.LastWholesaleReportingDate,
                OrganizationUnitId = assumption.OrganizationUnitId,
                Status = assumption.Status
            };
        }

        private async Task SumbitForApproval(CreateOrEditAffiliateAssumptionsDto newValue, AffiliateAssumption oldValue)
        {
            await _assumptionsApprovalRepository.InsertAsync(new AssumptionApproval()
            {
                OrganizationUnitId = oldValue.OrganizationUnitId,
                Framework = !newValue.LastObeReportingDate.Date.Equals(oldValue.LastObeReportingDate.Date) ? FrameworkEnum.OBE : (!newValue.LastRetailReportingDate.Date.Equals(oldValue.LastRetailReportingDate.Date) ? FrameworkEnum.Retail : (!newValue.LastWholesaleReportingDate.Date.Equals(oldValue.LastWholesaleReportingDate.Date) ? FrameworkEnum.Wholesale : FrameworkEnum.Investments)),
                AssumptionType = AssumptionTypeEnum.ReportingDate,
                AssumptionGroup = "Reporting Date",
                InputName = "Reporting Date",
                OldValue = !newValue.LastObeReportingDate.Date.Equals(oldValue.LastObeReportingDate.Date) ? oldValue.LastObeReportingDate.ToShortDateString() : (!newValue.LastRetailReportingDate.Date.Equals(oldValue.LastRetailReportingDate.Date) ? oldValue.LastRetailReportingDate.ToShortDateString() : (!newValue.LastWholesaleReportingDate.Date.Equals(oldValue.LastWholesaleReportingDate.Date) ? oldValue.LastWholesaleReportingDate.ToShortDateString() : oldValue.LastSecuritiesReportingDate.ToShortDateString())),
                NewValue = !newValue.LastObeReportingDate.Date.Equals(oldValue.LastObeReportingDate.Date) ? newValue.LastObeReportingDate.ToShortDateString() : ((!newValue.LastRetailReportingDate.Date.Equals(oldValue.LastRetailReportingDate.Date)) ? newValue.LastRetailReportingDate.ToShortDateString() : (!newValue.LastWholesaleReportingDate.Date.Equals(oldValue.LastWholesaleReportingDate.Date) ? newValue.LastWholesaleReportingDate.ToShortDateString() : newValue.LastSecuritiesReportingDate.ToShortDateString())),
                AssumptionId = oldValue.Id,
                AssumptionEntity = EclEnums.Assumption,
                Status = GeneralStatusEnum.Submitted
            });
        }


        public async Task CopyAffiliateAssumptions(CopyAffiliateDto input)
        {
            await _backgroundJobManager.EnqueueAsync<Importing.CopyAffiliateAssumptionJob, CopyAffiliateAssumptionJobArgs>(new CopyAffiliateAssumptionJobArgs()
            {
                FromAffiliateId = input.FromAffiliateId,
                ToAffiliateId = input.ToAffiliateId,
                User = AbpSession.ToUserIdentifier()
            });
        }

        public async Task ApplyToAllAffiliates(ApplyAssumptionToAllAffiliateDto input)
        {
            var ouIds = _organizationUnitRepository.GetAll().Where(e => e.Id != input.FromAffiliateId).Select(e => e.Id);
            foreach (var item in ouIds)
            {
                await _backgroundJobManager.EnqueueAsync<Importing.ApplyAssumptionToAffiliateJob, ApplyAffiliateAssumptionJobArgs>(new ApplyAffiliateAssumptionJobArgs()
                {
                    FromAffiliateId = input.FromAffiliateId,
                    ToAffiliateId = item,
                    Framework = input.Framework,
                    Type = input.Type,
                    User = AbpSession.ToUserIdentifier()
                });
            }
        }

        public async Task ApplyToSelectedAffiliates(ApplyAssumptionToSelectedAffiliateDto input)
        {
            await _backgroundJobManager.EnqueueAsync<Importing.ApplyAssumptionToAffiliateJob, ApplyAffiliateAssumptionJobArgs>(new ApplyAffiliateAssumptionJobArgs()
            {
                FromAffiliateId = input.FromAffiliateId,
                ToAffiliateId = input.ToAffiliateId,
                Framework = input.Framework,
                Type = input.Type,
                User = AbpSession.ToUserIdentifier()
            });
        }


    }
}
