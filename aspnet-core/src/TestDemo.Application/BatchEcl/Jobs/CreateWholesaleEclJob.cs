using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.Threading;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.EclShared;
using TestDemo.EclShared.Dtos;
using TestDemo.InvestmentComputation;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;
using TestDemo.WholesaleAssumption;

namespace TestDemo.BatchEcls.Job
{
    public class CreateWholesaleEclJob : BackgroundJob<CreateSubEclJobArgs>, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<WholesaleEcl, Guid> _eclRepository;
        private readonly IRepository<RetailEcl, Guid> _eclSub1Repository;
        private readonly IRepository<ObeEcl, Guid> _eclSub2Repository;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionRepository;
        private readonly IRepository<BatchEcl, Guid> _batchEclRepository;

        private readonly IRepository<WholesaleEclAssumption, Guid> _eclAssumptionRepository;
        private readonly IRepository<WholesaleEclEadInputAssumption, Guid> _eclEadInputAssumptionRepository;
        private readonly IRepository<WholesaleEclLgdAssumption, Guid> _eclLgdAssumptionRepository;
        private readonly IRepository<WholesaleEclPdAssumption, Guid> _eclPdAssumptionRepository;
        private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> _eclPdAssumptionMacroeconomicInputsRepository;
        private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> _eclPdAssumptionMacroeconomicProjectionRepository;
        private readonly IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> _eclPdAssumptionNonInternalModelRepository;
        private readonly IRepository<WholesaleEclPdAssumptionNplIndex, Guid> _eclPdAssumptionNplIndexRepository;
        private readonly IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> _eclPdSnPCummulativeDefaultRateRepository;

        private readonly IEclSharedAppService _eclSharedAppService;

        private readonly IEclCustomRepository _customRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public CreateWholesaleEclJob(
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<WholesaleEcl, Guid> eclRepository,
            IRepository<RetailEcl, Guid> eclSub1Repository,
            IRepository<ObeEcl, Guid> eclSub2Repository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
            IRepository<BatchEcl, Guid> batchEclRepository,

            IRepository<WholesaleEclAssumption, Guid> eclAssumptionRepository,
            IRepository<WholesaleEclEadInputAssumption, Guid> eclEadInputAssumptionRepository,
            IRepository<WholesaleEclLgdAssumption, Guid> eclLgdAssumptionRepository,
            IRepository<WholesaleEclPdAssumption, Guid> eclPdAssumptionRepository,
            IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> eclPdAssumptionMacroeconomicInputsRepository,
            IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> eclPdAssumptionMacroeconomicProjectionRepository,
            IRepository<WholesaleEclPdAssumptionNonInternalModel, Guid> eclPdAssumptionNonInternalModelRepository,
            IRepository<WholesaleEclPdAssumptionNplIndex, Guid> eclPdAssumptionNplIndexRepository,
            IRepository<WholesaleEclPdSnPCummulativeDefaultRate, Guid> eclPdSnPCummulativeDefaultRateRepository,

            IEclSharedAppService eclSharedAppService,
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
            IObjectMapper objectMapper)
        {
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _eclRepository = eclRepository;
            _eclSub1Repository = eclSub1Repository;
            _eclSub2Repository = eclSub2Repository;
            _affiliateAssumptionRepository = affiliateAssumptionRepository;
            _batchEclRepository = batchEclRepository;

            _eclAssumptionRepository = eclAssumptionRepository;
            _eclEadInputAssumptionRepository = eclEadInputAssumptionRepository;
            _eclLgdAssumptionRepository = eclLgdAssumptionRepository;
            _eclPdAssumptionRepository = eclPdAssumptionRepository;
            _eclPdAssumptionMacroeconomicInputsRepository = eclPdAssumptionMacroeconomicInputsRepository;
            _eclPdAssumptionMacroeconomicProjectionRepository = eclPdAssumptionMacroeconomicProjectionRepository;
            _eclPdAssumptionNonInternalModelRepository = eclPdAssumptionNonInternalModelRepository;
            _eclPdAssumptionNplIndexRepository = eclPdAssumptionNplIndexRepository;
            _eclPdSnPCummulativeDefaultRateRepository = eclPdSnPCummulativeDefaultRateRepository;

            _eclSharedAppService = eclSharedAppService;
            _objectMapper = objectMapper;
            _customRepository = customRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        public override void Execute(CreateSubEclJobArgs args)
        {
            var batch = _batchEclRepository.FirstOrDefault(args.BatchId);
            Guid eclId = CreateAndGetId(batch.OrganizationUnitId, batch.ReportingDate, batch.Id, args.UserId);

            AsyncHelper.RunSync(() => SaveFrameworkAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SaveEadInputAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SaveLgdInputAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdInputAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdMacroInputAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdMacroProjectAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdNonInternalModelAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdNplAssumption(batch.OrganizationUnitId, eclId));
            AsyncHelper.RunSync(() => SavePdSnpAssumption(batch.OrganizationUnitId, eclId));

            CheckCompletion(batch);
        }

        private void CheckCompletion(BatchEcl batch)
        {
            var eclSub0 = _eclRepository.FirstOrDefault(e => e.BatchId == batch.Id);
            var eclSub1 = _eclSub1Repository.FirstOrDefault(e => e.BatchId == batch.Id);
            var eclSub2 = _eclSub2Repository.FirstOrDefault(e => e.BatchId == batch.Id);

            if (eclSub0 != null && eclSub1 != null && eclSub2 != null)
            {
                batch.Status = EclStatusEnum.Draft;
                _batchEclRepository.Update(batch);
            }
        }


        protected Guid CreateAndGetId(long ouId, DateTime reportDate, Guid batchId, long userId)
        {
            var affiliateAssumption = _affiliateAssumptionRepository.FirstOrDefault(x => x.OrganizationUnitId == ouId);

            if (affiliateAssumption != null)
            {

                Guid id = _eclRepository.InsertAndGetId(new WholesaleEcl()
                {
                    ReportingDate = reportDate,
                    OrganizationUnitId = ouId,
                    BatchId = batchId,
                    Status = EclStatusEnum.Draft,
                    CreationTime = DateTime.Now,
                    CreatorUserId = userId
                });
                affiliateAssumption.LastWholesaleReportingDate = reportDate;
                _affiliateAssumptionRepository.Update(affiliateAssumption);
                CurrentUnitOfWork.SaveChanges();
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

    }
}
