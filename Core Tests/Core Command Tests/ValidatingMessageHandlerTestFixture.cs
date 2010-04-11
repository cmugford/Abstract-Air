using System;
using System.Collections.Generic;

using MbUnit.Framework;

using NServiceBus;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Commands.Tests
{
	public class ValidatingMessageHandlerTestFixture
	{
		private const string TestName = "Test Property";
		private const string TestErrorMessage = "Test Error Message";

		private IBus _bus;
		private IContainer _container;
		private ITestCommandMessage _testCommandMessage;

		private ValidatingMessageHandler _validatingMessageHandler;

		[SetUp]
		public void Setup()
		{
			_bus = MockRepository.GenerateStub<IBus>();
			_container = MockRepository.GenerateStub<IContainer>();
			_testCommandMessage = MockRepository.GenerateStub<ITestCommandMessage>();

			_validatingMessageHandler = new ValidatingMessageHandler(_bus, _container);
		}

		[Test]
		public void MessageWithoutValidatorsContinuesDispatching()
		{
			_container.Stub(container => container.GetAllInstances(typeof(IValidator<ITestCommandMessage>))).Return(new List<IValidator<ITestCommandMessage>>());

			_validatingMessageHandler.Handle(_testCommandMessage);

			_bus.AssertWasNotCalled(bus => bus.DoNotContinueDispatchingCurrentMessageToHandlers());
		}

		[Test]
		public void ValidMessageContinuesDispatching()
		{
			var validator = MockRepository.GenerateStub<IValidator<ITestCommandMessage>>();
			_container.Stub(container => container.GetAllInstances(typeof(IValidator<ITestCommandMessage>)))
				.Return(new List<IValidator<ITestCommandMessage>> { validator });
			validator.Stub(v => v.Validate((IMessage) _testCommandMessage)).Return(new List<ValidationError>());

			_validatingMessageHandler.Handle(_testCommandMessage);

			_bus.AssertWasNotCalled(bus => bus.DoNotContinueDispatchingCurrentMessageToHandlers());
		}

		[Test]
		public void InvalidMessageDoesNotContinueDispatching()
		{
			var validator = MockRepository.GenerateStub<IValidator<ITestCommandMessage>>();
			_container.Stub(container => container.GetAllInstances(typeof(IValidator<ITestCommandMessage>)))
				.Return(new List<IValidator<ITestCommandMessage>> { validator });
			validator.Stub(v => v.Validate((IMessage) _testCommandMessage)).Return(new List<ValidationError>
				{
					new ValidationError(TestName, TestErrorMessage)
				});

			_validatingMessageHandler.Handle(_testCommandMessage);

			_bus.AssertWasCalled(bus => bus.DoNotContinueDispatchingCurrentMessageToHandlers());
		}
	}
}