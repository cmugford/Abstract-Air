using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;

using StructureMap;

namespace AbstractAir.Portal
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		private const string AreaKey = "area";

		private readonly IContainer _container;

		[CLSCompliant(false)]
		public StructureMapControllerFactory(IContainer container)
		{
			_container = ArgumentValidation.IsNotNull(container, "container");
		}

		public override IController CreateController(RequestContext context, string controllerName)
		{
			return _container.GetInstance<IController>(DetermineControllerName(context.RouteData, controllerName));
		}

		private static string DetermineControllerName(RouteData routeData, string controllerName)
		{
			if (routeData.DataTokens.Count == 0 || !routeData.DataTokens.ContainsKey(AreaKey))
			{
				return controllerName;
			}

			return string.Format(CultureInfo.InvariantCulture,
				"{0}:{1}",
				routeData.DataTokens[AreaKey],
				controllerName);
		}
	}
}