using System.Web.Mvc;

namespace AbstractAir.Example.Web.Areas.Example.Controllers
{
    [HandleError]
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
