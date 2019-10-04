using Microsoft.AspNetCore.Mvc;
using TestDemo.Web.Controllers;

namespace TestDemo.Web.Public.Controllers
{
    public class AboutController : TestDemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}