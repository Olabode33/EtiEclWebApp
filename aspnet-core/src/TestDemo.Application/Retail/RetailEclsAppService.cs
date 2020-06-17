using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TestDemo.RetailAssumption;
using TestDemo.EclShared.Dtos;
using TestDemo.RetailAssumption.Dtos;
using Abp.Organizations;
using Abp.BackgroundJobs;
using TestDemo.EclLibrary.BaseEngine.PDInput;
using Abp.UI;
using TestDemo.RetailInputs;
using TestDemo.RetailComputation;
using TestDemo.EclInterfaces;
using TestDemo.Dto.Ecls;
using TestDemo.Dto.Approvals;
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

namespace TestDemo.Retail
{
    [AbpAuthorize(AppPermissions.Pages_RetailEcls)]
    public class RetailEclsAppService : TestDemoAppServiceBase, IEclsAppService
    {
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;
        private readonly IRepository<AssumptionApproval, Guid> _assumptionsApprovalRepository;
        private readonly IRepository<RetailEclApproval, Guid> _retailApprovalsRepository;
        private readonly IRepository<RetailEclOverride, Guid> _retailOverridesRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadRepository;
        private readonly IRepository<RetailEclDataLoanBook, Guid> _loanbookRepository;
        private readonly IRepository<RetailEclDataPaymentSchedule, Guid> _paymentScheduleRepository;

        private readonly IRepository<RetailEclAssumption, Guid> _eclAssumptionRepository;
        private readonly IRepository<RetailEclEadInputAssumption, Guid> _eclEadInputAssumptionRepository;
        private readonly IRepository<RetailEclLgdAssumption, Guid> _eclLgdAssumptionRepository;
        private readonly IRepository<RetailEclPdAssumption, Guid> _eclPdAssumptionRepository;
        private readonly IRepository<RetailEclPdAssumptionMacroeconomicInput, Guid> _eclPdAssumptionMacroeconomicInputsRepository;
        private readonly IRepository<RetailEclPdAssumptionMacroeconomicProjection, Guid> _eclPdAssumptionMacroeconomicProjectionRepository;
        private readonly IRepository<RetailEclPdAssumptionNonInteralModel, Guid> _eclPdAssumptionNonInternalModelRepository;
        private readonly IRepository<RetailEclPdAssumptionNplIndex, Guid> _eclPdAssumptionNplIndexRepository;
        private readonly IRepository<RetailEclPdSnPCummulativeDefaultRate, Guid> _eclPdSnPCummulativeDefaultRateRepository;

        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;
        private readonly IEclLoanbookExporter _loanbookExporter;
        private readonly IEclDataPaymentScheduleExporter _paymentScheduleExporter;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;

