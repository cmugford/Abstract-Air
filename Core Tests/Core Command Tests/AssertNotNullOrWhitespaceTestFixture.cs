using System;
using System.Linq;

using MbUnit.Framework;

namespace AbstractAir.Commands.Tests
{
	public class AssertNotNullOrWhitespaceTestFixture : ValidationExtensionsTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		[Row(null, true)]
		[Row("", true)]
		[Row(" ", true)]
		[Row("Blah", false)]
		[Row(" Blah", false)]
		[Row(" Blah ", false)]
		[Row("Blah ", false)]
		public void ValidationErrorAddedWhenRelevant(string value, bool response)
		{
			ValidationErrors.AssertNotNullOrWhitespace(PropertyName, value);

			Assert.AreEqual(response, ValidationErrors.Count == 1);
		}

		[Test]
		public void PropertyNameSetOnError()
		{
			ValidationErrors.AssertNotNullOrWhitespace(PropertyName, null);

			Assert.IsTrue(ValidationErrors.Any(error => error.PropertyName == PropertyName));
		}

		[Test]
		public void ErrorMessageContainsPropertyName()
		{
			ValidationErrors.AssertNotNullOrWhitespace(PropertyName, null);
			
			Assert.IsTrue(ValidationErrors.Any(error => error.ErrorMessage.IndexOf(PropertyName) != -1));
		}
	}
}