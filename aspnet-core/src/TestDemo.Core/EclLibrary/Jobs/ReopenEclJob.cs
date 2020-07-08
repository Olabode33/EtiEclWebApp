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
using TestDemo.OBE;
using TestDemo.Retail;
using TestDemo.Wholesale;

namespace TestDemo.EclLibrary.Jobs
{
    public class ReopenEclJob : BackgroundJob<RunEclJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _customEclRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<InvestmentEcl, Guid> _investmentEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleRepository;

        public ReopenEclJob(IEclCustomRepository customEclRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<WholesaleEcl, Guid> wholesaleRepository,
            IRepository<InvestmentEcl, Guid> investmentEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IAppNotifier appNotifier)
        {
            _customEclRepository = customEclRepository;
            _appNotifier = appNotifier;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _wholesaleRepository = wholesaleRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _investmentEclRepository = investmentEclRepository;
        }

        [UnitOfWork]
        public override void Execute(RunEclJobArgs args)
        {
            long ouId = 0;
            int frameworId = 0;

            switch (args.EclType)
            {
                case EclType.Investment:
                    _customEclRepository.RunInvestmentReopenEclStoredProcedure(args.EclId);
                    var iEcl = _investmentEclRepository.FirstOrDefault(args.EclId);
                    ouId = iEcl.OrganizationUnitId;
                    frameworId = (int)FrameworkEnum.Investments;
                    break;
                case EclType.Wholesale:
                    _customEclRepository.RunWholesaleReopenEclStoredProcedure(args.EclId);
                    var wEcl = _wholesaleRepository.FirstOrDefault(args.EclId);
                    ouId = wEcl.OrganizationUnitId;
                    frameworId = (int)FrameworkEnum.Wholesale;
                    break;
                case EclType.Retail:
                    _customEclRepository.RunRetailReopenEclStoredProcedure(args.EclId);
                    var retailEcl = _retailEclRepository.FirstOrDefault(args.EclId);
                    ouId = retailEcl.OrganizationUnitId;
                    frameworId = (int)FrameworkEnum.Retail;
                    break;
                case EclType.Obe:
                    _customEclRepository.RunObeReopenEclStoredProcedure(args.EclId);
                    var oEcl = _obeEclRepository.FirstOrDefault(args.EclId);
                    ouId = oEcl.OrganizationUnitId;
                    frameworId = (int)FrameworkEnum.OBE;
                    break;
                default:
                    Logger.Error("CloseEclJob: Ecl type does not exists :" + args.EclType.ToString());
                    break;
            }

            // Send notification to user.
            //AsyncHelper.RunSync(() => _appNotifier.EclClosed(args.UserIdentifier));
            SendEmailAlert(args, ouId, frameworId);
        }


        private void SendEmailAlert(RunEclJobArgs args, long ouID, int frameworkId)
        {
            var user = _userRepository.FirstOrDefault(args.UserIdentifier.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            string link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/" + args.EclId;
            var type = args.EclType.ToString() + " ECL";
            var ou = _ouRepository.FirstOrDefault(ouID);
            AsyncHelper.RunSync(() => _emailer.SendEmailReopenedAsync(user, type, ou.DisplayName, link));
        }
    }
}
