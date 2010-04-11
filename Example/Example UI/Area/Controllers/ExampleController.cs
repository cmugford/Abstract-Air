using System;
using System.Linq;
using System.Web.Mvc;

using AbstractAir.Example.UI.Area.Models;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Portal;
using Norm.Linq;
using AbstractAir.Examples.QuerySchemas;

namespace AbstractAir.Example.UI.Area.Controllers
{
    [HandleError]
    public class ExampleController : Controller
    {
    	private readonly ICommandBus _commandBus;
        private readonly IMongoQueryProvider _mongoQueryProvider;

		public ExampleController(ICommandBus commandBus,
            IMongoQueryProvider mongoQueryProvider)
		{
			_commandBus = ArgumentValidation.IsNotNull(commandBus, "commandBus");
            _mongoQueryProvider = ArgumentValidation.IsNotNull(mongoQueryProvider, "mongoQueryProvider");
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
            var productId = Guid.NewGuid();

			var result = _commandBus.Send<ICreateProductMessage>(this, message =>
				{
					message.ProductId = productId;
					message.Name = createProductModel.Name;
					message.Category = createProductModel.Category;
				});

			return result ? RedirectToAction("Details", new {id = productId})
                : (ActionResult) View();
		}

        public ActionResult Details(Guid id)
        {
            var model = new ProductSummaryModel();

            var query = new MongoQuery<ProductSummary>(_mongoQueryProvider);

            var summary = query.FirstOrDefault(productSummary => productSummary.ProductId == id);

            model.Name = summary.Name;
            model.ProductId = summary.ProductId;

            return View(model);
        }
    }
}
