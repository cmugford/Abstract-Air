using System;
using System.Web.Mvc;
using StructureMap;

namespace AbstractAir.Web.Portal
{
    /// <summary>
    /// Structure Map Controller Factory
    /// </summary>
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Gets the controller instance.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns></returns>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType != null)
                {
                    IController controller = ObjectFactory.GetInstance(controllerType) as IController;
                    ObjectFactory.BuildUp(controller);
                    return controller;
                }
                else
                {
                    return null;
                }
            }
            catch (StructureMapException)
            {
                throw;
            }
        }
    }
}
