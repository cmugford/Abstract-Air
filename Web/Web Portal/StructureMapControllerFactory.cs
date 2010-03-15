using System;
using System.Web.Mvc;
using System.Web.Routing;

using StructureMap;

namespace AbstractAir.Web.Portal
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return null;
			}

			var controller = ObjectFactory.GetInstance(controllerType) as IController;
			ObjectFactory.BuildUp(controller);
			return controller;
		}
	}
}