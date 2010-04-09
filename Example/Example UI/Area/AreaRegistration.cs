using System.Web.Mvc;

using MvcContrib.PortableAreas;

namespace AbstractAir.Example.UI.Area
{
	public class PortableAreaAreaRegistration : PortableAreaRegistration
	{
		public override string AreaName
		{
			get { return Constants.AreaName; }
		}

		public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
		{
			context.MapRoute("Example_UI_default",
				"Example/{controller}/{action}/{id}",
				new {action = "Index", controller = "Example", id = ""});

			base.RegisterTheViewsInTheEmbeddedViewEngine(GetType());
		}
	}
}