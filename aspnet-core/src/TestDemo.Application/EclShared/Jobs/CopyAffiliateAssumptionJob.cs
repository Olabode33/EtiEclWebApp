using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Threading;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.Calibration;
using TestDemo.CalibrationInput;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Importing.Calibration;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Notifications;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class CopyAffiliateAssumptionJob : BackgroundJob<CopyAffiliateAssumptionJobArgs>, ITransientDependency
    {

        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptions;
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
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroVariableRepository;

        private readonly IAppNotifier _appNotifier;
        private readonly ILocalizationSource _localizationSource;

        public CopyAffiliateAssumptionJob(
            IRepository<Assumption, Guid> frameworkAssumptionRepository,
            IRepository<EadInputAssumption, Guid> eadAssumptionRepository,
            IRepository<LgdInputAssumption, Guid> lgdAssumptionRepository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptions,
            IRepository<PdInputAssumption, Guid> pdAssumptionRepository,
            IRepository<PdInputAssumptionMacroeconomicInput, Guid> pdAssumptionMacroEcoInputRepository,
            IRepository<PdInputAssumptionMacroeconomicProjection, Guid> pdAssumptionMacroecoProjectionRepository,
            IRepository<PdInputAssumptionNonInternalModel, Guid> pdAssumptionNonInternalModelRepository,
            IRepository<PdInputAssumptionNplIndex, Guid> pdAssumptionNplIndexRepository,
            IRepository<PdInputAssumptionSnPCummulativeDefaultRate, Guid> pdSnPCummulativeAssumptionRepository,
            IRepository<InvSecFitchCummulativeDefaultRate, Guid> invsecFitchCummulativeAssumptionRepository,
            IRepository<InvSecMacroEconomicAssumption, Guid> invsecMacroEcoAssumptionRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IObjectMapper objectMapper)
        {
            _affiliateAssumptions = affiliateAssumptions;
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
            _affiliateMacroVariableRepository = affiliateMacroVariableRepository;

            _appNotifier = appNotifier;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        [UnitOfWork]
        public override void Execute(CopyAffiliateAssumptionJobArgs args)
        {
            var input = new CopyAffiliateDto
            {
                FromAffiliateId = args.FromAffiliateId,
                ToAffiliateId = args.ToAffiliateId
            };
            CopyAffiliateAssumptions(args);
            SendCopyCompleteNotification(args);
        }

        private void CopyAffiliateAssumptions(CopyAffiliateAssumptionJobArgs input)
        {
             CopyReportingDate(input);
                CurrentUnitOfWork.SaveChanges();
             CopyFrameworkAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyEadInputAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyLgdInputAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdInputAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdMacroInputAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdMacroProjectAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdNonInternalModelAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdNplAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdSnpAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyInvesPdMacroAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyPdFitchAssumption(input);
            CurrentUnitOfWork.SaveChanges();
            CopyMacroVariables(input);
            CurrentUnitOfWork.SaveChanges();
        }

        private void CopyReportingDate(CopyAffiliateAssumptionJobArgs input)
        {
            var from =  _affiliateAssumptions.FirstOrDefault(e => e.OrganizationUnitId == input.FromAffiliateId);
            var to =  _affiliateAssumptions.FirstOrDefault(e => e.OrganizationUnitId == input.ToAffiliateId);

            if (to == null)
            {
                 _affiliateAssumptions.Insert(new AffiliateAssumption
                {
                    OrganizationUnitId = input.ToAffiliateId,
                    LastAssumptionUpdate = from.LastAssumptionUpdate,
                    LastObeReportingDate = from.LastObeReportingDate,
                    LastRetailReportingDate = from.LastRetailReportingDate,
                    LastWholesaleReportingDate = from.LastWholesaleReportingDate,
                    LastSecuritiesReportingDate = from.LastSecuritiesReportingDate,
                    Status = from.Status
                });
            }
            else
            {
                to.LastAssumptionUpdate = from.LastAssumptionUpdate;
                to.LastObeReportingDate = from.LastObeReportingDate;
                to.LastRetailReportingDate = from.LastRetailReportingDate;
                to.LastWholesaleReportingDate = from.LastWholesaleReportingDate;
                to.LastSecuritiesReportingDate = from.LastSecuritiesReportingDate;
                to.Status = from.Status;

                 _affiliateAssumptions.Update(to);
            }
        }
        private void CopyFrameworkAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _frameworkAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _frameworkAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _frameworkAssumptionRepository.Insert(new Assumption()
                    {
                        AssumptionGroup = assumption.AssumptionGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                        LastModificationTime = DateTime.Now,
                        LastModifierUserId = input.User.UserId,
                        CreatorUserId = input.User.UserId
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private void CopyEadInputAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _eadAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _eadAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _eadAssumptionRepository.Insert(new EadInputAssumption()
                    {
                        EadGroup = assumption.EadGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        Datatype = assumption.Datatype,
                        IsComputed = assumption.IsComputed,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private void CopyLgdInputAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _lgdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _lgdAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _lgdAssumptionRepository.Insert(new LgdInputAssumption()
                    {
                        LgdGroup = assumption.LgdGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdInputAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdAssumptionRepository.Insert(new PdInputAssumption()
                    {
                        PdGroup = assumption.PdGroup,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        DataType = assumption.DataType,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdMacroInputAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionMacroEcoInputRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionMacroEcoInputRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdAssumptionMacroEcoInputRepository.Insert(new PdInputAssumptionMacroeconomicInput()
                    {
                        MacroeconomicVariableId = assumption.MacroeconomicVariableId,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Value = assumption.Value,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdMacroProjectAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionMacroecoProjectionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionMacroecoProjectionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdAssumptionMacroecoProjectionRepository.Insert(new PdInputAssumptionMacroeconomicProjection()
                    {
                        MacroeconomicVariableId = assumption.MacroeconomicVariableId,
                        Key = assumption.Key,
                        InputName = assumption.InputName,
                        Date = assumption.Date,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
                        IsComputed = assumption.IsComputed,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdNonInternalModelAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionNonInternalModelRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionNonInternalModelRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdAssumptionNonInternalModelRepository.Insert(new PdInputAssumptionNonInternalModel()
                    {
                        PdGroup = assumption.PdGroup,
                        Key = assumption.Key,
                        Month = assumption.Month,
                        MarginalDefaultRate = assumption.MarginalDefaultRate,
                        CummulativeSurvival = assumption.CummulativeSurvival,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdNplAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionNplIndexRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionNplIndexRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdAssumptionNplIndexRepository.Insert(new PdInputAssumptionNplIndex()
                    {
                        Date = assumption.Date,
                        Key = assumption.Key,
                        Actual = assumption.Actual,
                        Standardised = assumption.Standardised,
                        EtiNplSeries = assumption.EtiNplSeries,
                        IsComputed = assumption.IsComputed,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        OrganizationUnitId = input.ToAffiliateId,
                        Status = assumption.Status,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdSnpAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdSnPCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdSnPCummulativeAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _pdSnPCummulativeAssumptionRepository.Insert(new PdInputAssumptionSnPCummulativeDefaultRate()
                    {
                        Rating = assumption.Rating,
                        Key = assumption.Key,
                        Years = assumption.Years,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId,
                        Framework = assumption.Framework,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyInvesPdMacroAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _invsecMacroEcoAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _invsecMacroEcoAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _invsecMacroEcoAssumptionRepository.Insert(new InvSecMacroEconomicAssumption()
                    {
                        Key = assumption.Key,
                        Month = assumption.Month,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private void CopyPdFitchAssumption(CopyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _invsecFitchCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _invsecFitchCummulativeAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                     _invsecFitchCummulativeAssumptionRepository.Insert(new InvSecFitchCummulativeDefaultRate()
                    {
                        Key = assumption.Key,
                        Rating = assumption.Rating,
                        Year = assumption.Year,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId,
                         LastModificationTime = DateTime.Now,
                         LastModifierUserId = input.User.UserId,
                         CreatorUserId = input.User.UserId
                     });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private void CopyMacroVariables(CopyAffiliateAssumptionJobArgs input)
        {
            var from =  _affiliateMacroVariableRepository.GetAll()
                                    .Where(x => x.AffiliateId == input.FromAffiliateId)
                                    .ToList();

            if (from.Count > 0)
            {
                 _affiliateMacroVariableRepository.Delete(x => x.AffiliateId == input.ToAffiliateId);

                foreach (var item in from)
                {
                     _affiliateMacroVariableRepository.Insert(new AffiliateMacroEconomicVariableOffset()
                    {
                        AffiliateId = input.ToAffiliateId,
                        BackwardOffset = item.BackwardOffset,
                        MacroeconomicVariableId = item.MacroeconomicVariableId
                     });
                }
            }
        }


        private void SendCopyCompleteNotification(CopyAffiliateAssumptionJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("CopyAffiliateProcessCompleted"),
                Abp.Notifications.NotificationSeverity.Success);
        }
    }
}
