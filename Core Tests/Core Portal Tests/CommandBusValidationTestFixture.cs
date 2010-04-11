using System.Collections.Generic;
using System.Linq;

using AbstractAir.Commands;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Portal.Tests
{
	public class CommandBusValidationTestFixture : CommandBusTestFixtureBase
	{
		private const string PropertyName = "TestProperty";
		private const string ErrorMessage = "Test Error Message";

		private IValidator<ITestCommandMessage> _validator;
		private List<ValidationError> _validationErrors;

		[SetUp]
		public override void Setup()
		{
			base.Setup();

			_validator = MockRepository.GenerateStub<IValidator<ITestCommandMessage>>();
			_validationErrors = new List<ValidationError>();

			_validator.Stub(v => v.Validate(TestCommandMessage)).Return(_validationErrors);
			Container.Stub(container => container.GetAllInstances<IValidator<ITestCommandMessage>>())
				.Return(new List<IValidator<ITestCommandMessage>> {_validator});
		}

		[Test]
		public void ValidMessageInstanceDoesntAddErrorsToModelState()
		{
			CommandBus.Send<ITestCommandMessage>(TestController, message => { });

			Assert.IsTrue(TestController.ModelState.Values.All(item => item.Errors.Count != 0));
		}

		[Test]
		public void InvalidMessageInstanceAddsErrorToModelState()
		{
			_validationErrors.Add(new ValidationError(PropertyName, ErrorMessage));

			CommandBus.Send<ITestCommandMessage>(TestController, message => { });

			Assert.IsTrue(TestController.ModelState[PropertyName].Errors.Any(error => error.ErrorMessage == ErrorMessage));
		}

		[Test]
		public void InvalidMessageDoesNotSendToNServiceBus()
		{
			_validationErrors.Add(new ValidationError(PropertyName, ErrorMessage));

			CommandBus.Send<ITestCommandMessage>(TestController, message => { });

			Bus.AssertWasNotCalled(bus => bus.Send(Arg<ITestCommandMessage>.Is.Anything));
		}
	}
}