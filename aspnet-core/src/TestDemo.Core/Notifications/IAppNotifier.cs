using System;
using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using TestDemo.Authorization.Users;
using TestDemo.MultiTenancy;

namespace TestDemo.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task GdprDataPrepared(UserIdentifier user, Guid binaryObjectId);
        Task EclReportGenerated(UserIdentifier user, string fileToken, string fileType, string fileName);
        Task EclClosed(UserIdentifier user);
        Task EclReopened(UserIdentifier user);
        Task EclComputed(UserIdentifier user);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);

        Task TenantsMovedToEdition(UserIdentifier argsUser, string sourceEditionName, string targetEditionName);
        Task SomeDataCouldntBeImported(UserIdentifier argsUser, string fileToken, string fileType, string fileName);
        Task SomeUsersCouldntBeImported(UserIdentifier argsUser, string fileToken, string fileType, string fileName);
    }
}
