using Abp.Authorization;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Users;

namespace TestDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
