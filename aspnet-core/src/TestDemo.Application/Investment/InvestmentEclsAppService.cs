using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Investment.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.InvestmentAssumption;
using Abp.BackgroundJobs;
using Abp.Organizations;
using Abp.UI;
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.Investment.Dtos.GetAllForLookupTableInput;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.InvestmentInputs;
using TestDemo.InvestmentComputation;
using TestDemo.EclLibrary.Investment;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.Reports.Jobs;
using TestDemo.Reports;
using Abp.Runtime.Session;
using Abp.Configuration;
using TestDemo.EclConfig;
using TestDemo.EclLibrary.Jobs;
using TestDemo.EclInterfaces;
using TestDemo.Dto.Ecls;
using TestDemo.Dto.Approvals;
using TestDemo.Common.Exporting;
using TestDemo.Dto.Inputs;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;

namespace TestDemo.Investment
{
    [AbpAuthorize(AppPermissions.Pages_EclView)]
    public class InvestmentEclsAppService : TestDemoAppServiceBase, IEclsAppService
    {
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;
        private readonly IRepository<AssumptionApproval, Guid> _assumptionsApprovalRepository;
        private readonly IRepository<InvestmentEclApproval, Guid> _investmentApprovalsRepository;
        private readonly IRepository<InvestmentEclOverride, Guid> _investmentOverridesRepository;
        private readonly IRepository<InvestmentEclUpload, Guid> _investmentUploadRepository;
        private readonly IRepository<InvestmentAssetBook, Guid> _dataUploadRepository;

        private readonly IRepository<InvestmentEclEadInputAssumption, Guid> _eclEadInputAssumptionRepository;
        private readonly IRepository<InvestmentEclLgdInputAssumption, Guid> _eclLgdAssumptionRepository;
        private readonly IRepository<InvestmentEclPdInputAssumption, Guid> _eclPdAssumptionRepository;
        private readonly IRepository<InvestmentPdInputMacroEconomicAssumption, Guid> _eclPdAssumptionMacroeconomicInputsRepository;
        private readonly IRepository<InvestmentEclPdFitchDefaultRate, Guid> _eclPdSnPCummulativeDefaultRateRepository;

        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;
        private readonly IEclCustomRepository _investmentEclCustomRepository;
        private readonly IEclDataAssetBookExporter _dataExporter;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;


        public InvestmentEclsAppService(IRepository<InvestmentEcl, Guid> investmentEclRepository, 
                                        IRepository<User, long> lookup_userRepository,
                                        IRepository<OrganizationUnit, long> organizationUnitRepository,
                                        IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
                                        IRepository<AssumptionApproval, Guid> assumptionsApprovalRepository,
                                        IRepository<InvestmentEclApproval, Guid> investmentApprovalsRepository,
                                        IRepository<InvestmentEclOverride, Guid> investmentOverridesRepository,
                                        IRepository<InvestmentEclUpload, Guid> investmentUploadRepository,
                                        IRepository<InvestmentAssetBook, Guid> dataUploadRepository,
                                        IRepository<InvestmentEclEadInputAssumption, Guid> eclEadInputAssumptionRepository,
                                        IRepository<InvestmentEclLgdInputAssumption, Guid> eclLgdAssumptionRepository,
                                        IRepository<InvestmentEclPdInputAssumption, Guid> eclPdAssumptionRepository,
                                        IRepository<InvestmentPdInputMacroEconomicAssumption, Guid> eclPdAssumptionMacroeconomicInputsRepository,
                                        IRepository<InvestmentEclPdFitchDefaultRate, Guid> eclPdSnPCummulativeDefaultRateRepository,
                                        IBackgroundJobManager backgroundJobManager,
                                        IEclCustomRepository investmentEclCustomRepository,
                                        IEclDataAssetBookExporter dataExporter,
                                        IEclEngineEmailer emailer,
                                        IHostingEnvironment env,
                                        IEclSharedAppService eclSharedAppService)
        {
            _investmentEclRepository = investmentEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;
            _assumptionsApprovalRepository = assumptionsApprovalRepository;
            _investmentApprovalsRepository = investmentApprovalsRepository;
            _investmentOverridesRepository = investmentOverridesRepository;
            _investmentUploadRepository = investmentUploadRepository;
            _dataUploadRepository = dataUploadRepository;

            _eclEadInputAssumptionRepository = eclEadInputAssumptionRepository;
            _eclLgdAssumptionRepository = eclLgdAssumptionRepository;
            _eclPdAssumptionRepository = eclPdAssumptionRepository;
            _eclPdAssumptionMacroeconomicInputsRepository = eclPdAssumptionMacroeconomicInputsRepository;
            _eclPdSnPCummulativeDefaultRateRepository = eclPdSnPCummulativeDefaultRateRepository;

            _backgroundJobManager = backgroundJobManager;
            _investmentEclCustomRepository = investmentEclCustomRepository;
            _eclSharedAppService = eclSharedAppService;
            _dataExporter = dataExporter;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
        }

