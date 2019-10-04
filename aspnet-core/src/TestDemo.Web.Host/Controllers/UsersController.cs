using Abp.AspNetCore.Mvc.Authorization;
using TestDemo.Authorization;
using TestDemo.Storage;
using Abp.BackgroundJobs;

namespace TestDemo.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}