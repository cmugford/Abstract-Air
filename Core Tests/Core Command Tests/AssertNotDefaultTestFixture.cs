using System;
using System.Linq;

using MbUnit.Framework;

namespace AbstractAir.Commands.Tests
{
	public class AssertNotDefaultTestFixture : ValidationExtensionsTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		[Row(default(int), true)]
		[Row(1, false)]
		[Row(-1, false)]
		[Row(int.MaxValue, false)]
		[Row(int.MinValue, false)]
		public void ValidationErrorAddedWhenNotDefaultInt32(int value, bool response)
		{
			ValidationErrors.AssertNotDefault(PropertyName, value);

			Assert.AreEqual(response, ValidationErrors.Count == 1);
		}

		[Test]
		public void PropertyNameSetOnError()
		{
			ValidationErrors.AssertNotDefault(PropertyName, default(int));

			Assert.IsTrue(ValidationErrors.Any(error => error.PropertyName == PropertyName));
		}

		[Test]
		public void ErrorMessageContainsPropertyName()
		{
			ValidationErrors.AssertNotDefault(PropertyName, default(int));

			Assert.IsTrue(ValidationErrors.Any(error => error.ErrorMessage.IndexOf(PropertyName) != -1));
		}
	}
}