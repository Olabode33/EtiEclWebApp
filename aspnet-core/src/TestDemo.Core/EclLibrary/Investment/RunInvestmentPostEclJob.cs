using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Threading;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;

namespace TestDemo.EclLibrary.Investment
{
    public class RunInvestmentPostEclJob : BackgroundJob<RunEclJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _investmentEclRepository;
        private readonly IAppNotifier _appNotifier;

        public RunInvestmentPostEclJob(IEclCustomRepository investmentEclRepository,
            IAppNotifier appNotifier)
        {
            _investmentEclRepository = investmentEclRepository;
            _appNotifier = appNotifier;
        }

        [UnitOfWork]
        public override void Execute(RunEclJobArgs args)
        {
            _investmentEclRepository.RunInvestmentPostOverrideEclStoredProcedure(args.EclId);

            // Send notification to user.
            //AsyncHelper.RunSync(() => _appNotifier.EclClosed(args.UserIdentifier));
        }
    }
}
