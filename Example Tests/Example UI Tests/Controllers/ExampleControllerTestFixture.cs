﻿using System;

using AbstractAir.Example.UI.Area.Controllers;
using AbstractAir.Example.UI.Area.Models;
using AbstractAir.Examples.InternalMessages;

using MbUnit.Framework;

using NServiceBus;

using Rhino.Mocks;

namespace AbstractAir.Example.UI.Tests.Controllers
{
	public class ExampleControllerTestFixture
	{
		private const string TestName = "Test Product";
		private const string TestCategory = "Test Category";

		private CreateProductModel _createProductModel;
		private IBus _bus;
		private ExampleController _exampleController;

		[SetUp]
		public void Setup()
		{
			_bus = MockRepository.GenerateStub<IBus>();

			_createProductModel = new CreateProductModel { Name = TestName, Category = TestCategory };

			_exampleController = new ExampleController(_bus);
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

			_bus.AssertWasCalled(bus => bus.Send(Arg<Action<ICreateProductMessage>>.Is.NotNull));
		}
	}
}