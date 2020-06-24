using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.Threading;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.Authorization.Users;
using TestDemo.Calibration;
using TestDemo.CalibrationInput;
using TestDemo.Configuration;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
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
    public class ApplyAssumptionToAffiliateJob : BackgroundJob<ApplyAffiliateAssumptionJobArgs>, ITransientDependency
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
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;

        public ApplyAssumptionToAffiliateJob(
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
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
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
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
        }

        [UnitOfWork]
        public override void Execute(ApplyAffiliateAssumptionJobArgs args)
        {
            CopyAffiliateAssumptions(args);
            SendCopyCompleteNotification(args);
            SendEmailAlert(args);
        }

        [UnitOfWork]
        private void CopyAffiliateAssumptions(ApplyAffiliateAssumptionJobArgs input)
        {
            switch (input.Type)
            {
                case AssumptionTypeEnum.General:

                    CopyFrameworkAssumption(input);
                    //CurrentUnitOfWork.SaveChanges();
                    break;
                case AssumptionTypeEnum.EadInputAssumption:

                    CopyEadInputAssumption(input);
                    //CurrentUnitOfWork.SaveChanges();
                    break;
                case AssumptionTypeEnum.LgdInputAssumption:
                    CopyLgdInputAssumption(input);
                    //CurrentUnitOfWork.SaveChanges();
                    break;
                case AssumptionTypeEnum.PdInputAssumption:
                    CopyPdInputAssumption(input);
                    //CurrentUnitOfWork.SaveChanges();

                    if (input.Framework != FrameworkEnum.Investments)
                    {
                        CopyPdMacroInputAssumption(input);
                        //CurrentUnitOfWork.SaveChanges();
                        CopyPdMacroProjectAssumption(input);
                        //CurrentUnitOfWork.SaveChanges();
                        CopyPdNonInternalModelAssumption(input);
                        //CurrentUnitOfWork.SaveChanges();
                        CopyPdNplAssumption(input);
                        //CurrentUnitOfWork.SaveChanges();
                        CopyPdSnpAssumption(input);
                    }

                    if (input.Framework == FrameworkEnum.Investments)
                    {
                        //CurrentUnitOfWork.SaveChanges();
                        CopyInvesPdMacroAssumption(input);
                        //CurrentUnitOfWork.SaveChanges();
                        CopyPdFitchAssumption(input);
                    }

                    break;
                default:
                    break;
            }
            //CopyReportingDate(input);
            //CurrentUnitOfWork.SaveChanges();
            
            //CurrentUnitOfWork.SaveChanges();
            //CopyMacroVariables(input);
            //CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void CopyReportingDate(ApplyAffiliateAssumptionJobArgs input)
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
                CurrentUnitOfWork.SaveChanges();
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
                CurrentUnitOfWork.SaveChanges();
            }
        }
        [UnitOfWork]
        private void CopyFrameworkAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _frameworkAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _frameworkAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        [UnitOfWork]
        private void CopyEadInputAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _eadAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _eadAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }

        [UnitOfWork]
        private void CopyLgdInputAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _lgdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _lgdAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdInputAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdMacroInputAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionMacroEcoInputRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionMacroEcoInputRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdMacroProjectAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionMacroecoProjectionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionMacroecoProjectionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdNonInternalModelAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionNonInternalModelRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionNonInternalModelRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdNplAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdAssumptionNplIndexRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdAssumptionNplIndexRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdSnpAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _pdSnPCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId && x.Framework == input.Framework)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _pdSnPCummulativeAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId && x.Framework == input.Framework);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyInvesPdMacroAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _invsecMacroEcoAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _invsecMacroEcoAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        [UnitOfWork]
        private void CopyPdFitchAssumption(ApplyAffiliateAssumptionJobArgs input)
        {
            var assumptions =  _invsecFitchCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToList();

            if (assumptions.Count > 0)
            {
                 _invsecFitchCummulativeAssumptionRepository.HardDelete(x => x.OrganizationUnitId == input.ToAffiliateId);
                CurrentUnitOfWork.SaveChanges();

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
                CurrentUnitOfWork.SaveChanges();
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }


        private void SendCopyCompleteNotification(ApplyAffiliateAssumptionJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("CopyAffiliateProcessCompleted"),
                Abp.Notifications.NotificationSeverity.Success);
        }

        private void SendEmailAlert(ApplyAffiliateAssumptionJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/assumption/affiliates/view/" + args.ToAffiliateId;
            var from = _ouRepository.FirstOrDefault(args.FromAffiliateId);
            var to = _ouRepository.FirstOrDefault(args.ToAffiliateId);
            var type = _localizationSource.GetString(args.Framework.ToString()) + " " + _localizationSource.GetString(args.Type.ToString());
            _emailer.SendEmailAssumptionAppiedAsync(user, type, from.DisplayName, to.DisplayName, link);
        }
    }
}
