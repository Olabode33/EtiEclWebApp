using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Wholesale.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.WholesaleInputs;
using System.Data;
using TestDemo.Utils;
using TestDemo.WholesaleComputation;
using Abp.Organizations;
using TestDemo.WholesaleAssumption;
using Abp.BackgroundJobs;
using TestDemo.EclInterfaces;
using TestDemo.Dto.Ecls;
using TestDemo.Dto.Approvals;
using Abp.UI;
using TestDemo.EclShared.Dtos;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.EclConfig;
using Abp.Configuration;
using TestDemo.Reports.Jobs;
using TestDemo.Reports;
using Abp.Runtime.Session;
using TestDemo.EclLibrary.Jobs;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.Common.Exporting;
using TestDemo.Dto.Inputs;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;

namespace TestDemo.Wholesale
{
    [AbpAuthorize(AppPermissions.Pages_EclView)]
    public class WholesaleEclsAppService : TestDemoAppServiceBase, IEclsAppService
    {
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;
        private readonly IRepository<AssumptionApproval, Guid> _assumptionsApprovalRepository;
        private readonly IRepository<WholesaleEclApproval, Guid> _wholesaleApprovalsRepository;
        private readonly IRepository<WholesaleEclOverride, Guid> _wholesaleOverridesRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _loanbookRepository;
        private readonly IRepository<WholesaleEclDataPaymentSchedule, Guid> _paymentScheduleRepository;

        private readonly IRepository<WholesaleEclAssumption, Guid> _eclAssumptionRepository;
        private readonly IRepository<WholesaleEclEadInputAssumption, Guid> _eclEadInputAssumptionRepository;
        private readonly IRepository<WholesaleEclLgdAssumption, Guid> _eclLgdAssumptionRepository;
        private readonly IRepository<WholesaleEclPdAssumption, Guid> _eclPdAssumptionRepository;
        private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> _eclPdAssumptionMacroeconomicInputsRepository;
        private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> _eclPdAssumptionMacroeconomicProjectionRepository;
        private readonly IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> _eclPdAssumptionNonInternalModelRepository;
        private readonly IRepository<WholesaleEclPdAssumptionNplIndex, Guid> _eclPdAssumptionNplIndexRepository;
        private readonly IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> _eclPdSnPCummulativeDefaultRateRepository;

        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;
        private readonly IEclLoanbookExporter _loanbookExporter;
        private readonly IEclDataPaymentScheduleExporter _paymentScheduleExporter;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;


