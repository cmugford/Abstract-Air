using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NServiceBus;
using AbstractAir;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Example.Web.StandAlone.Models;

namespace AbstractAir.Example.Web.StandAlone.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBus _bus;

        public ProductController(IBus bus)
        {
            _bus = ArgumentValidation.IsNotNull(bus, "bus");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreatePost(ProductCreateEditModel model)
        {
            var productId = Guid.NewGuid();

            _bus.Send<ICreateProductMessage>(message =>
                {
                    message.Name = model.Name;
                    message.ProductId = productId;
                    message.Category = model.Category;
                });

            return RedirectToAction("Details", new {id = productId} );
        }

        public ActionResult Details(Guid id)
        {
            return View();
        }
    }
}
