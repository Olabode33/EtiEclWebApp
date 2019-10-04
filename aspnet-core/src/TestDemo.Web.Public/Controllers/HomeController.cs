using Microsoft.AspNetCore.Mvc;
using TestDemo.Web.Controllers;

namespace TestDemo.Web.Public.Controllers
{
    public class HomeController : TestDemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}