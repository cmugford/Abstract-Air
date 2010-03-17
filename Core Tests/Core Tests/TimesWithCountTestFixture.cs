using System;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Tests
{
	public class TimesWithCountTestFixture
	{
		[Test]
		[Row(1)]
		[Row(6)]
		[Row(25)]
		public void ActionInvokedSpecifiedNumberOfTimes(int numberOfTimes)
		{
			var action = MockRepository.GenerateMock<Action<int>>();

			for (var number = 1; number <= numberOfTimes; number++)
			{
				var expectedCount = number;
				action.Expect(a => a(expectedCount));
			}

			numberOfTimes.Times(action);

			action.VerifyAllExpectations();
		}

		[Test]
		public void ActionNotInvokedForZero()
		{
			var action = MockRepository.GenerateStub<Action<int>>();

			0.Times(action);

			action.AssertWasNotCalled(a => a(Arg<int>.Is.Anything));
		}

		[Test]
		[Row(-1)]
		[Row(-6)]
		[Row(Int32.MinValue)]
		public void TimesThrowsArgumentOutOfRangeForNegative(int numberOfTimes)
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => numberOfTimes.Times(MockRepository.GenerateStub<Action<int>>()));
		}
	}
}