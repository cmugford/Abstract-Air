using System;

using MbUnit.Framework;

namespace AbstractAir.Tests
{
	public class IsNotNullValidationTestFixture
	{
		[Test]
		public void IsNotNullWithNotNullInstance()
		{
			var instance = new object();

			Assert.AreSame(instance, ArgumentValidation.IsNotNull(instance, "test"));
		}

		[Test]
		public void IsNotNullWithNullInstance()
		{
			const object testNull = null;
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsNotNull(testNull, "test"));
		}

		[Test]
		public void IsNotNullWithNullArgumentName()
		{
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsNotNull(new object(), null));
		}

		[Test]
		public void IsNotNullWithEmptyArgumentName()
		{
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsNotNull(new object(), string.Empty));
		}

		[Test]
		public void ReferenceIsNotNullWithNotNullInstance()
		{
			int? instance = 5;

			Assert.AreEqual(instance, ArgumentValidation.IsNotNull(instance, "test"));
		}

		[Test]
		public void ReferenceIsNotNullWithNullInstance()
		{
			int? testNull = null;
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsNotNull(testNull, "test"));
		}

		[Test]
		public void ReferenceIsNotNullWithNullArgumentName()
		{
			int? testNull = 2;
			Assert.Throws<ArgumentNullException>(() => ArgumentValidation.IsNotNull(testNull, null));
		}

		[Test]
		public void ReferenceIsNotNullWithEmptyArgumentName()
		{
			int? testNull = 2;
			Assert.Throws<ArgumentException>(() => ArgumentValidation.IsNotNull(testNull, string.Empty));
		}
	}
}