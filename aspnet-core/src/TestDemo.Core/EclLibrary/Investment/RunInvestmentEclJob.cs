using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.Configuration;
using TestDemo.EclLibrary.BaseEngine.Dtos;
using TestDemo.EclShared;
using TestDemo.EclShared.Emailer;
using TestDemo.Investment;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;

namespace TestDemo.EclLibrary.Investment
{
    public class RunInvestmentEclJob : BackgroundJob<RunEclJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _investmentEclRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<InvestmentEcl, Guid> _eclRepository;

        public RunInvestmentEclJob(IEclCustomRepository investmentEclRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<InvestmentEcl, Guid> eclRepository,
            IAppNotifier appNotifier)
        {
            _investmentEclRepository = investmentEclRepository;
            _appNotifier = appNotifier;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _eclRepository = eclRepository;
        }

        //[UnitOfWork]
        public override void Execute(RunEclJobArgs args)
        {
            _investmentEclRepository.RunInvestmentPreOverrideEclStoredProcedure(args.EclId);
            SendEmailAlert(args);
            // Send notification to user.
            //AsyncHelper.RunSync(() => _appNotifier.EclClosed(args.UserIdentifier));
        }

        private void SendEmailAlert(RunEclJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.UserIdentifier.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var frameworkId = (int)FrameworkEnum.Investments;
            string link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/";
            var type = "Investment ECL Pre-override";
            var ecl = _eclRepository.FirstOrDefault(args.EclId);
            var ou = _ouRepository.FirstOrDefault(ecl.OrganizationUnitId);
            _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }
    }
}
