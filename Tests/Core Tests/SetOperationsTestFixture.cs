using System;
using System.Collections.Generic;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Tests
{
	public class SetOperationsTestFixture
	{
		[Test]
		public void CannotApplyToNullEnumeration()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<object>) null).Apply(instance => instance.GetHashCode()));
		}

		[Test]
		public void CannotApplyWithNullAction()
		{
			var enumerable = new List<object>();

			Assert.Throws<ArgumentNullException>(() => enumerable.Apply(null));
		}

		[Test]
		public void CanApplyToEmptyCollection()
		{
			var action = MockRepository.GenerateMock<Action<object>>();

			new List<object>().Apply(action);
			
			action.AssertWasNotCalled( a => a(Arg<object>.Is.Anything) );
		}

		[Test]
		public void CanApplyToSingleItem()
		{
			var action = MockRepository.GenerateMock<Action<object>>();

			var enumerable = new List<object>
				{
					new object()
				};

			action.Expect(a =>a(enumerable[0]));

			enumerable.Apply(action);

			action.VerifyAllExpectations();
		}

		[Test]
		public void CanApplyToMultipleItems()
		{
			var action = MockRepository.GenerateMock<Action<object>>();

			var enumerable = new List<object>
				{
					new object(),
					new object(),
					new object()
				};

			foreach (var item in enumerable)
			{
				var temporaryItem = item;
				action.Expect(a => a(temporaryItem));
			}

			enumerable.Apply(action);

			action.VerifyAllExpectations();
		}
	}
}