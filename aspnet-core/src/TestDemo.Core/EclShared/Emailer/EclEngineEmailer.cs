using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using TestDemo.Chat;
using TestDemo.Editions;
using TestDemo.Localization;
using TestDemo.MultiTenancy;
using System.Net.Mail;
using System.Web;
using Abp.Runtime.Security;
using TestDemo.Net.Emailing;
using TestDemo.Authorization.Users;

namespace TestDemo.EclShared.Emailer
{
    /// <summary>
    /// Used to send email to users.
    /// </summary>
    public class EclEngineEmailer : TestDemoServiceBase, ITransientDependency, IEclEngineEmailer
    {
        private readonly IEmailTemplateProvider _emailTemplateProvider;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ISettingManager _settingManager;
        private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;

        // used for styling action links on email messages.
        private string _emailButtonStyle =
            "padding-left: 30px; padding-right: 30px; padding-top: 12px; padding-bottom: 12px; color: #ffffff; background-color: #BED600; font-size: 14pt; text-decoration: none;";
        private string _emailButtonColor = "#BED600";

        public EclEngineEmailer(
            IEmailTemplateProvider emailTemplateProvider,
            IEmailSender emailSender,
            IRepository<Tenant> tenantRepository,
            ICurrentUnitOfWorkProvider unitOfWorkProvider,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            EditionManager editionManager,
            UserManager userManager)
        {
            _emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;
            _tenantRepository = tenantRepository;
            _unitOfWorkProvider = unitOfWorkProvider;
            _unitOfWorkManager = unitOfWorkManager;
            _settingManager = settingManager;
            _editionManager = editionManager;
            _userManager = userManager;
        }

        [UnitOfWork]
        public virtual async Task SendEmailDataUploadCompleteAsync(User user, string uploadType, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailDataUploadComplete_Title"), L("EmailActivation_SubTitle", uploadType, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailDataUploadComplete_Body", uploadType, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailDataUploadComplete_Subject", uploadType), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailSubmittedForApprovalAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailRecordSubmitted_Title", type), L("EmailRecordSubmitted_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailRecordSubmitted_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailRecordSubmitted_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailSubmittedForAdditionalApprovalAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailRecordAwaitingAdditional_Title", type), L("EmailRecordAwaitingAdditional_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailRecordAwaitingAdditional_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailRecordAwaitingAdditional_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailApprovedAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailRecordApproved_Title", type), L("EmailRecordApproved_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailRecordApproved_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailRecordApproved_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailRunCompletedAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailComputationCompleted_Title", type), L("EmailComputationCompleted_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailComputationCompleted_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailComputationCompleted_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailReportGeneratedAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailReportGenerated_Title", type), L("EmailReportGenerated_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailReportGenerated_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToDownload") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailReportGenerated_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailClosedAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailClosed_Title", type), L("EmailClosed_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailClosed_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailClosed_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailReopenedAsync(User user, string type, string affiliateName, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailReopened_Title", type), L("EmailReopened_SubTitle", type, affiliateName));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailReopened_Body", type, affiliateName) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailReopened_Subject", type), emailTemplate, mailMessage);
        }

        public virtual async Task SendEmailAffiliateCopiedAsync(User user, string fromAffiliate, string toAffiliate, string link)
        {

            var emailTemplate = GetTitleAndSubTitle(user.TenantId, L("EmailAffiliateCopied_Title"), L("EmailAffiliateCopied_SubTitle", fromAffiliate, toAffiliate));
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<b>Dear " + user.Name + " " + user.Surname + ",<br />");

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine(L("EmailAffiliateCopied_Body", fromAffiliate, toAffiliate) + "<br /><br />");
            mailMessage.AppendLine(L("Email_ClickTheLinkBelowToView") + "<br /><br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor + "\" href=\"" + link + "\">" + L("View") + "</a>");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<br />");

            await ReplaceBodyAndSend(user.EmailAddress, L("EmailAffiliateCopied_Subject", fromAffiliate), emailTemplate, mailMessage);
        }



        private string GetTenancyNameOrNull(int? tenantId)
        {
            if (tenantId == null)
            {
                return null;
            }

            using (_unitOfWorkProvider.Current.SetTenantId(null))
            {
                return _tenantRepository.Get(tenantId.Value).TenancyName;
            }
        }

        private StringBuilder GetTitleAndSubTitle(int? tenantId, string title, string subTitle)
        {
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            return emailTemplate;
        }

        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate.ToString(),
                IsBodyHtml = true
            });
        }

        /// <summary>
        /// Returns link with encrypted parameters
        /// </summary>
        /// <param name="link"></param>
        /// <param name="encrptedParameterName"></param>
        /// <returns></returns>
        private string EncryptQueryParameters(string link, string encrptedParameterName = "c")
        {
            if (!link.Contains("?"))
            {
                return link;
            }

            var uri = new Uri(link);
            var basePath = link.Substring(0, link.IndexOf('?'));
            var query = uri.Query.TrimStart('?');

            return basePath + "?" + encrptedParameterName + "=" + HttpUtility.UrlEncode(SimpleStringCipher.Instance.Encrypt(query));
        }
    }
}
