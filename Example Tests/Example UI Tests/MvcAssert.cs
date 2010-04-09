using System;
using System.Web.Mvc;

using MbUnit.Framework;

namespace AbstractAir.Example.UI.Tests
{
	public static class MvcAssert
	{
		public static void IsViewResult(ActionResult actionResult)
		{
			Assert.IsInstanceOfType<ViewResult>(actionResult);
		}

		public static void IsRedirectToRouteResult(ActionResult actionResult)
		{
			Assert.IsInstanceOfType<RedirectToRouteResult>(actionResult);
		}

		public static void IsRedirectToRouteResult(ActionResult actionResult, string action)
		{
			var redirectToRouteResult = actionResult as RedirectToRouteResult;
			Assert.IsNotNull(redirectToRouteResult);
			Assert.AreEqual(action, redirectToRouteResult.RouteValues["action"]);
		}

		public static void HasValidationErrors(ActionResult actionResult, string erroredProperty, int errorCount)
		{
			var viewResult = actionResult as ViewResult;
			Assert.IsNotNull(viewResult);
			Assert.AreEqual(errorCount, viewResult.ViewData.ModelState[erroredProperty].Errors.Count);
		}
	}
}