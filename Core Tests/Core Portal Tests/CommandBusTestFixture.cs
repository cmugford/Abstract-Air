using System;
using System.Collections.Generic;

using AbstractAir.Commands;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Portal.Tests
{
	public class CommandBusTestFixture : CommandBusTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();

			Container.Stub(container => container.GetAllInstances<IValidator<ITestCommandMessage>>())
				.Return(new List<IValidator<ITestCommandMessage>>());
		}

		[Test]
		public void MessageInstanceCreatedAndSentViaNServiceBus()
		{
			CommandBus.Send<ITestCommandMessage>(TestController, message => { });

			Bus.AssertWasCalled(bus => bus.Send(TestCommandMessage));
		}

		[Test]
		public void MessageInstancePassedToMessageConstructor()
		{
			var commandConstructor = MockRepository.GenerateStub<Action<ITestCommandMessage>>();

			CommandBus.Send(TestController, commandConstructor);

			commandConstructor.AssertWasCalled(constructor => constructor(TestCommandMessage));
		}
	}
}