        public RetailEclsAppService(IRepository<RetailEcl, Guid> retailEclRepository,
                                    IRepository<User, long> lookup_userRepository,
                                    IRepository<OrganizationUnit, long> organizationUnitRepository,
                                    IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
                                    IRepository<AssumptionApproval, Guid> assumptionsApprovalRepository,
                                    IRepository<RetailEclApproval, Guid> retailApprovalsRepository,
                                    IRepository<RetailEclOverride, Guid> retailOverridesRepository,
                                    IRepository<RetailEclUpload, Guid> retailUploadRepository,
                                    IRepository<RetailEclDataLoanBook, Guid> loanbookRepository,
                                    IRepository<RetailEclDataPaymentSchedule, Guid> paymentScheduleRepository,
                                    IRepository<RetailEclAssumption, Guid> eclAssumptionRepository,
                                    IRepository<RetailEclEadInputAssumption, Guid> eclEadInputAssumptionRepository,
                                    IRepository<RetailEclLgdAssumption, Guid> eclLgdAssumptionRepository,
                                    IRepository<RetailEclPdAssumption, Guid> eclPdAssumptionRepository,
                                    IRepository<RetailEclPdAssumptionMacroeconomicInput, Guid> eclPdAssumptionMacroeconomicInputsRepository,
                                    IRepository<RetailEclPdAssumptionMacroeconomicProjection, Guid> eclPdAssumptionMacroeconomicProjectionRepository,
                                    IRepository<RetailEclPdAssumptionNonInteralModel, Guid> eclPdAssumptionNonInternalModelRepository,
                                    IRepository<RetailEclPdAssumptionNplIndex, Guid> eclPdAssumptionNplIndexRepository,
                                    IRepository<RetailEclPdSnPCummulativeDefaultRate, Guid> eclPdSnPCummulativeDefaultRateRepository,
                                    IBackgroundJobManager backgroundJobManager,
                                    IEclSharedAppService eclSharedAppService,
                                    IEclLoanbookExporter loanbookExporter,
                                    IEclEngineEmailer emailer,
                                    IHostingEnvironment env,
                                    IEclDataPaymentScheduleExporter paymentScheduleExporter
                                    )
        {
            _retailEclRepository = retailEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;
            _assumptionsApprovalRepository = assumptionsApprovalRepository;
            _retailApprovalsRepository = retailApprovalsRepository;
            _retailOverridesRepository = retailOverridesRepository;
            _retailUploadRepository = retailUploadRepository;
            _loanbookRepository = loanbookRepository;
            _paymentScheduleRepository = paymentScheduleRepository;

            _eclAssumptionRepository = eclAssumptionRepository;
            _eclEadInputAssumptionRepository = eclEadInputAssumptionRepository;
            _eclLgdAssumptionRepository = eclLgdAssumptionRepository;
            _eclPdAssumptionRepository = eclPdAssumptionRepository;
            _eclPdAssumptionMacroeconomicInputsRepository = eclPdAssumptionMacroeconomicInputsRepository;
            _eclPdAssumptionMacroeconomicProjectionRepository = eclPdAssumptionMacroeconomicProjectionRepository;
            _eclPdAssumptionNonInternalModelRepository = eclPdAssumptionNonInternalModelRepository;
            _eclPdAssumptionNplIndexRepository = eclPdAssumptionNplIndexRepository;
            _eclPdSnPCummulativeDefaultRateRepository = eclPdSnPCummulativeDefaultRateRepository;

            _backgroundJobManager = backgroundJobManager;
            _eclSharedAppService = eclSharedAppService;
            _loanbookExporter = loanbookExporter;
            _paymentScheduleExporter = paymentScheduleExporter;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
        }