        public async Task<PagedResultDto<GetInvestmentEclForViewDto>> GetAll(GetAllInvestmentEclsInput input)
        {
            var statusFilter = (EclStatusEnum)input.StatusFilter;

            var filteredInvestmentEcls = _investmentEclRepository.GetAll()
                        .Include(e => e.ClosedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name == input.UserNameFilter);

            var pagedAndFilteredInvestmentEcls = filteredInvestmentEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentEcls = from o in pagedAndFilteredInvestmentEcls
                                 join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                                 from s1 in j1.DefaultIfEmpty()

                                 select new GetInvestmentEclForViewDto()
                                 {
                                     InvestmentEcl = new InvestmentEclDto
                                     {
                                         ReportingDate = o.ReportingDate,
                                         ClosedDate = o.ClosedDate,
                                         IsApproved = o.IsApproved,
                                         Status = o.Status,
                                         Id = o.Id
                                     },
                                     UserName = s1 == null ? "" : s1.Name.ToString()
                                 };

            var totalCount = await filteredInvestmentEcls.CountAsync();

            return new PagedResultDto<GetInvestmentEclForViewDto>(
                totalCount,
                await investmentEcls.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_EclView)]
        public async Task<GetEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input)
        {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditEclDto>(investmentEcl) };

            if (investmentEcl.CreatorUserId != null)
            {
                var _creatorUser = await _lookup_userRepository.FirstOrDefaultAsync((long)investmentEcl.CreatorUserId);
                output.CreatedByUserName = _creatorUser.FullName.ToString();
            }

            if (investmentEcl.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((long)investmentEcl.OrganizationUnitId);
                output.Country = ou.DisplayName;
            }

