using System;

using AbstractAir.Example.UI.Area.Controllers;
using AbstractAir.Example.UI.Area.Models;
using AbstractAir.Examples.InternalMessages;
using AbstractAir.Portal;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Example.UI.Tests.Controllers
{
	public class ExampleControllerTestFixture
	{
		private const string TestName = "Test Product";
		private const string TestCategory = "Test Category";

		private CreateProductModel _createProductModel;
		private ICommandBus _commandBus;
		private ExampleController _exampleController;

		[SetUp]
		public void Setup()
		{
			_commandBus = MockRepository.GenerateStub<ICommandBus>();

			_createProductModel = new CreateProductModel { Name = TestName, Category = TestCategory };

			_exampleController = new ExampleController(_commandBus);
		}

		[Test]
		public void CreateProductRedirectsToExampleIndex()
		{
			var result = _exampleController.CreateProduct(_createProductModel);

			MvcAssert.IsRedirectToRouteResult(result, "Index");
		}

		[Test]
		public void CreateProductSendsCreateProductMessage()
		{
			_exampleController.CreateProduct(_createProductModel);

			_commandBus.AssertWasCalled(bus => bus.Send(Arg.Is(_exampleController), Arg<Action<ICreateProductMessage>>.Is.NotNull));
		}
	}
}