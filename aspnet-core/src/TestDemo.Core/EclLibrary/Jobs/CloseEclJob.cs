using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.EclShared;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;

namespace TestDemo.EclLibrary.Jobs
{
    public class CloseEclJob: BackgroundJob<RunEclJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _customEclRepository;
        private readonly IAppNotifier _appNotifier;

        public CloseEclJob(IEclCustomRepository investmentEclRepository,
            IAppNotifier appNotifier)
        {
            _customEclRepository = investmentEclRepository;
            _appNotifier = appNotifier;
        }

        [UnitOfWork]
        public override void Execute(RunEclJobArgs args)
        {
            switch (args.EclType)
            {
                case EclType.Investment:
                    _customEclRepository.RunInvestmentCloseEclStoredProcedure(args.EclId);
                    break;
                case EclType.Wholesale:
                    _customEclRepository.RunWholesaleCloseEclStoredProcedure(args.EclId);
                    break;
                case EclType.Retail:
                    _customEclRepository.RunRetailCloseEclStoredProcedure(args.EclId);
                    break;
                case EclType.Obe:
                    _customEclRepository.RunObeCloseEclStoredProcedure(args.EclId);
                    break;
                default:
                    Logger.Error("CloseEclJob: Ecl type does not exists :" + args.EclType.ToString());
                    break;
            }
            // Send notification to user.
            //AsyncHelper.RunSync(() => _appNotifier.EclClosed(args.UserIdentifier));
        }
    }
}
