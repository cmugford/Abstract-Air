using System;
using System.Web.Mvc;

using AbstractAir.Example.UI.Area.Models;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Portal;

namespace AbstractAir.Example.UI.Area.Controllers
{
    [HandleError]
    public class ExampleController : Controller
    {
    	private readonly ICommandBus _commandBus;

		public ExampleController(ICommandBus commandBus)
		{
			_commandBus = ArgumentValidation.IsNotNull(commandBus, "commandBus");
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
			var result = _commandBus.Send<ICreateProductMessage>(this, message =>
				{
					message.ProductId = Guid.NewGuid();
					message.Name = createProductModel.Name;
					message.Category = createProductModel.Category;
				});

			return result ? RedirectToAction("Index") : (ActionResult) View();
		}
    }
}
