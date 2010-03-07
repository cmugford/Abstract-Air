using System;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Tests
{
	public class TimesTestFixture
	{
		[Test]
		[Row(1)]
		[Row(6)]
		[Row(250)]
		public void ActionInvokedSpecifiedNumberOfTimes(int numberOfTimes)
		{
			var action = MockRepository.GenerateMock<Action>();

			action.Expect(a => a()).Repeat.Times(numberOfTimes);

			numberOfTimes.Times(action);

			action.VerifyAllExpectations();
		}

		[Test]
		public void ActionNotInvokedForZero()
		{
			var action = MockRepository.GenerateStub<Action>();

			0.Times(action);

			action.AssertWasNotCalled(a => a());
		}

		[Test]
		[Row(-1)]
		[Row(-6)]
		[Row(Int32.MinValue)]
		public void TimesThrowsArgumentOutOfRangeForNegative(int numberOfTimes)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => numberOfTimes.Times(MockRepository.GenerateStub<Action>()));
		}
	}
}