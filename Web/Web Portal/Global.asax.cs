using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.UI.InputBuilder;
using StructureMap;

namespace AbstractAir.Web.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            ObjectFactory.Initialize(x => x.AddRegistry(new CoreRegistry()));

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            // Used for testing routes
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            InputBuilder.BootStrap();
        }
    }
}