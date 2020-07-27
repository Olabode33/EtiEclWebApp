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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization;
using TestDemo.Authorization.Users;
using TestDemo.CalibrationInput;
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.InvestmentComputation;

namespace TestDemo.EclShared.Jobs
{
    public class SendEmailJob : BackgroundJob<SendEmailJobArgs>, ITransientDependency
    {
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly UserManager _userManager;

        public SendEmailJob(IEclEngineEmailer emailer, IHostingEnvironment env, UserManager userManager,
            IRepository<User, long> lookup_userRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository)
        {
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userManager = userManager;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

        [UnitOfWork]
        public override void Execute(SendEmailJobArgs args)
        {
            switch (args.SendEmailType)
            {
                case SendEmailTypeEnum.EclSubmittedEmail:
                    AsyncHelper.RunSync(() => SendSubmittedEmail(args, AppPermissions.Pages_EclView_Review));
                    break;
                case SendEmailTypeEnum.EclAwaitingApprovalEmail:
                    AsyncHelper.RunSync(() => SendAdditionalApprovalEmail(args, AppPermissions.Pages_EclView_Review));
                    break;
                case SendEmailTypeEnum.CalibrationSubmittedEmail:
                    AsyncHelper.RunSync(() => SendSubmittedEmail(args, AppPermissions.Pages_Calibration_Review));
                    break;
                case SendEmailTypeEnum.CalibrationAwaitingApprovalEmail:
                    AsyncHelper.RunSync(() => SendAdditionalApprovalEmail(args, AppPermissions.Pages_Calibration_Review));
                    break;
                case SendEmailTypeEnum.EclOverrideSubmittedEmail:
                    AsyncHelper.RunSync(() => SendSubmittedEmail(args, AppPermissions.Pages_EclView_Override));
                    break;
                case SendEmailTypeEnum.EclOverrideAwaitingApprovalEmail:
                    AsyncHelper.RunSync(() => SendAdditionalApprovalEmail(args, AppPermissions.Pages_EclView_Override_Review));
                    break;
                case SendEmailTypeEnum.CalibrationApprovedEmail:
                case SendEmailTypeEnum.EclApprovedEmail:
                case SendEmailTypeEnum.EclOverrideApprovedEmail:
                    AsyncHelper.RunSync(() => SendApprovedEmail(args));
                    break;
                default:
                    break;
            }
        }

        public async Task SendSubmittedEmail(SendEmailJobArgs args, string permissionName)
        {
            var users = _lookup_userRepository.GetAllList();
            var ou = await _organizationUnitRepository.FirstOrDefaultAsync(e => e.Id == args.AffiliateId);
            
            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    if (await _userManager.IsInOrganizationUnitAsync(user.Id, args.AffiliateId) && await _userManager.IsGrantedAsync(user.Id, permissionName))
                    {
                        await _emailer.SendEmailSubmittedForApprovalAsync(user, args.Type, ou != null ? ou.DisplayName : "", args.Link);
                    }
                }
            }
        }

        public async Task SendAdditionalApprovalEmail(SendEmailJobArgs args, string permissionName)
        {
            var users = _lookup_userRepository.GetAllList();
            var ou = await _organizationUnitRepository.FirstOrDefaultAsync(e => e.Id == args.AffiliateId);

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    if (await _userManager.IsInOrganizationUnitAsync(user.Id, args.AffiliateId) && await _userManager.IsGrantedAsync(user.Id, permissionName))
                    {
                        await _emailer.SendEmailSubmittedForAdditionalApprovalAsync(user, args.Type, ou != null ? ou.DisplayName : "", args.Link);
                    }
                }
            }
        }

        public async Task SendApprovedEmail(SendEmailJobArgs args)
        {
            var user = _lookup_userRepository.FirstOrDefault(args.UserId);
            var ou = _organizationUnitRepository.FirstOrDefault(args.AffiliateId);
            await _emailer.SendEmailApprovedAsync(user, args.Type, ou != null ? ou.DisplayName : "", args.Link);
        }
    }
}