        public WholesaleEclsAppService(
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository, 
            IRepository<User, long> lookup_userRepository, 
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
            IRepository<AssumptionApproval, Guid> assumptionsApprovalRepository,
            IRepository<WholesaleEclApproval, Guid> wholesaleApprovalsRepository,
            IRepository<WholesaleEclOverride, Guid> wholesaleOverridesRepository,
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadRepository,
            IRepository<WholesaleEclDataLoanBook, Guid> loanbookRepository,
            IRepository<WholesaleEclDataPaymentSchedule, Guid> paymentScheduleRepository,
            IRepository<WholesaleEclAssumption, Guid> eclAssumptionRepository,
            IRepository<WholesaleEclEadInputAssumption, Guid> eclEadInputAssumptionRepository,
            IRepository<WholesaleEclLgdAssumption, Guid> eclLgdAssumptionRepository,
            IRepository<WholesaleEclPdAssumption, Guid> eclPdAssumptionRepository,
            IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> eclPdAssumptionMacroeconomicInputsRepository,
            IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> eclPdAssumptionMacroeconomicProjectionRepository,
            IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> eclPdAssumptionNonInternalModelRepository,
            IRepository<WholesaleEclPdAssumptionNplIndex, Guid> eclPdAssumptionNplIndexRepository,
            IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> eclPdSnPCummulativeDefaultRateRepository,
            IBackgroundJobManager backgroundJobManager,
            IEclSharedAppService eclSharedAppService,
            IEclLoanbookExporter loanbookExporter,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IEclDataPaymentScheduleExporter paymentScheduleExporter
            )
        {
            _wholesaleEclRepository = wholesaleEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;
            _assumptionsApprovalRepository = assumptionsApprovalRepository;
            _wholesaleApprovalsRepository = wholesaleApprovalsRepository;
            _wholesaleOverridesRepository = wholesaleOverridesRepository;
            _wholesaleUploadRepository = wholesaleUploadRepository;
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

        public async Task<PagedResultDto<GetWholesaleEclForViewDto>> GetAll(GetAllWholesaleEclsInput input)
        {
            var statusFilter = (EclStatusEnum)input.StatusFilter;

            var filteredWholesaleEcls = _wholesaleEclRepository.GetAll()
                        .Include(e => e.ClosedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
                        .WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
                        .WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
                        .WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
                        .WhereIf(input.IsApprovedFilter > -1, e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredWholesaleEcls = filteredWholesaleEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var wholesaleEcls = from o in pagedAndFilteredWholesaleEcls
                                join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                                from s1 in j1.DefaultIfEmpty()

                                select new GetWholesaleEclForViewDto()
                                {
                                    WholesaleEcl = new WholesaleEclDto
                                    {
                                        ReportingDate = o.ReportingDate,
                                        ClosedDate = o.ClosedDate,
                                        IsApproved = o.IsApproved,
                                        Status = o.Status,
                                        Id = o.Id
                                    },
                                    UserName = s1 == null ? "" : s1.Name.ToString()
                                };

            var totalCount = await filteredWholesaleEcls.CountAsync();

            return new PagedResultDto<GetWholesaleEclForViewDto>(
                totalCount,
                await wholesaleEcls.ToListAsync()
            );
        }

        public async Task<GetWholesaleEclForEditOutput> GetWholesaleEclForEdit(EntityDto<Guid> input)
        {
            var wholesaleEcl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWholesaleEclForEditOutput { WholesaleEcl = ObjectMapper.Map<CreateOrEditWholesaleEclDto>(wholesaleEcl) };

            if (output.WholesaleEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEcl.ClosedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            return output;
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

        protected virtual async Task Create(CreateOrEditEclDto input)
        {
            var wholesaleEcl = ObjectMapper.Map<WholesaleEcl>(input);

            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                wholesaleEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
            {
                wholesaleEcl.TenantId = (int?)AbpSession.TenantId;
            }

            await _wholesaleEclRepository.InsertAsync(wholesaleEcl);
        }

        protected virtual async Task Update(CreateOrEditEclDto input)
        {
            var wholesaleEcl = await _wholesaleEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            wholesaleEcl.ReportingDate = input.ReportingDate;
            await _wholesaleEclRepository.UpdateAsync(wholesaleEcl);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclRepository.DeleteAsync(input.Id);
        }


        public async Task<GetEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input)
        {
            var wholesaleEcl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditEclDto>(wholesaleEcl) };
            if (wholesaleEcl.CreatorUserId != null)
            {
                var _creatorUser = await _lookup_userRepository.FirstOrDefaultAsync((long)wholesaleEcl.CreatorUserId);
                output.CreatedByUserName = _creatorUser.FullName.ToString();
            }

            if (wholesaleEcl.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((long)wholesaleEcl.OrganizationUnitId);
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
            var assumptions = _eclAssumptionRepository.GetAll().Where(x => x.WholesaleEclId == eclId)
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
            var assumptions = _eclEadInputAssumptionRepository.GetAll().Where(x => x.WholesaleEclId == eclId)
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
            var assumptions = _eclLgdAssumptionRepository.GetAll().Where(x => x.WholesaleEclId == eclId)
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
            var assumptions = _eclPdAssumptionRepository.GetAll().Where(x => x.WholesaleEclId == eclId)
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
                                                              .Where(x => x.WholesaleEclId == eclId)
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
                                                              .Where(x => x.WholesaleEclId == eclId)
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
                                                              .Where(x => x.WholesaleEclId == eclId)
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
                                                              .Where(x => x.WholesaleEclId == eclId)
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
            var assumptions = _eclPdSnPCummulativeDefaultRateRepository.GetAll().Where(x => x.WholesaleEclId == eclId)
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


        [AbpAuthorize(AppPermissions.Pages_Workspace_CreateEcl)]
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

        protected virtual async Task<Guid> CreateAndGetId(long ouId)
        {
            var affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == ouId);

            if (affiliateAssumption != null)
            {

                Guid id = await _wholesaleEclRepository.InsertAndGetIdAsync(new WholesaleEcl()
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclAssumptionRepository.InsertAsync(new WholesaleEclAssumption()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclEadInputAssumptionRepository.InsertAsync(new WholesaleEclEadInputAssumption()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclLgdAssumptionRepository.InsertAsync(new WholesaleEclLgdAssumption()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionRepository.InsertAsync(new WholesaleEclPdAssumption()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionMacroeconomicInputsRepository.InsertAsync(new WholesaleEclPdAssumptionMacroeconomicInput()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionMacroeconomicProjectionRepository.InsertAsync(new WholesaleEclPdAssumptionMacroeconomicProjection()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionNonInternalModelRepository.InsertAsync(new WholesaleEclPdAssumptionNonInternalModel()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionNplIndexRepository.InsertAsync(new WholesaleEclPdAssumptionNplIndex()
                    {
                        WholesaleEclId = eclId,
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
                Framework = FrameworkEnum.Wholesale
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdSnPCummulativeDefaultRateRepository.InsertAsync(new WholesaleEclPdSnPCummulativeDefaultRate()
                    {
                        WholesaleEclId = eclId,
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


        [AbpAuthorize(AppPermissions.Pages_EclView_Submit)]
        public async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync((Guid)input.Id);
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
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync((Guid)input.EclId);

            await _wholesaleApprovalsRepository.InsertAsync(new WholesaleEclApproval
            {
                WholesaleEclId = input.EclId,
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
                var eclApprovals = await _wholesaleApprovalsRepository.GetAllListAsync(x => x.WholesaleEclId == input.EclId && x.Status == GeneralStatusEnum.Approved);
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
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);
            if (ecl.Status == EclStatusEnum.Approved)
            {
                ecl.Status = EclStatusEnum.Approved;
                await _wholesaleEclRepository.UpdateAsync(ecl);
            }
            else if (ecl.Status == EclStatusEnum.Running)
            {

            }
            else
            {
                throw new UserFriendlyException(L("EclMustBeApprovedBeforeRunning"));
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Run)]
        public async Task RunPostEcl(EntityDto<Guid> input)
        {
            var validation = await ValidateForPostRun(input.Id);
            if (validation.Status)
            {
                var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);
                ecl.Status = EclStatusEnum.QueuePostOverride;
                await _wholesaleEclRepository.UpdateAsync(ecl);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public async Task GenerateReport(EntityDto<Guid> input)
        {
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed || ecl.Status == EclStatusEnum.Closed)
            {
                await _backgroundJobManager.EnqueueAsync<GenerateEclReportJob, GenerateReportJobArgs>(new GenerateReportJobArgs()
                {
                    eclId = input.Id,
                    eclType = EclType.Wholesale,
                    userIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("GenerateReportErrorEclNotRun"));
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Close)]
        public async Task CloseEcl(EntityDto<Guid> input)
        {
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed)
            {
                await _backgroundJobManager.EnqueueAsync<CloseEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Wholesale,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("CloseEcltErrorEclNotRun"));
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Reopen)]
        public async Task ReopenEcl(EntityDto<Guid> input)
        {
            var ecl = await _wholesaleEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.Closed)
            {
                await _backgroundJobManager.EnqueueAsync<ReopenEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Wholesale,
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
            var items = await _loanbookRepository.GetAll().Where(x => x.WholesaleEclUploadId == input.Id)
                                                         .Select(x => ObjectMapper.Map<EclDataLoanBookDto>(x))
                                                         .ToListAsync();

            return _loanbookExporter.ExportToFile(items);
        }

        public async Task<FileDto> ExportPaymentScheduleToExcel(EntityDto<Guid> input)
        {
            var items = await _paymentScheduleRepository.GetAll().Where(x => x.WholesaleEclUploadId == input.Id)
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

            var uploads = await _wholesaleUploadRepository.GetAllListAsync(x => x.WholesaleEclId == eclId);
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
            var overrides = await _wholesaleOverridesRepository.GetAllListAsync(x => x.WholesaleEclDataLoanBookId == eclId);
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
                    var ecl = _wholesaleEclRepository.FirstOrDefault((Guid)eclId);

                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Wholesale;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Wholesale ECL";
                        var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
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
                foreach (var user in users)
                {

                    var ecl = _wholesaleEclRepository.FirstOrDefault((Guid)eclId);
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Wholesale;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Wholesale ECL";
                        var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Wholesale;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Wholesale ECL";
            var ecl = _wholesaleEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}