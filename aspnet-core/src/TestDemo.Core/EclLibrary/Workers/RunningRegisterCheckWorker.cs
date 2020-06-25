using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.Calibration;
using TestDemo.Configuration;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.EclLibrary.Jobs;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared;
using TestDemo.EclShared.Emailer;
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;

namespace TestDemo.EclLibrary.Workers
{
    public class RunningRegisterCheckWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private const int CheckPeriodAsMilliseconds = 1 * 5 * 60 * 1000; //5 minutes

        private readonly IRepository<TrackRunningGuidRegister> _guidTrackreRepository;
        private readonly IRepository<TrackRunningIntRegister> _intTrackreRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleRepository;
        private readonly IRepository<RetailEcl, Guid> _retailRepository;
        private readonly IRepository<ObeEcl, Guid> _obeRepository;
        private readonly IRepository<CalibrationEadBehaviouralTerm, Guid> _behaviouralTermRepository;
        private readonly IRepository<CalibrationEadCcfSummary, Guid> _ccfRepository;
        private readonly IRepository<CalibrationLgdHairCut, Guid> _haircutRepository;
        private readonly IRepository<CalibrationLgdRecoveryRate, Guid> _recoveryRateRepository;
        private readonly IRepository<CalibrationPdCrDr, Guid> _pdcrdrRepository;
        private readonly IRepository<MacroAnalysis> _macroAnalysisRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RunningRegisterCheckWorker(
            AbpTimer timer,
            IHostingEnvironment env,
            IRepository<TrackRunningGuidRegister> guidTrackreRepository,
            IRepository<TrackRunningIntRegister> intTrackreRepository,
            IRepository<WholesaleEcl, Guid> wholesaleRepository, 
            IRepository<RetailEcl, Guid> retailRepository, 
            IRepository<ObeEcl, Guid> obeRepository, 
            IRepository<CalibrationEadBehaviouralTerm, Guid> behaviouralTermRepository, 
            IRepository<CalibrationEadCcfSummary, Guid> ccfRepository, 
            IRepository<CalibrationLgdHairCut, Guid> haircutRepository, 
            IRepository<CalibrationLgdRecoveryRate, Guid> recoveryRateRepository, 
            IRepository<CalibrationPdCrDr, Guid> pdcrdrRepository, 
            IRepository<MacroAnalysis> macroAnalysisRepository,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IBackgroundJobManager backgroundJobManager,
            IEclEngineEmailer emailer)
            : base(timer)
        {
            Timer.Period = CheckPeriodAsMilliseconds;

            _guidTrackreRepository = guidTrackreRepository;
            _intTrackreRepository = intTrackreRepository;
            _wholesaleRepository = wholesaleRepository;
            _retailRepository = retailRepository;
            _obeRepository = obeRepository;
            _behaviouralTermRepository = behaviouralTermRepository;
            _ccfRepository = ccfRepository;
            _haircutRepository = haircutRepository;
            _recoveryRateRepository = recoveryRateRepository;
            _pdcrdrRepository = pdcrdrRepository;
            _macroAnalysisRepository = macroAnalysisRepository;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            var allRunningGuid = _guidTrackreRepository.GetAllList();
            var allRunningInt = _intTrackreRepository.GetAllList();

            GetRunningId(allRunningGuid, allRunningInt);
            CheckProgress(allRunningGuid, allRunningInt);
        }

        [UnitOfWork]
        private void GetRunningId(List<TrackRunningGuidRegister> allRunningGuid, List<TrackRunningIntRegister> allRunningInt)
        {
            GetRunningWholesale(allRunningGuid);
            GetRunningkRetail(allRunningGuid);
            GetRunningObe(allRunningGuid);
            GetRunningBehaviourTerm(allRunningGuid);
            GetRunningCcf(allRunningGuid);
            GetRunningHaircut(allRunningGuid);
            GetRunningRecoveryRate(allRunningGuid);
            GetRunningPdCrDr(allRunningGuid);
            GetRunningMacro(allRunningInt);
        }

