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
using System.Threading.Tasks;
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

            AsyncHelper.RunSync(() => CopyAffiliateAssumptions(input));
            SendCopyCompleteNotification(args);
        }

        private async Task CopyAffiliateAssumptions(CopyAffiliateDto input)
        {
            await CopyReportingDate(input);
            await CopyFrameworkAssumption(input);
            await CopyEadInputAssumption(input);
            await CopyLgdInputAssumption(input);
            await CopyPdInputAssumption(input);
            await CopyPdMacroInputAssumption(input);
            await CopyPdMacroProjectAssumption(input);
            await CopyPdNonInternalModelAssumption(input);
            await CopyPdNplAssumption(input);
            await CopyPdSnpAssumption(input);
            await CopyInvesPdMacroAssumption(input);
            await CopyPdFitchAssumption(input);
            await CopyMacroVariables(input);
        }

        private async Task CopyReportingDate(CopyAffiliateDto input)
        {
            var from = await _affiliateAssumptions.FirstOrDefaultAsync(e => e.OrganizationUnitId == input.FromAffiliateId);
            var to = await _affiliateAssumptions.FirstOrDefaultAsync(e => e.OrganizationUnitId == input.ToAffiliateId);

            if (to == null)
            {
                await _affiliateAssumptions.InsertAsync(new AffiliateAssumption
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

                await _affiliateAssumptions.UpdateAsync(to);
            }
        }
        private async Task CopyFrameworkAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _frameworkAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _frameworkAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _frameworkAssumptionRepository.InsertAsync(new Assumption()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private async Task CopyEadInputAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _eadAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _eadAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _eadAssumptionRepository.InsertAsync(new EadInputAssumption()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private async Task CopyLgdInputAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _lgdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _lgdAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _lgdAssumptionRepository.InsertAsync(new LgdInputAssumption()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdInputAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdAssumptionRepository.InsertAsync(new PdInputAssumption()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdMacroInputAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdAssumptionMacroEcoInputRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdAssumptionMacroEcoInputRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdAssumptionMacroEcoInputRepository.InsertAsync(new PdInputAssumptionMacroeconomicInput()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdMacroProjectAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdAssumptionMacroecoProjectionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdAssumptionMacroecoProjectionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdAssumptionMacroecoProjectionRepository.InsertAsync(new PdInputAssumptionMacroeconomicProjection()
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
                        RequiresGroupApproval = assumption.RequiresGroupApproval
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdNonInternalModelAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdAssumptionNonInternalModelRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdAssumptionNonInternalModelRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdAssumptionNonInternalModelRepository.InsertAsync(new PdInputAssumptionNonInternalModel()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdNplAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdAssumptionNplIndexRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdAssumptionNplIndexRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdAssumptionNplIndexRepository.InsertAsync(new PdInputAssumptionNplIndex()
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
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdSnpAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _pdSnPCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _pdSnPCummulativeAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _pdSnPCummulativeAssumptionRepository.InsertAsync(new PdInputAssumptionSnPCummulativeDefaultRate()
                    {
                        Rating = assumption.Rating,
                        Key = assumption.Key,
                        Years = assumption.Years,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId,
                        Framework = assumption.Framework
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyInvesPdMacroAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _invsecMacroEcoAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _invsecMacroEcoAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _invsecMacroEcoAssumptionRepository.InsertAsync(new InvSecMacroEconomicAssumption()
                    {
                        Key = assumption.Key,
                        Month = assumption.Month,
                        BestValue = assumption.BestValue,
                        OptimisticValue = assumption.OptimisticValue,
                        DownturnValue = assumption.DownturnValue,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        CanAffiliateEdit = assumption.CanAffiliateEdit,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }

        }
        private async Task CopyPdFitchAssumption(CopyAffiliateDto input)
        {
            var assumptions = await _invsecFitchCummulativeAssumptionRepository.GetAll()
                                    .Where(x => x.OrganizationUnitId == input.FromAffiliateId)
                                    .ToListAsync();

            if (assumptions.Count > 0)
            {
                await _invsecFitchCummulativeAssumptionRepository.HardDeleteAsync(x => x.OrganizationUnitId == input.ToAffiliateId);

                foreach (var assumption in assumptions)
                {
                    await _invsecFitchCummulativeAssumptionRepository.InsertAsync(new InvSecFitchCummulativeDefaultRate()
                    {
                        Key = assumption.Key,
                        Rating = assumption.Rating,
                        Year = assumption.Year,
                        Value = assumption.Value,
                        RequiresGroupApproval = assumption.RequiresGroupApproval,
                        Status = assumption.Status,
                        OrganizationUnitId = input.ToAffiliateId
                    });
                }
            }
            else
            {
                throw new UserFriendlyException(L("AffiliateAssumptionDoesNotExistError"));
            }
        }
        private async Task CopyMacroVariables(CopyAffiliateDto input)
        {
            var from = await _affiliateMacroVariableRepository.GetAll()
                                    .Where(x => x.AffiliateId == input.FromAffiliateId)
                                    .ToListAsync();

            if (from.Count > 0)
            {
                await _affiliateMacroVariableRepository.DeleteAsync(x => x.AffiliateId == input.ToAffiliateId);

                foreach (var item in from)
                {
                    await _affiliateMacroVariableRepository.InsertAsync(new AffiliateMacroEconomicVariableOffset()
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
