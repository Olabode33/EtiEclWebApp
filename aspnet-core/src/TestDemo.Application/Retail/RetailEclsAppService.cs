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

namespace TestDemo.Retail
{
    [AbpAuthorize(AppPermissions.Pages_RetailEcls)]
    public class RetailEclsAppService : TestDemoAppServiceBase, IRetailEclsAppService
    {
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;

        private readonly IRetailEclAssumptionsAppService _retailEclAssumptionAppService;
        private readonly IRetailEclEadInputAssumptionsAppService _retailEclEadInputAssumptionsAppService;
        private readonly IRetailEclLgdAssumptionsAppService _retailEclLgdAssumptionsAppService;
        private readonly IRetailEclPdAssumptionsAppService _retailEclPdAssumptionsAppService;
        private readonly IRetailEclPdAssumptionMacroeconomicInputsAppService _retailPdAssumptionMacroInputAppService;
        private readonly IRetailEclPdAssumptionMacroeconomicProjectionsAppService _retailEclPdAssumptionMacroProjectionAppService;
        private readonly IRetailEclPdAssumptionNonInteralModelsAppService _retailEclPdAssumptionNonInteralAppService;
        private readonly IRetailEclPdAssumptionNplIndexesAppService _retailEclPdAssumptionNplAppService;
        private readonly IRetailEclPdSnPCummulativeDefaultRatesAppService _retailPdAssumptionSnpAppService;
        private readonly IRetailEclUploadsAppService _retailEclUploadsAppService;

        private readonly IRetailEclApprovalsAppService _retailEclApprovalsAppService;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;

        public RetailEclsAppService(IRepository<RetailEcl, Guid> retailEclRepository,
                                    IRepository<User, long> lookup_userRepository,
                                    IRepository<OrganizationUnit, long> organizationUnitRepository,
                                    IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
                                    IRetailEclAssumptionsAppService retailEclAssumptionAppService,
                                    IRetailEclEadInputAssumptionsAppService retailEclEadInputAssumptionsAppService,
                                    IRetailEclLgdAssumptionsAppService retailEclLgdAssumptionsAppService,
                                    IRetailEclApprovalsAppService retailEclApprovalsAppService,
                                    IRetailEclPdAssumptionsAppService retailEclPdAssumptionsAppService,
                                    IRetailEclPdAssumptionMacroeconomicInputsAppService retailPdAssumptionMacroInputAppService,
                                    IRetailEclPdAssumptionMacroeconomicProjectionsAppService retailEclPdAssumptionMacroProjectionAppService,
                                    IRetailEclPdAssumptionNonInteralModelsAppService retailEclPdAssumptionNonInteralAppService,
                                    IRetailEclPdAssumptionNplIndexesAppService retailEclPdAssumptionNplAppService,
                                    IRetailEclPdSnPCummulativeDefaultRatesAppService retailPdAssumptionSnpAppService,
                                    IRetailEclUploadsAppService retailEclUploadsAppService,
                                    IBackgroundJobManager backgroundJobManager,
                                    IEclSharedAppService eclSharedAppService
                                    )
        {
            _retailEclRepository = retailEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;

            _retailEclAssumptionAppService = retailEclAssumptionAppService;
            _retailEclEadInputAssumptionsAppService = retailEclEadInputAssumptionsAppService;
            _retailEclLgdAssumptionsAppService = retailEclLgdAssumptionsAppService;
            _retailEclPdAssumptionsAppService = retailEclPdAssumptionsAppService;
            _retailPdAssumptionMacroInputAppService = retailPdAssumptionMacroInputAppService;
            _retailEclPdAssumptionMacroProjectionAppService = retailEclPdAssumptionMacroProjectionAppService;
            _retailEclPdAssumptionNonInteralAppService = retailEclPdAssumptionNonInteralAppService;
            _retailEclPdAssumptionNplAppService = retailEclPdAssumptionNplAppService;
            _retailPdAssumptionSnpAppService = retailPdAssumptionSnpAppService;
            _retailEclApprovalsAppService = retailEclApprovalsAppService;
            _retailEclUploadsAppService = retailEclUploadsAppService;

            _backgroundJobManager = backgroundJobManager;
            _eclSharedAppService = eclSharedAppService;
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

            var output = new GetRetailEclForEditOutput { RetailEcl = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl) };

            if (output.RetailEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEcl.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        public async Task<GetRetailEclForEditOutput> GetRetailEclDetailsForEdit(EntityDto<Guid> input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclForEditOutput { RetailEcl = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl) };
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

            if (output.RetailEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEcl.ClosedByUserId);
                output.ClosedByUserName = _lookupUser.FullName.ToString();
            }

