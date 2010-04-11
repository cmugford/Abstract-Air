using System;

using MbUnit.Framework;

using NServiceBus;

using Rhino.Mocks;

namespace AbstractAir.Commands.Tests
{
	public class ValidatorBaseTestFixture
	{
		[Test]
		public void NonGenericValidateInvokesGenericValidate()
		{
			var testCommandMessage = MockRepository.GenerateStub<ITestCommandMessage>();
			var validatorBase = MockRepository.GenerateStub<ValidatorBase<ITestCommandMessage>>();

			validatorBase.Validate((IMessage) testCommandMessage);

			validatorBase.AssertWasCalled(validator => validator.Validate(testCommandMessage));
		}
	}
}