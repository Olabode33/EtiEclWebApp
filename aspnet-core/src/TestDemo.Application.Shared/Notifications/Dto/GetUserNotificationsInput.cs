using Abp.Notifications;
using TestDemo.Dto;

namespace TestDemo.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}