        [UnitOfWork]
        private void CheckProgress(List<TrackRunningGuidRegister> allRunningGuid, List<TrackRunningIntRegister> allRunningInt)
        {
            CheckProgressWholesale(allRunningGuid);
            CheckProgressRetail(allRunningGuid);
            CheckProgressObe(allRunningGuid);
            CheckProgressBehavourialTerm(allRunningGuid);
            CheckProgressCcfSummary(allRunningGuid);
            CheckProgressHaircut(allRunningGuid);
            CheckProgressRecoveryRate(allRunningGuid);
            CheckProgressPdCrDr(allRunningGuid);
            CheckProgressMacroAnalysis(allRunningInt);
        }

        private void GetRunningWholesale(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.Wholesale;
            var w_running = _wholesaleRepository.GetAll().Where(e => e.Status == EclStatusEnum.Running).Select(e => e.Id).ToList();
            if (w_running.Count > 0)
            {
                foreach (var id in w_running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningkRetail(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.Retail;
            var running = _retailRepository.GetAll().Where(e => e.Status == EclStatusEnum.Running).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningObe(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.Obe;
            var running = _obeRepository.GetAll().Where(e => e.Status == EclStatusEnum.Running).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningBehaviourTerm(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.CalibrateBehaviouralTerm;
            var running = _behaviouralTermRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing ).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningCcf(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.CalibrateCcfSummary;
            var running = _ccfRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningHaircut(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.CalibrateHaircut;
            var running = _haircutRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningRecoveryRate(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.CalibrateRecoveryRate;
            var running = _recoveryRateRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningPdCrDr(List<TrackRunningGuidRegister> allRunningGuid)
        {
            var type = TrackTypeEnum.CalibratePdCrDr;
            var running = _pdcrdrRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningGuid.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _guidTrackreRepository.Insert(new TrackRunningGuidRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }
        private void GetRunningMacro(List<TrackRunningIntRegister> allRunningId)
        {
            var type = TrackTypeEnum.MacroAnalysis;

            var running = _macroAnalysisRepository.GetAll().Where(e => e.Status == CalibrationStatusEnum.Processing).Select(e => e.Id).ToList();
            if (running.Count > 0)
            {
                foreach (var id in running)
                {
                    if (!allRunningId.Any(e => e.RegisterId == id && e.Type == type))
                    {
                        _intTrackreRepository.Insert(new TrackRunningIntRegister
                        {
                            RegisterId = id,
                            Type = type
                        });
                    }
                }
            }
        }

        private void CheckProgressWholesale(List<TrackRunningGuidRegister> allRunningGuid)
        {

            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.Wholesale).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _wholesaleRepository.FirstOrDefault(item.RegisterId);
                    int frameworkId = (int)FrameworkEnum.Wholesale;
                    string link = "/app/main/ecl/view/" + frameworkId.ToString() + "/" + register.Id;
                    switch (register.Status)
                    {
                        case EclStatusEnum.PreOverrideComplete:
                        case EclStatusEnum.PostOverrideComplete:
                        case EclStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, "Wholesale ECL", register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: WholesaleEcl: " + item.RegisterId + " completed, Email sent & tracker deleted.");

                            _backgroundJobManager.Enqueue<UpdateFacilityStageTrackerJob, UpdateFacilityStageTrackerJobArgs>(new UpdateFacilityStageTrackerJobArgs()
                            {
                                EclId = register.Id,
                                EclType = FrameworkEnum.Wholesale,
                                OrganizationUnitId = register.OrganizationUnitId
                            });

                            break;
                        case EclStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, "Wholesale ECL", register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: WholesaleEcl: " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressRetail(List<TrackRunningGuidRegister> allRunningGuid)
        {

            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.Retail).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _retailRepository.FirstOrDefault(item.RegisterId);
                    int frameworkId = (int)FrameworkEnum.Retail;
                    string link = "/app/main/ecl/view/" + frameworkId.ToString() + "/" + register.Id;
                    switch (register.Status)
                    {
                        case EclStatusEnum.PreOverrideComplete:
                        case EclStatusEnum.PostOverrideComplete:
                        case EclStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, "Retail ECL", register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: RetailEcl: " + item.RegisterId + " completed, Email sent & tracker deleted.");

                            _backgroundJobManager.Enqueue<UpdateFacilityStageTrackerJob, UpdateFacilityStageTrackerJobArgs>(new UpdateFacilityStageTrackerJobArgs()
                            {
                                EclId = register.Id,
                                EclType = FrameworkEnum.Retail,
                                OrganizationUnitId = register.OrganizationUnitId
                            });

                            break;
                        case EclStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, "Retail ECL", register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: RetailEcl: " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressObe(List<TrackRunningGuidRegister> allRunningGuid)
        {

            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.Obe).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _obeRepository.FirstOrDefault(item.RegisterId);
                    int frameworkId = (int)FrameworkEnum.OBE;
                    string link = "/app/main/ecl/view/" + frameworkId.ToString() + "/" + register.Id;
                    switch (register.Status)
                    {
                        case EclStatusEnum.PreOverrideComplete:
                        case EclStatusEnum.PostOverrideComplete:
                        case EclStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, "OBE ECL", register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: ObeEcl: " + item.RegisterId + " completed, Email sent & tracker deleted.");

                            _backgroundJobManager.Enqueue<UpdateFacilityStageTrackerJob, UpdateFacilityStageTrackerJobArgs>(new UpdateFacilityStageTrackerJobArgs()
                            {
                                EclId = register.Id,
                                EclType = FrameworkEnum.OBE,
                                OrganizationUnitId = register.OrganizationUnitId
                            });
                            break;
                        case EclStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, "OBE ECL", register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: ObeEcl: " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressBehavourialTerm(List<TrackRunningGuidRegister> allRunningGuid)
        {
            string type = "EAD Behavioural Term";
            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.CalibrateBehaviouralTerm).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _behaviouralTermRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/behavioralTerms/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressCcfSummary(List<TrackRunningGuidRegister> allRunningGuid)
        {
            string type = "EAD CCF Summary";
            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.CalibrateCcfSummary).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _ccfRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/ccfSummary/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressHaircut(List<TrackRunningGuidRegister> allRunningGuid)
        {
            string type = "LGD Haircut";
            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.CalibrateHaircut).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _haircutRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/haircut/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressRecoveryRate(List<TrackRunningGuidRegister> allRunningGuid)
        {
            string type = "LGD Recovery Rate";
            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.CalibrateRecoveryRate).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _recoveryRateRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/recovery/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressPdCrDr(List<TrackRunningGuidRegister> allRunningGuid)
        {
            string type = "PD CR DR";
            var running = allRunningGuid.Where(e => e.Type == TrackTypeEnum.CalibratePdCrDr).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _pdcrdrRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/pdcrdr/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckProgressMacroAnalysis(List<TrackRunningIntRegister> allRunningInt)
        {
            string type = "Macro Analysis";
            var running = allRunningInt.Where(e => e.Type == TrackTypeEnum.MacroAnalysis).ToList();
            if (running.Count > 0)
            {
                foreach (var item in running)
                {
                    var register = _macroAnalysisRepository.FirstOrDefault(item.RegisterId);
                    string link = "/app/main/calibration/macroAnalysis/view/" + register.Id;
                    switch (register.Status)
                    {
                        case CalibrationStatusEnum.Completed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailCompleted((long)register.CreatorUserId, type, register.OrganizationUnitId, link);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " completed, Email sent & tracker deleted.");
                            break;
                        case CalibrationStatusEnum.Failed:
                            if (register.CreatorUserId != null)
                            {
                                SendEmailFailed((long)register.CreatorUserId, type, register.OrganizationUnitId, link, register.ExceptionComment);
                            }
                            _guidTrackreRepository.Delete(item.Id);
                            Logger.Debug("RunningRegisterCheckWorker: " + type + ": " + item.RegisterId + " Failed, Email sent & tracker deleted.");
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        private void SendEmailCompleted(long userId, string type, long ouId, string link)
        {
            var user = _userRepository.FirstOrDefault(userId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var ou = _ouRepository.FirstOrDefault(ouId);
            _emailer.SendEmailRunCompletedAsync(user, type, ou.DisplayName, baseUrl + link);
        }

        private void SendEmailFailed(long userId, string type, long ouId, string link, string exception)
        {
            var user = _userRepository.FirstOrDefault(userId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var ou = _ouRepository.FirstOrDefault(ouId);
            _emailer.SendEmailRunFailedAsync(user, type, ou.DisplayName, baseUrl + link, exception);
        }
    }
}
