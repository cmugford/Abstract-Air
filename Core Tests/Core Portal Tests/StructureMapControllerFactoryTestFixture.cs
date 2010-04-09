using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using MbUnit.Framework;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Portal.Tests
{
	[TestFixture]
	public class StructureMapControllerFactoryTestFixture
	{
		private const string ControllerName = "Test";
		private const string AreaName = "Area";
		private const string AreaControllerName = AreaName + ":" + ControllerName;
		private const string AreaKey = "area";

		private IContainer _container;
		private RouteData _routeData;
		private RequestContext _requestContext;
		private StructureMapControllerFactory _structureMapControllerFactory;
		private TestController _testController;

		[SetUp]
		public void Setup()
		{
			_container = MockRepository.GenerateStub<IContainer>();
			_routeData = new RouteData();
			_requestContext = new RequestContext(MockRepository.GenerateStub<HttpContextBase>(), _routeData);
			_testController = new TestController();

			_structureMapControllerFactory = new StructureMapControllerFactory(_container);
		}

		[Test]
		public void FactoryRequestsControllerNameWithNoArea()
		{
			_container.Stub(container => container.GetInstance<IController>(ControllerName)).Return(_testController);

			var result = _structureMapControllerFactory.CreateController(_requestContext, ControllerName);

			Assert.AreSame(_testController, result);
		}

		[Test]
		public void FactoryRequestsControllerWithAreaNameAppendedWhenInArea()
		{
			_routeData.DataTokens.Add(AreaKey, AreaName);

			_container.Stub(container => container.GetInstance<IController>(AreaControllerName)).Return(_testController);

			var result = _structureMapControllerFactory.CreateController(_requestContext, ControllerName);

			Assert.AreSame(_testController, result);
		}
	}
}