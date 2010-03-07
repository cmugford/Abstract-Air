using System;

using MbUnit.Framework;

namespace AbstractAir.Tests
{
	public class StringNotNullOrEmptyValidationTestFixture
	{
		[Test]
		public void StringNotNullOrEmptyValid()
		{
			Assert.AreEqual("Test", ArgumentValidation.StringNotNullOrEmpty("Test", "Test"), "Did not return argument value");
		}

		[Test]
		public void StringNotNullOrEmptyNullValue()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.StringNotNullOrEmpty(null, "Test"));
		}

		[Test]
		public void StringNotNullOrEmptyEmptyValue()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.StringNotNullOrEmpty(string.Empty, "Test"));
		}

		[Test]
		public void StringNotNullOrEmptyNullArgumentName()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.StringNotNullOrEmpty("Test", null));
		}

		[Test]
		public void StringNotNullOrEmptyEmptyArgumentName()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.StringNotNullOrEmpty("Test", string.Empty));
		}
	}
}