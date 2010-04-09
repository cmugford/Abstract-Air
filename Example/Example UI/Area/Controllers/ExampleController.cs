using System;
using System.Web.Mvc;

using AbstractAir.Example.UI.Area.Models;
using AbstractAir.Examples.InternalMessages;

using NServiceBus;

namespace AbstractAir.Example.UI.Area.Controllers
{
    [HandleError]
    public class ExampleController : Controller
    {
    	private readonly IBus _bus;

		public ExampleController(IBus bus)
		{
			_bus = ArgumentValidation.IsNotNull(bus, "bus");
		}

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult CreateProduct()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ValidateAntiForgeryToken]
		public ActionResult CreateProduct(CreateProductModel createProductModel)
		{
			_bus.CreateInstance<ICreateProductMessage>();
			_bus.Send<ICreateProductMessage>(message =>
				{
					message.ProductId = Guid.NewGuid();
					message.Name = createProductModel.Name;
					message.Category = createProductModel.Category;
				});

			return RedirectToAction("Index");
		}
    }
}