            if (investmentEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)investmentEcl.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.FullName.ToString();
            }

            output.EadInputAssumptions = await GetEadInputAssumption(input.Id);
            output.LgdInputAssumptions = await GetLgdInputAssumption(input.Id);
            output.PdInputAssumption = await GetPdInputAssumption(input.Id);
            output.PdInputAssumptionMacroeconomic = await GetPdMacroInputAssumption(input.Id);
            output.PdInputFitchCummulativeDefaultRate = await GetPdFitchAssumption(input.Id);

            return output;
        }

        protected virtual async Task<List<EadInputAssumptionDto>> GetEadInputAssumption(Guid eclId)
        {
            var assumptions = _eclEadInputAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == eclId)
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
                                                                  //Status = x.st,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<LgdAssumptionDto>> GetLgdInputAssumption(Guid eclId)
        {
            var assumptions = _eclLgdAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == eclId)
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
                                                                  //Status = x.s,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<PdInputAssumptionDto>> GetPdInputAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionRepository.GetAll().Where(x => x.InvestmentEclId == eclId)
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
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }
        protected virtual async Task<List<InvSecMacroEconomicAssumptionDto>> GetPdMacroInputAssumption(Guid eclId)
        {
            var assumptions = _eclPdAssumptionMacroeconomicInputsRepository.GetAll().Where(x => x.InvestmentEclId == eclId)
                                                              .Select(x => new InvSecMacroEconomicAssumptionDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Month = x.Month,
                                                                  BestValue = x.BestValue,
                                                                  OptimisticValue = x.OptimisticValue,
                                                                  DownturnValue = x.DownturnValue,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  Status = x.Status,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();


        }
        protected virtual async Task<List<InvSecFitchCummulativeDefaultRateDto>> GetPdFitchAssumption(Guid eclId)
        {
            var assumptions = _eclPdSnPCummulativeDefaultRateRepository.GetAll().Where(x => x.InvestmentEclId == eclId)
                                                              .Select(x => new InvSecFitchCummulativeDefaultRateDto()
                                                              {
                                                                  Key = x.Key,
                                                                  Rating = x.Rating,
                                                                  Years = x.Year,
                                                                  Value = x.Value,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  Status = x.Status,
                                                                  Id = x.Id
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

        protected virtual async Task Create(CreateOrEditEclDto input)
        {
            var investmentEcl = ObjectMapper.Map<InvestmentEcl>(input);



            await _investmentEclRepository.InsertAsync(investmentEcl);
        }

        protected virtual async Task Update(CreateOrEditEclDto input)
        {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            investmentEcl.ReportingDate = input.ReportingDate;
            await _investmentEclRepository.UpdateAsync(investmentEcl);
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
                if (input.OrganizationUnitId != null )
                {
                    ouId = (long)input.OrganizationUnitId;
                    affiliateAssumption = await _affiliateAssumptionRepository.FirstOrDefaultAsync(x => x.OrganizationUnitId == input.OrganizationUnitId);
                }
                throw new UserFriendlyException(L("UserDoesNotBelongToAnyAffiliateError"));
            }

            

            if (affiliateAssumption != null)
            {
                await ValidateForCreation(ouId);

                Guid eclId = await CreateAndGetId(ouId, input.ReportingDate);

                await SaveEadInputAssumption(ouId, eclId);
                await SaveLgdInputAssumption(ouId, eclId);
                await SavePdInputAssumption(ouId, eclId);
                await SavePdMacroAssumption(ouId, eclId);
                await SavePdFitchAssumption(ouId, eclId);

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

                Guid id = await _investmentEclRepository.InsertAndGetIdAsync(new InvestmentEcl()
                {
                    ReportingDate = reportDate,
                    OrganizationUnitId = ouId,
                    Status = EclStatusEnum.Draft,
                });
                return id;
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
                Framework = FrameworkEnum.Investments
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclEadInputAssumptionRepository.InsertAsync(new InvestmentEclEadInputAssumption()
                    {
                        InvestmentEclId = eclId,
                        EadGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        RequiresGroupApproval = assumption.RequiresGroupApproval
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
                Framework = FrameworkEnum.Investments
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclLgdAssumptionRepository.InsertAsync(new InvestmentEclLgdInputAssumption()
                    {
                        InvestmentEclId = eclId,
                        LgdGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit
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
                Framework = FrameworkEnum.Investments
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionRepository.InsertAsync(new InvestmentEclPdInputAssumption()
                    {
                        InvestmentEclId = eclId,
                        PdGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdMacroAssumption(long ouId, Guid eclId)
        {
            List<InvSecMacroEconomicAssumptionDto> assumptions = await _eclSharedAppService.GetAffiliateInvSecPdMacroEcoAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Investments
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdAssumptionMacroeconomicInputsRepository.InsertAsync(new InvestmentPdInputMacroEconomicAssumption()
                    {
                        InvestmentEclId = eclId,
                        Key = assumption.Key,
                        Month = assumption.Month,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        protected virtual async Task SavePdFitchAssumption(long ouId, Guid eclId)
        {
            List<InvSecFitchCummulativeDefaultRateDto> assumptions = await _eclSharedAppService.GetAffiliateInvSecPdFitchCummulativeAssumption(new GetAffiliateAssumptionInputDto()
            {
                AffiliateOuId = ouId,
                Framework = FrameworkEnum.Investments
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _eclPdSnPCummulativeDefaultRateRepository.InsertAsync(new InvestmentEclPdFitchDefaultRate()
                    {
                        InvestmentEclId = eclId,
                        Key = assumption.Key,
                        Rating = assumption.Rating,
                        Year = assumption.Years,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            //TODO: Update to job
            await _investmentEclRepository.DeleteAsync(input.Id);
            await _eclEadInputAssumptionRepository.HardDeleteAsync(e => e.InvestmentEclId == input.Id);
            await _eclLgdAssumptionRepository.HardDeleteAsync(e => e.InvestmentEclId == input.Id);
            await _eclPdAssumptionRepository.HardDeleteAsync(e => e.InvestmentEclId == input.Id);
            await _eclPdAssumptionMacroeconomicInputsRepository.HardDeleteAsync(e => e.InvestmentEclId == input.Id);
            await _eclPdSnPCummulativeDefaultRateRepository.HardDeleteAsync(e => e.InvestmentEclId == input.Id);
            await DeleteAssetBook(input.Id);
            
        }

        private async Task DeleteAssetBook(Guid eclId)
        {
            var summary = await _investmentUploadRepository.FirstOrDefaultAsync(x => x.InvestmentEclId == eclId);
            await _dataUploadRepository.HardDeleteAsync(x => x.InvestmentEclUploadId == summary.Id);
            await _investmentUploadRepository.DeleteAsync(x => x.InvestmentEclId == eclId);
        }


        [AbpAuthorize(AppPermissions.Pages_EclView_Submit)]
        public virtual async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var ecl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
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
        public virtual async Task ApproveReject(CreateOrEditEclApprovalDto input)
        {
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.EclId);

            await _investmentApprovalsRepository.InsertAsync(new InvestmentEclApproval
            {
                InvestmentEclId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _investmentApprovalsRepository.GetAllListAsync(x => x.InvestmentEclId == input.EclId && x.Status == GeneralStatusEnum.Approved);
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
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            if (ecl.Status == EclStatusEnum.Approved)
            {
                await _backgroundJobManager.EnqueueAsync<RunInvestmentEclJob, RunEclJobArgs>(new RunEclJobArgs
                {
                    EclId = input.Id,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
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
                await _backgroundJobManager.EnqueueAsync<RunInvestmentPostEclJob, RunEclJobArgs>(new RunEclJobArgs { 
                    EclId = input.Id,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
            } else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public async Task GenerateReport(EntityDto<Guid> input)
        {
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed || ecl.Status == EclStatusEnum.Closed)
            {
                await _backgroundJobManager.EnqueueAsync<GenerateEclReportJob, GenerateReportJobArgs>(new GenerateReportJobArgs()
                {
                    eclId = input.Id,
                    eclType = EclType.Investment,
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
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.PreOverrideComplete || ecl.Status == EclStatusEnum.PostOverrideComplete || ecl.Status == EclStatusEnum.Completed)
            {
                //Call archive ecl procedure
                await _backgroundJobManager.EnqueueAsync<CloseEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Investment,
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
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);

            if (ecl.Status == EclStatusEnum.Closed)
            {
                //Call archive ecl procedure
                await _backgroundJobManager.EnqueueAsync<ReopenEclJob, RunEclJobArgs>(new RunEclJobArgs()
                {
                    EclId = input.Id,
                    EclType = EclType.Investment,
                    UserIdentifier = AbpSession.ToUserIdentifier()
                });
            }
            else
            {
                throw new UserFriendlyException(L("ReopenEcltErrorEclNotRun"));
            }
        }

        public async Task<FileDto> ExportAssetBookToExcel(EntityDto<Guid> input)
        {
            //var uploadSummary = _investmentUploadRepository.FirstOrDefaultAsync(x => x.InvestmentEclId == input.Id);
            var items = await _dataUploadRepository.GetAll().Where(x => x.InvestmentEclUploadId == input.Id)
                                                         .Select(x => ObjectMapper.Map<EclDataAssetBookDto>(x))
                                                         .ToListAsync();

            return _dataExporter.ExportToFile(items);
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

            var uploads = await _investmentUploadRepository.GetAllListAsync(x => x.InvestmentEclId == eclId);
            if (uploads.Count > 0)
            {
                var notCompleted = uploads.Any(x => x.Status != GeneralStatusEnum.Completed);
                output.Status = !notCompleted;
                output.Message = notCompleted == true ? L("UploadInProgressError") : "";
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
            var overrides = await _investmentOverridesRepository.GetAllListAsync(x => x.EclId == eclId);
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

        public async Task<PagedResultDto<InvestmentEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentEclUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new InvestmentEclUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<InvestmentEclUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task SendSubmittedEmail(Guid eclId)
        {
            var users = await UserManager.GetUsersInRoleAsync("Affiliate Reviewer");
            if (users.Count > 0)
            {
                var ecl = _investmentEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Investments;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Investment ECL";
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
                var ecl = _investmentEclRepository.FirstOrDefault((Guid)eclId);
                var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
                foreach (var user in users)
                {
                    if (await UserManager.IsInOrganizationUnitAsync(user.Id, ecl.OrganizationUnitId))
                    {
                        int frameworkId = (int)FrameworkEnum.Investments;
                        var baseUrl = _appConfiguration["App:ClientRootAddress"];
                        var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
                        var type = "Investment ECL";
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, type, ou.DisplayName, link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(Guid eclId)
        {
            int frameworkId = (int)FrameworkEnum.Investments;
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + eclId;
            var type = "Investment ECL";
            var ecl = _investmentEclRepository.FirstOrDefault((Guid)eclId);
            var user = _lookup_userRepository.FirstOrDefault(ecl.CreatorUserId == null ? (long)AbpSession.UserId : (long)ecl.CreatorUserId);
            var ou = _organizationUnitRepository.FirstOrDefault(ecl.OrganizationUnitId);
            await _emailer.SendEmailApprovedAsync(user, type, ou.DisplayName, link);
        }
    }
}