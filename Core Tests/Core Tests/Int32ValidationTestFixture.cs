using System;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Tests
{
	public class Int32ValidationTestFixture
	{
		[Test]
		public void IntegerGreaterThanZeroValidGreaterThanZero()
		{
			Assert.AreEqual(Int32.MaxValue, ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument"));
		}

		[Test]
		public void IntegerGreaterThanZeroInvalidZero()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(0, "argument"));
		}

		[Test]
		public void IntegerGreaterThanZeroInvalidNegative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MinValue, "argument"));
		}

		[Test]
		public void IntegerGreaterThanZeroNullArgumentName()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, null));
		}

		[Test]
		public void IntegerGreaterThanZeroEmptyArgumentName()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, string.Empty));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageValidGreaterThanZero()
		{
			Assert.AreEqual(Int32.MaxValue, ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument", "Test Message"));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageInvalidZero()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(0, "argument", "Test Message"));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageInvalidNegative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MinValue, "argument", "Test Message"));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageNullArgumentName()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, null, "Test Message"));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageEmptyArgumentName()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, string.Empty, "Test Message"));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageNullMessage()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument", (string) null));
		}

		[Test]
		public void IntegerGreaterThanZeroWithMessageEmptyMessage()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument", string.Empty));
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaValidGreaterThanZero()
		{
			var messageGenerator = MockRepository.GenerateMock<Func<string>>();

			Assert.AreEqual(Int32.MaxValue,
				ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument", messageGenerator),
				"Returned value does not match passed value");

			messageGenerator.VerifyAllExpectations();
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaInvalidZero()
		{
			var messageGenerator = MockRepository.GenerateMock<Func<string>>();

			messageGenerator.Expect(generator => generator()).Return("Test Message");

			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(0, "argument", messageGenerator));

			messageGenerator.VerifyAllExpectations();
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaInvalidNegative()
		{
			var messageGenerator = MockRepository.GenerateMock<Func<string>>();

			messageGenerator.Expect(generator => generator()).Return("Test Message");

			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MinValue, "argument", messageGenerator));

			messageGenerator.VerifyAllExpectations();
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaNullArgumentName()
		{
			var messageGenerator = MockRepository.GenerateMock<Func<string>>();

			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, null, messageGenerator));

			messageGenerator.VerifyAllExpectations();
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaEmptyArgumentName()
		{
			var messageGenerator = MockRepository.GenerateMock<Func<string>>();

			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, string.Empty, messageGenerator));

			messageGenerator.VerifyAllExpectations();
		}

		[Test]
		public void IntegerGreaterThanZeroWithLambdaNullLambda()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanZero(Int32.MaxValue, "argument", (Func<string>) null));
		}

		[Test]
		public void IntegerGreaterThanOrEqualToZeroValidGreaterThanZero()
		{
			Assert.AreEqual(Int32.MaxValue, ArgumentValidation.IsGreaterThanOrEqualToZero(Int32.MaxValue, "argument"));
		}

		[Test]
		public void IntegerGreaterThanOrEqualToZeroValidZero()
		{
			Assert.AreEqual(0, ArgumentValidation.IsGreaterThanOrEqualToZero(0, "argument"));
		}

		[Test]
		public void IntegerGreaterThanOrEqualToZeroInvalidNegative()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => ArgumentValidation.IsGreaterThanOrEqualToZero(Int32.MinValue, "argument"));
		}

		[Test]
		public void IntegerGreaterThanOrEqualToZeroNullArgumentName()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsGreaterThanOrEqualToZero(Int32.MaxValue, null));
		}

		[Test]
		public void IntegerGreaterThanOrEqualToZeroEmptyArgumentName()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsGreaterThanOrEqualToZero(Int32.MaxValue, string.Empty));
		}
	}
}