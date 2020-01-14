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

namespace TestDemo.Investment
{
    [AbpAuthorize(AppPermissions.Pages_InvestmentEcls)]
    public class InvestmentEclsAppService : TestDemoAppServiceBase, IInvestmentEclsAppService
    {
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;

        private readonly IInvestmentEclEadInputAssumptionsAppService _invsecEclEadInputAssumptionsAppService;
        private readonly IInvestmentEclLgdInputAssumptionsAppService _invsecEclLgdAssumptionsAppService;
        private readonly IInvestmentEclPdInputAssumptionsAppService _invsecEclPdAssumptionsAppService;
        private readonly IInvestmentPdInputMacroEconomicAssumptionsAppService _invsecPdAssumptionMacroAsusmptionAppService;
        private readonly IInvestmentEclPdFitchDefaultRatesAppService _invsecEclPdAssumptionFitchRatingAppService;
        private readonly IInvestmentEclUploadsAppService _invsecEclUploadsAppService;

        private readonly IInvestmentEclApprovalsAppService _invsecEclApprovalsAppService;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IEclSharedAppService _eclSharedAppService;


        public InvestmentEclsAppService(IRepository<InvestmentEcl, Guid> investmentEclRepository, 
                                        IRepository<User, long> lookup_userRepository,
                                        IRepository<OrganizationUnit, long> organizationUnitRepository,
                                        IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
                                        IInvestmentEclEadInputAssumptionsAppService invsecEclEadInputAssumptionsAppService,
                                        IInvestmentEclLgdInputAssumptionsAppService invsecEclLgdAssumptionsAppService,
                                        IInvestmentEclPdInputAssumptionsAppService invsecEclPdAssumptionsAppService,
                                        IInvestmentPdInputMacroEconomicAssumptionsAppService invsecPdAssumptionMacroAssumptionAppService,
                                        IInvestmentEclPdFitchDefaultRatesAppService invsecEclPdAssumptionFitchRatingAppService,
                                        IInvestmentEclUploadsAppService invsecEclUploadsAppService,
                                        IInvestmentEclApprovalsAppService invsecEclApprovalsAppService,
                                        IBackgroundJobManager backgroundJobManager,
                                        IEclSharedAppService eclSharedAppService)
        {
            _investmentEclRepository = investmentEclRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;

            _invsecEclEadInputAssumptionsAppService = invsecEclEadInputAssumptionsAppService;
            _invsecEclLgdAssumptionsAppService = invsecEclLgdAssumptionsAppService;
            _invsecEclPdAssumptionsAppService = invsecEclPdAssumptionsAppService;
            _invsecEclPdAssumptionFitchRatingAppService = invsecEclPdAssumptionFitchRatingAppService;
            _invsecPdAssumptionMacroAsusmptionAppService = invsecPdAssumptionMacroAssumptionAppService;
            _invsecEclUploadsAppService = invsecEclUploadsAppService;

            _invsecEclApprovalsAppService = invsecEclApprovalsAppService;
            _backgroundJobManager = backgroundJobManager;
            _eclSharedAppService = eclSharedAppService;
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

        [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Edit)]
        public async Task<GetInvestmentEclForEditOutput> GetEclDetailsForEdit(EntityDto<Guid> input)
        {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvestmentEclForEditOutput { EclDto = ObjectMapper.Map<CreateOrEditInvestmentEclDto>(investmentEcl) };

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

            output.EadInputAssumptions = await _invsecEclEadInputAssumptionsAppService.GetListForEclView(input);
            output.LgdInputAssumptions = await _invsecEclLgdAssumptionsAppService.GetListForEclView(input);
            output.PdInputAssumption = await _invsecEclPdAssumptionsAppService.GetListForEclView(input);
            output.PdInputAssumptionMacroeconomic = await _invsecPdAssumptionMacroAsusmptionAppService.GetListForEclView(input);
            output.PdInputFitchCummulativeDefaultRate = await _invsecEclPdAssumptionFitchRatingAppService.GetListForEclView(input);

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvestmentEclDto input)
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

        [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Create)]
        protected virtual async Task Create(CreateOrEditInvestmentEclDto input)
        {
            var investmentEcl = ObjectMapper.Map<InvestmentEcl>(input);



            await _investmentEclRepository.InsertAsync(investmentEcl);
        }

        [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Edit)]
        protected virtual async Task Update(CreateOrEditInvestmentEclDto input)
        {
            var investmentEcl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, investmentEcl);
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

                Guid id = await _investmentEclRepository.InsertAndGetIdAsync(new InvestmentEcl()
                {
                    ReportingDate = affiliateAssumption.LastRetailReportingDate,
                    OrganizationUnitId = affiliateAssumption.OrganizationUnitId,
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
                    await _invsecEclEadInputAssumptionsAppService.CreateOrEdit(new CreateOrEditInvestmentEclEadInputAssumptionDto()
                    {
                        InvestmentEclId = eclId,
                        EadGroup = assumption.AssumptionGroup,
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
                    await _invsecEclLgdAssumptionsAppService.CreateOrEdit(new CreateOrEditInvestmentEclLgdInputAssumptionDto()
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
                    await _invsecEclPdAssumptionsAppService.CreateOrEdit(new CreateOrEditInvestmentEclPdInputAssumptionDto()
                    {
                        InvestmentEclId = eclId,
                        PdGroup = assumption.AssumptionGroup,
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
                    await _invsecPdAssumptionMacroAsusmptionAppService.CreateOrEdit(new CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto()
                    {
                        InvestmentEclId = eclId,
                        Key = assumption.Key,
                        Month = assumption.Month,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
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
                    await _invsecEclPdAssumptionFitchRatingAppService.CreateOrEdit(new CreateOrEditInvestmentEclPdFitchDefaultRateDto()
                    {
                        InvestmentEclId = eclId,
                        Key = assumption.Key,
                        Rating = assumption.Rating,
                        Year = assumption.Years,
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



        [AbpAuthorize(AppPermissions.Pages_InvestmentEcls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclRepository.DeleteAsync(input.Id);
        }

        public virtual async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var ecl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.Id);
                ecl.Status = EclStatusEnum.Submitted;
                ObjectMapper.Map(ecl, ecl);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditInvestmentEclApprovalDto input)
        {
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync((Guid)input.InvestmentEclId);
            ecl.Status = input.Status == GeneralStatusEnum.Approved ? EclStatusEnum.Approved : EclStatusEnum.Draft;
            ObjectMapper.Map(ecl, ecl);
            await _invsecEclApprovalsAppService.CreateOrEdit(input);
        }


        public async Task RunEcl(EntityDto<Guid> input)
        {
            var ecl = await _investmentEclRepository.FirstOrDefaultAsync(input.Id);
            ecl.Status = EclStatusEnum.Running;
            await _investmentEclRepository.UpdateAsync(ecl);
            //await _backgroundJobManager.EnqueueAsync<RunRetailPdJob, RetailPdJobArgs>(new RetailPdJobArgs { RetailEclId = input.Id });
        }

        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(Guid eclId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _invsecEclUploadsAppService.GetEclUploads(new EntityDto<Guid> { Id = eclId });
            if (uploads.Count > 0)
            {
                var notCompleted = uploads.Any(x => x.EclUpload.Status != GeneralStatusEnum.Completed);
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

        [AbpAuthorize(AppPermissions.Pages_InvestmentEcls)]
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
    }
}