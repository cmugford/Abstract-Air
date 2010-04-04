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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(string productName)
        {
            // TODO: Create the product

            // TODO: Get the ID

            return RedirectToAction("Index");
        }
    }
}
