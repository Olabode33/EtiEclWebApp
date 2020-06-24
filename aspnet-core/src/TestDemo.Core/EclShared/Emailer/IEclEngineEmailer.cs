using System.Threading.Tasks;
using TestDemo.Authorization.Users;

namespace TestDemo.EclShared.Emailer
{
    public interface IEclEngineEmailer
    {
        Task SendEmailAffiliateCopiedAsync(User user, string fromAffiliate, string toAffiliate, string link);
        Task SendEmailAssumptionAppiedAsync(User user, string type, string fromAffiliate, string toAffiliate, string link);
        Task SendEmailApprovedAsync(User user, string type, string affiliateName, string link);
        Task SendEmailClosedAsync(User user, string type, string affiliateName, string link);
        Task SendEmailDataUploadCompleteAsync(User user, string uploadType, string affiliateName, string link);
        Task SendEmailInvalidDataUploadCompleteAsync(User user, string uploadType, string affiliateName, string link);
        Task SendEmailReopenedAsync(User user, string type, string affiliateName, string link);
        Task SendEmailReportGeneratedAsync(User user, string type, string affiliateName, string link);
        Task SendEmailRunCompletedAsync(User user, string type, string affiliateName, string link);
        Task SendEmailRunFailedAsync(User user, string type, string affiliateName, string link, string reason);
        Task SendEmailSubmittedForAdditionalApprovalAsync(User user, string type, string affiliateName, string link);
        Task SendEmailSubmittedForApprovalAsync(User user, string type, string affiliateName, string link);
    }
}