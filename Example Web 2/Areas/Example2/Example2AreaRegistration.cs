using System.Web.Mvc;

namespace Example_Web_2.Areas.Example2
{
    public class Example2AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Example2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Example2_default",
                "Example2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