        public async Task<PagedResultDto<GetRetailEclForViewDto>> GetAll(GetAllRetailEclsInput input)
        {
            var statusFilter = (EclStatusEnum)input.StatusFilter;

            var filteredRetailEcls = _retailEclRepository.GetAll()
                        .Include(e => e.ClosedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
                        .WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
                        .WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
                        .WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
                        .WhereIf(input.IsApprovedFilter > -1, e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredRetailEcls = filteredRetailEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEcls = from o in pagedAndFilteredRetailEcls
                             join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             select new GetRetailEclForViewDto()
                             {
                                 RetailEcl = new RetailEclDto
                                 {
                                     ReportingDate = o.ReportingDate,
                                     ClosedDate = o.ClosedDate,
                                     IsApproved = o.IsApproved,
                                     Status = o.Status,
                                     Id = o.Id
                                 },
                                 UserName = s1 == null ? "" : s1.Name.ToString()
                             };

            var totalCount = await filteredRetailEcls.CountAsync();

            return new PagedResultDto<GetRetailEclForViewDto>(
                totalCount,
                await retailEcls.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        public async Task<GetRetailEclForEditOutput> GetRetailEclForEdit(EntityDto<Guid> input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl) };

            if (output.EclDto.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.EclDto.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        public async Task<GetEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditEclDto>(retailEcl) };
            if (retailEcl.CreatorUserId != null)
            {
                var _creatorUser = await _lookup_userRepository.FirstOrDefaultAsync((long)retailEcl.CreatorUserId);
                output.CreatedByUserName = _creatorUser.FullName.ToString();
            }

            if (retailEcl.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((long)retailEcl.OrganizationUnitId);
                output.Country = ou.DisplayName;
            }

            if (output.EclDto.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.EclDto.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.FullName.ToString();
            }

            output.FrameworkAssumption = await GetFrameworkAssumption(input.Id);
            output.EadInputAssumptions = await GetEadInputAssumption(input.Id);
            output.LgdInputAssumptions = await GetLgdInputAssumption(input.Id);
            output.PdInputAssumption = await GetPdInputAssumption(input.Id);
            output.PdInputAssumptionMacroeconomicInput = await GetPdMacroInputAssumption(input.Id);
            output.PdInputAssumptionMacroeconomicProjections = await GetPdMacroProjectAssumption(input.Id);
            output.PdInputAssumptionNonInternalModels = await GetPdNonInternalModelAssumption(input.Id);
            output.PdInputAssumptionNplIndex = await GetPdNplAssumption(input.Id);
            output.PdInputSnPCummulativeDefaultRate = await GetPdSnpAssumption(input.Id);

            return output;
        }

        protected virtual async Task<List<AssumptionDto>> GetFrameworkAssumption(Guid eclId)
        {
            var assumptions = _eclAssumptionRepository.GetAll().Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new AssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.AssumptionGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();
        }
        protected virtual async Task<List<EadInputAssumptionDto>> GetEadInputAssumption(Guid eclId)
        {
            var assumptions = _eclEadInputAssumptionRepository.GetAll().Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new EadInputAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.EadGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<LgdAssumptionDto>> GetLgdInputAssumption(Guid eclId)
        {
            var assumptions = _eclLgdAssumptionRepository.GetAll().Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new LgdAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.LgdGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<PdInputAssumptionDto>> GetPdInputAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionRepository.GetAll().Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.PdGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<PdInputAssumptionMacroeconomicInputDto>> GetPdMacroInputAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionMacroeconomicInputsRepository.GetAll()
                                                              .Include(x => x.MacroeconomicVariable)
                                                              .Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputAssumptionMacroeconomicInputDto()
                                                              {
                                                                  AssumptionGroup = x.MacroeconomicVariableId,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  MacroeconomicVariable = x.MacroeconomicVariable == null ? "" : x.MacroeconomicVariable.Name,
                                                                  Value = x.Value,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();


        }
        protected virtual async Task<List<PdInputAssumptionMacroeconomicProjectionDto>> GetPdMacroProjectAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionMacroeconomicProjectionRepository.GetAll()
                                                              .Include(x => x.MacroeconomicVariable)
                                                              .Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputAssumptionMacroeconomicProjectionDto()
                                                              {
                                                                  AssumptionGroup = x.MacroeconomicVariableId,
                                                                  Key = x.Key,
                                                                  Date = x.Date,
                                                                  InputName = x.MacroeconomicVariable != null ? x.MacroeconomicVariable.Name : "",
                                                                  BestValue = x.BestValue,
                                                                  OptimisticValue = x.OptimisticValue,
                                                                  DownturnValue = x.DownturnValue,
                                                                  IsComputed = x.IsComputed,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<PdInputAssumptionNonInternalModelDto>> GetPdNonInternalModelAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionNonInternalModelRepository.GetAll()
                                                              .Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputAssumptionNonInternalModelDto()
                                                              {
                                                                  Key = x.Key,
                                                                  PdGroup = x.PdGroup,
                                                                  Month = x.Month,
                                                                  MarginalDefaultRate = x.MarginalDefaultRate,
                                                                  CummulativeSurvival = x.CummulativeSurvival,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();
        }
        protected virtual async Task<List<PdInputAssumptionNplIndexDto>> GetPdNplAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionNplIndexRepository.GetAll()
                                                              .Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputAssumptionNplIndexDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Date = x.Date,
                                                                  Actual = x.Actual,
                                                                  Standardised = x.Standardised,
                                                                  EtiNplSeries = x.EtiNplSeries,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<PdInputSnPCummulativeDefaultRateDto>> GetPdSnpAssumption(Guid eclId)
        {
            var assumptions = _eclPdSnPCummulativeDefaultRateRepository.GetAll().Where(x => x.RetailEclId == eclId)
                                                              .Select(x => new PdInputSnPCummulativeDefaultRateDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Rating = x.Rating,
                                                                  Years = x.Years,
                                                                  Value = x.Value,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  Id = x.Id,
                                                                  Status = x.Status,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  IsComputed = x.IsComputed
                                                              });

            return await assumptions.ToListAsync();
        }

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

        public async Task<Guid> CreateEclAndAssumption()
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                long ouId = userSubsidiaries[0].Id;
                var affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == ouId);

                if (affiliateAssumption != null)
                {
                    await ValidateForCreation(ouId);

                    Guid eclId = await CreateAndGetId(ouId);

                    await SaveFrameworkAssumption(ouId, eclId);
                    await SaveEadInputAssumption(ouId, eclId);
                    await SaveLgdInputAssumption(ouId, eclId);
                    await SavePdInputAssumption(ouId, eclId);
                    await SavePdMacroInputAssumption(ouId, eclId);
                    await SavePdMacroProjectAssumption(ouId, eclId);
                    await SavePdNonInternalModelAssumption(ouId, eclId);
                    await SavePdNplAssumption(ouId, eclId);
                    await SavePdSnpAssumption(ouId, eclId);

                    return eclId;
                }
                else
                {
                    throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
                }
            }
            else
            {
                throw new UserFriendlyException(L("UserDoesNotBelongToAnyAffiliateError"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Create)]
        protected virtual async Task Create(CreateOrEditEclDto input)
        {
            var retailEcl = ObjectMapper.Map<RetailEcl>(input);

            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                retailEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
            {
                retailEcl.TenantId = (int?)AbpSession.TenantId;
            }


            await _retailEclRepository.InsertAsync(retailEcl);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        protected virtual async Task Update(CreateOrEditEclDto input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEcl);
        }

        protected virtual async Task<Guid> CreateAndGetId(long ouId)
        {
            var affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == ouId);

            if (affiliateAssumption != null)
            {

                Guid id = await _retailEclRepository.InsertAndGetIdAsync(new RetailEcl()
                {
                    ReportingDate = affiliateAssumption.LastRetailReportingDate,
                    OrganizationUnitId = affiliateAssumption.OrganizationUnitId,
                    Status = EclStatusEnum.Draft
                });
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SaveFrameworkAssumption(long ouId, Guid eclId)
        {
            List<AssumptionDto> assumptions = await _eclSharedAppService.GetAffiliateFrameworkAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclAssumptionRepository.InsertAsync(new RetailEclAssumption()
                    {
                        RetailEclId = eclId,
                        AssumptionGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        protected virtual async Task SaveEadInputAssumption(long ouId, Guid eclId)
        {
            List<EadInputAssumptionDto> assumptions = await _eclSharedAppService.GetAffiliateEadAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclEadInputAssumptionRepository.InsertAsync(new RetailEclEadInputAssumption()
                    {
                        RetailEclId = eclId,
                        EadGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = assumption.OrganizationUnitId
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        protected virtual async Task SaveLgdInputAssumption(long ouId, Guid eclId)
        {
            List<LgdAssumptionDto> assumptions = await _eclSharedAppService.GetAffiliateLgdAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclLgdAssumptionRepository.InsertAsync(new RetailEclLgdAssumption()
                    {
                        RetailEclId = eclId,
                        LgdGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdInputAssumption(long ouId, Guid eclId)
        {
            List<PdInputAssumptionDto> assumptions = await _eclSharedAppService.GetAffiliatePdAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionRepository.InsertAsync(new RetailEclPdAssumption()
                    {
                        RetailEclId = eclId,
                        PdGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        Status = assumption.Status,
                        OrganizationUnitId = assumption.OrganizationUnitId
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdMacroInputAssumption(long ouId, Guid eclId)
        {
            List<PdInputAssumptionMacroeconomicInputDto> assumptions = await _eclSharedAppService.GetAffiliatePdMacroeconomicInputAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionMacroeconomicInputsRepository.InsertAsync(new RetailEclPdAssumptionMacroeconomicInput()
                    {
                        RetailEclId = eclId,
                        MacroeconomicVariableId = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdMacroProjectAssumption(long ouId, Guid eclId)
        {
            List<PdInputAssumptionMacroeconomicProjectionDto> assumptions = await _eclSharedAppService.GetAffiliatePdMacroeconomicProjectionAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionMacroeconomicProjectionRepository.InsertAsync(new RetailEclPdAssumptionMacroeconomicProjection()
                    {
                        RetailEclId = eclId,
                        MacroeconomicVariableId = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Date = assumption.Date,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
                        IsComputed = assumption.IsComputed,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status,
                        RequiresGroupApproval = assumption.RequiresGroupApproval
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdNonInternalModelAssumption(long ouId, Guid eclId)
        {
            List<PdInputAssumptionNonInternalModelDto> assumptions = await _eclSharedAppService.GetAffiliatePdNonInternalModelAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionNonInternalModelRepository.InsertAsync(new RetailEclPdAssumptionNonInteralModel()
                    {
                        RetailEclId = eclId,
                        PdGroup = assumption.PdGroup,
                        Key = assumption.Key,
                        Month = assumption.Month,
                        MarginalDefaultRate = assumption.MarginalDefaultRate,
                        CummulativeSurvival = assumption.CummulativeSurvival,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdNplAssumption(long ouId, Guid eclId)
        {
            List<PdInputAssumptionNplIndexDto> assumptions = await _eclSharedAppService.GetAffiliatePdNplIndexAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionNplIndexRepository.InsertAsync(new RetailEclPdAssumptionNplIndex()
                    {
                        RetailEclId = eclId,
                        Date = assumption.Date,
                        Key = assumption.Key,
                        Actual = assumption.Actual,
                        Standardised = assumption.Standardised,
                        EtiNplSeries = assumption.EtiNplSeries,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdSnpAssumption(long ouId, Guid eclId)
        {
            List<PdInputSnPCummulativeDefaultRateDto> assumptions = await _eclSharedAppService.GetAffiliatePdSnpCummulativeAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdSnPCummulativeDefaultRateRepository.InsertAsync(new RetailEclPdSnPCummulativeDefaultRate()
                    {
                        RetailEclId = eclId,
                        Rating = assumption.Rating,
                        Key = assumption.Key,
                        Years = assumption.Years,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = assumption.OrganizationUnitId,
                        IsComputed = assumption.IsComputed
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }


        public virtual async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var ecl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.Id);
                ecl.Status = EclStatusEnum.Submitted;
                ObjectMapper.Map(ecl, ecl);
                await SendSubmittedEmail(input.Id);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditEclApprovalDto input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.EclId);

            await _retailApprovalsRepository.InsertAsync(new RetailEclApproval
            {
                RetailEclId = input.EclId,
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
                var eclApprovals = await _retailApprovalsRepository.GetAllListAsync(x => x.RetailEclId == input.EclId && x.Status == GeneralStatusEnum.Approved);
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

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclRepository.DeleteAsync(input.Id);
        }

        public async Task RunEcl(EntityDto<Guid> input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);
            if (ecl.Status == EclStatusEnum.Approved)
            {
                ecl.Status = EclStatusEnum.Approved;
                await _retailEclRepository.UpdateAsync(ecl);
            }
            else
            {
                throw new UserFriendlyException(L("EclMustBeApprovedBeforeRunning"));
            }
        }

        public async Task RunPostEcl(EntityDto<Guid> input)
        {
            var validation = await ValidateForPostRun(input.Id);
            if (validation.Status)
            {
                var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);
                ecl.Status = EclStatusEnum.QueuePostOverride;
                await _retailEclRepository.UpdateAsync(ecl);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public async Task GenerateReport(EntityDto<Guid> input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed || ecl.Status == EclStatusEnum.Closed)
            {
                await _backgroundJobManager.EnqueueAsync<GenerateEclReportJob, GenerateReportJobArgs>(new GenerateReportJobArgs()
                {
                    eclId = input.Id,
                    eclType = EclType.Retail,
                    userIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("GenerateReportErrorEclNotRun"));
            }
        }

        public async Task CloseEcl(EntityDto<Guid> input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed)
            {
                await _backgroundJobManager.EnqueueAsync<CloseEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Retail,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("CloseEcltErrorEclNotRun"));
            }
        }

        public async Task ReopenEcl(EntityDto<Guid> input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.Closed)
            {
                await _backgroundJobManager.EnqueueAsync<ReopenEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Retail,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("ReopenEcltErrorEclNotRun"));
            }
        }

        public async Task<FileDto> ExportLoanBookToExcel(EntityDto<Guid> input)
        {
            var items = await _loanbookRepository.GetAll().Where(x => x.RetailEclUploadId == input.Id)
                                                         .Select(x => ObjectMapper.Map<EclDataLoanBookDto>(x))
                                                         .ToListAsync();

            return _loanbookExporter.ExportToFile(items);
        }

        public async Task<FileDto> ExportPaymentScheduleToExcel(EntityDto<Guid> input)
        {
            var items = await _paymentScheduleRepository.GetAll().Where(x => x.RetailEclUploadId == input.Id)
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
        }

        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(Guid eclId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _retailUploadRepository.GetAllListAsync(x => x.RetailEclId == eclId);
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
            var overrides = await _retailOverridesRepository.GetAllListAsync(x => x.RetailEclId == eclId);
            if (overrides.Count > 0)
            {
                var submitted = overrides.Any(x => x.Status == GeneralStatusEnum.Submitted || x.Status == GeneralStatusEnum.AwaitngAdditionApproval);
                output.Status = !submitted;
                output.Message = submitted == true ? L("PostRunErrorYetToReviewSubmittedOverrides") : "";
            }
            else
            {
                output.Status = false;
                output.Message = L("NoOverrideRecordFoundForEcl");
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
                    int frameworkId = (int)FrameworkEnum.Retail;
                    var baseUrl = _appConfiguration["App:ClientRootAddress"];
                    var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                    var type = "Retail ECL";
                    var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
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
                    int frameworkId = (int)FrameworkEnum.Retail;
                    var baseUrl = _appConfiguration["App:ClientRootAddress"];
                    var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                    var type = "Retail ECL";
                    var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
                    var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                    await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Retail;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Retail ECL";
            var ecl = _retailEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}