            output.FrameworkAssumption = await _retailEclAssumptionAppService.GetListForEclView(input);
            output.EadInputAssumptions = await _retailEclEadInputAssumptionsAppService.GetListForEclView(input);
            output.LgdInputAssumptions = await _retailEclLgdAssumptionsAppService.GetListForEclView(input);
            output.PdInputAssumption = await _retailEclPdAssumptionsAppService.GetListForEclView(input);
            output.PdInputAssumptionMacroeconomicInput = await _retailPdAssumptionMacroInputAppService.GetListForEclView(input);
            output.PdInputAssumptionMacroeconomicProjections = await _retailEclPdAssumptionMacroProjectionAppService.GetListForEclView(input);
            output.PdInputAssumptionNonInternalModels = await _retailEclPdAssumptionNonInteralAppService.GetListForEclView(input);
            output.PdInputAssumptionNplIndex = await _retailEclPdAssumptionNplAppService.GetListForEclView(input);
            output.PdInputSnPCummulativeDefaultRate = await _retailPdAssumptionSnpAppService.GetListForEclView(input);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclDto input)
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
        protected virtual async Task Create(CreateOrEditRetailEclDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
        protected virtual async Task Update(CreateOrEditRetailEclDto input)
        {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEcl);
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
                    await _retailEclAssumptionAppService.CreateOrEdit(new CreateOrEditRetailEclAssumptionDto()
                    {
                        RetailEclId = eclId,
                        AssumptionGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        Datatype = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
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
                    await _retailEclEadInputAssumptionsAppService.CreateOrEdit(new CreateOrEditRetailEclEadInputAssumptionDto()
                    {
                        RetailEclId = eclId,
                        EadGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        Datatype = assumption.DataType,
                        IsComputed = assumption.IsComputed,
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
                Framework = FrameworkEnum.Retail
            });

            if (assumptions.Count > 0)
            {
                foreach (var assumption in assumptions)
                {
                    await _retailEclLgdAssumptionsAppService.CreateOrEdit(new CreateOrEditRetailEclLgdAssumptionDto()
                    {
                        RetailEclId = eclId,
                        LgdGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval
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
                    await _retailEclPdAssumptionsAppService.CreateOrEdit(new CreateOrEditRetailEclPdAssumptionDto()
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
                    await _retailPdAssumptionMacroInputAppService.CreateOrEdit(new CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto()
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
                    await _retailEclPdAssumptionMacroProjectionAppService.CreateOrEdit(new CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto()
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
                        Status = assumption.Status
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
                    await _retailEclPdAssumptionNonInteralAppService.CreateOrEdit(new CreateOrEditRetailEclPdAssumptionNonInteralModelDto()
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
                    await _retailEclPdAssumptionNplAppService.CreateOrEdit(new CreateOrEditRetailEclPdAssumptionNplIndexDto()
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
                    await _retailPdAssumptionSnpAppService.CreateOrEdit(new CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto()
                    {
                        RetailEclId = eclId,
                        Rating = assumption.Rating,
                        Key = assumption.Key,
                        Years = assumption.Years,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval
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
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditRetailEclApprovalDto input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.RetailEclId);
            ecl.Status = input.Status == GeneralStatusEnum.Approved ? EclStatusEnum.Approved : EclStatusEnum.Draft;
            ObjectMapper.Map(ecl, ecl);
            await _retailEclApprovalsAppService.CreateOrEdit(input);
        }

        [AbpAuthorize(AppPermissions.Pages_RetailEcls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclRepository.DeleteAsync(input.Id);
        }

        public async Task RunEcl(EntityDto<Guid> input)
        {
            var ecl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);
            ecl.Status = EclStatusEnum.Running;
            await _retailEclRepository.UpdateAsync(ecl);
            await _backgroundJobManager.EnqueueAsync<RunRetailPdJob, RetailPdJobArgs>(new RetailPdJobArgs { RetailEclId = input.Id });
        }

        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(Guid eclId)
        {
            ValidationMessageDto output = new ValidationMessageDto();
            var uploads = await _retailEclUploadsAppService.GetEclUploads(new EntityDto<Guid> {Id = eclId });
            if (uploads.Count > 0)
            {
                var notCompleted = uploads.Any(x => x.RetailEclUpload.Status != GeneralStatusEnum.Completed);
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
    }
}