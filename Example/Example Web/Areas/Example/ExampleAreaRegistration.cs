using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace AbstractAir.Example.Web.Areas.Example
{
    public class ExampleAreaRegistration : PortableAreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Example";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            context.MapRoute(
                "Example_default",
                "Example/{controller}/{action}/{id}",
                new { action = "Index", id = "" }
            );

            RegisterTheViewsInTheEmbeddedViewEngine(GetType());
        }
    }
}
