using Abp.Application.Services.Dto;
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

namespace TestDemo.EclShared
{
    public class EclSharedAppService : TestDemoAppServiceBase, IEclSharedAppService
    {
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentclRepository;
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
        private readonly UserManager _userManager;

        public EclSharedAppService(
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository, 
            IRepository<RetailEcl, Guid> retailEclRepository, 
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<InvestmentEcl, Guid> investmentclRepository,
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
        UserManager userManager)
        {
            _wholesaleEclRepository = wholesaleEclRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentclRepository = investmentclRepository;
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
        }

        public async Task<GetWorkspaceSummaryDataOutput> GetWorkspaceSummaryData()
        {
            GetWorkspaceSummaryDataOutput output = new GetWorkspaceSummaryDataOutput();
            output.AffiliateAssumptionNotUpdatedCount = await _affiliateAssumptions.CountAsync(x => (DateTime.Now.Date - x.LastAssumptionUpdate.Date).TotalDays > 30);
            output.AffiliateAssumptionYetToBeApprovedCount = await _assumptionsApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);
            output.InvestmentSubmittedOverrideCount = await _investmentOverrideApprovalRepository.CountAsync(x => x.Status == GeneralStatusEnum.Submitted);

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


            var allEcl = (from w in _wholesaleEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                              LastUpdated = w.LastModificationTime
                          }
                          ).Union(
                            from w in _retailEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                                LastUpdated = w.LastModificationTime
                            }
                          ).Union(
                            from w in _obeEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                                LastUpdated = w.LastModificationTime
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
                                LastUpdated = w.LastModificationTime
                            }
                          );

            var pagedEcls = allEcl
                            .OrderBy("lastUpdated desc")
                            .Take(10);

            var totalCount = await allEcl.CountAsync();

            return await pagedEcls.ToListAsync();
        }


        public async Task<PagedResultDto<GetAllEclForWorkspaceDto>> GetAllEclForWorkspace(GetAllEclForWorkspaceInput input)
        {
            var user = await _userManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await _userManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var portfolioFilter = (FrameworkEnum)input.Portfolio;
            var statusFilter = (EclStatusEnum)input.Status;


            var allEcl = (from w in _wholesaleEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0,  x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                            from w in _retailEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                            from w in _obeEclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                          ).Union(
                            from w in _investmentclRepository.GetAll().WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))

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
                                Id = w.Id
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
            var submittedAssumptions = await _assumptionsApprovalRepository.GetAll().Where(x => x.Status == GeneralStatusEnum.Submitted).ToListAsync();
            var affiliates = _affiliateAssumptions.GetAll().Include(x => x.OrganizationUnitFk)
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
                                                        HasSubmittedAssumptions = submittedAssumptions.Any(y => y.OrganizationUnitId == x.OrganizationUnitId)
                                                    });

            var pagedEcls = affiliates
                            .OrderBy("requiresAttention desc")
                            .PageBy(input);

            var totalCount = await affiliates.CountAsync();

            return new PagedResultDto<GetAllAffiliateAssumptionDto>(
                totalCount,
                await pagedEcls.ToListAsync()
            );
        }

        public async Task<List<AssumptionDto>> GetAffiliateFrameworkAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _frameworkAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a,u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId )
                                    .Select(x => new AssumptionDto
                                    {
                                        AssumptionGroup = x.Assumption.AssumptionGroup,
                                        Key = x.Assumption.Key,
                                        InputName = x.Assumption.InputName,
                                        Value = x.Assumption.Value,
                                        DataType = x.Assumption.DataType,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<EadInputAssumptionDto>> GetAffiliateEadAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _eadAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new EadInputAssumptionDto
                                    {
                                        AssumptionGroup = x.Assumption.EadGroup,
                                        Key = x.Assumption.Key,
                                        InputName = x.Assumption.InputName,
                                        Value = x.Assumption.Value,
                                        DataType = x.Assumption.Datatype,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<LgdAssumptionDto>> GetAffiliateLgdAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _lgdAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new LgdAssumptionDto
                                    {
                                        AssumptionGroup = x.Assumption.LgdGroup,
                                        Key = x.Assumption.Key,
                                        InputName = x.Assumption.InputName,
                                        Value = x.Assumption.Value,
                                        DataType = x.Assumption.DataType,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputAssumptionDto>> GetAffiliatePdAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputAssumptionDto
                                    {
                                        AssumptionGroup = x.Assumption.PdGroup,
                                        Key = x.Assumption.Key,
                                        InputName = x.Assumption.InputName,
                                        Value = x.Assumption.Value,
                                        DataType = x.Assumption.DataType,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy =  x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputAssumptionMacroeconomicInputDto>> GetAffiliatePdMacroeconomicInputAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdAssumptionMacroEcoInputRepository.GetAll().Include(x => x.MacroeconomicVariable)
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputAssumptionMacroeconomicInputDto
                                    {
                                        AssumptionGroup = x.Assumption.MacroeconomicVariableId,
                                        Key = x.Assumption.Key,
                                        InputName = x.Assumption.InputName,
                                        MacroeconomicVariable = x.Assumption.MacroeconomicVariable == null ? "" : x.Assumption.MacroeconomicVariable.Name,
                                        Value = x.Assumption.Value,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetAffiliatePdMacroeconomicProjectionAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdAssumptionMacroecoProjectionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputAssumptionMacroeconomicProjectionDto
                                    {
                                        AssumptionGroup = x.Assumption.MacroeconomicVariableId,
                                        Key = x.Assumption.Key,
                                        Date = x.Assumption.Date,
                                        InputName = x.Assumption.MacroeconomicVariable != null ? x.Assumption.MacroeconomicVariable.Name : "",
                                        BestValue = x.Assumption.BestValue,
                                        OptimisticValue = x.Assumption.OptimisticValue,
                                        DownturnValue = x.Assumption.DownturnValue,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputAssumptionNonInternalModelDto>> GetAffiliatePdNonInternalModelAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdAssumptionNonInternalModelRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputAssumptionNonInternalModelDto
                                    {
                                        Key = x.Assumption.Key,
                                        PdGroup = x.Assumption.PdGroup,
                                        Month = x.Assumption.Month,
                                        MarginalDefaultRate = x.Assumption.MarginalDefaultRate,
                                        CummulativeSurvival = x.Assumption.CummulativeSurvival,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputAssumptionNplIndexDto>> GetAffiliatePdNplIndexAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdAssumptionNplIndexRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputAssumptionNplIndexDto
                                    {
                                        Key = x.Assumption.Key,
                                        Date = x.Assumption.Date,
                                        Actual = x.Assumption.Actual,
                                        Standardised = x.Assumption.Standardised,
                                        EtiNplSeries = x.Assumption.EtiNplSeries,
                                        IsComputed = x.Assumption.IsComputed,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<PdInputSnPCummulativeDefaultRateDto>> GetAffiliatePdSnpCummulativeAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _pdSnPCummulativeAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.Framework == input.Framework && x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new PdInputSnPCummulativeDefaultRateDto
                                    {
                                        Key = x.Assumption.Key,
                                        Rating = x.Assumption.Rating,
                                        Years = x.Assumption.Years,
                                        Value = x.Assumption.Value,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<InvSecFitchCummulativeDefaultRateDto>> GetAffiliateInvSecPdFitchCummulativeAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _invsecFitchCummulativeAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new InvSecFitchCummulativeDefaultRateDto
                                    {
                                        Key = x.Assumption.Key,
                                        Rating = x.Assumption.Rating,
                                        Years = x.Assumption.Year,
                                        Value = x.Assumption.Value,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
        }

        public async Task<List<InvSecMacroEconomicAssumptionDto>> GetAffiliateInvSecPdMacroEcoAssumption(GetAffiliateAssumptionInputDto input)
        {
            var assumptions = await _invsecMacroEcoAssumptionRepository.GetAll()
                                    .Join(_lookup_userRepository.GetAll(), a => a.LastModifierUserId, u => u.Id, (a, u) => new { Assumption = a, User = u })
                                    .Where(x => x.Assumption.OrganizationUnitId == input.AffiliateOuId)
                                    .Select(x => new InvSecMacroEconomicAssumptionDto
                                    {
                                        Key = x.Assumption.Key,
                                        Month = x.Assumption.Month,
                                        BestValue = x.Assumption.BestValue,
                                        OptimisticValue = x.Assumption.OptimisticValue,
                                        DownturnValue = x.Assumption.DownturnValue,
                                        CanAffiliateEdit = x.Assumption.CanAffiliateEdit,
                                        IsComputed = false,
                                        RequiresGroupApproval = x.Assumption.RequiresGroupApproval,
                                        OrganizationUnitId = x.Assumption.OrganizationUnitId,
                                        Status = x.Assumption.Status,
                                        LastUpdated = x.Assumption.LastModificationTime,
                                        LastUpdatedBy = x.User == null ? "" : x.User.FullName,
                                        Id = x.Assumption.Id
                                    }).ToListAsync();

            return assumptions;
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
                OrganizationUnitId = assumption.OrganizationUnitId
            };
        }
    }
}
