using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace TestDemo.Web.Controllers
{
    public class HomeController : TestDemoControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ui");
        }
    }
}
