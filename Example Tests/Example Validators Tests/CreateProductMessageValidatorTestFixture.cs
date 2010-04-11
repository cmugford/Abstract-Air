using System;
using System.Linq;

using AbstractAir.Examples.InternalMessages;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Example.Validators.Tests
{
	public class CreateProductMessageValidatorTestFixture
	{
		private const string TestName = "Test Product";
		private const string TestCategory = "Test Category";

		private CreateProductMessageValidator _createProductMessageValidator;
		private ICreateProductMessage _createProductMessage;

		[SetUp]
		public void Setup()
		{
			_createProductMessageValidator = new CreateProductMessageValidator();
			_createProductMessage = MockRepository.GenerateStub<ICreateProductMessage>();
		}

		[Test]
		public void ValidMessageProducesNoValidationErrors()
		{
			ConfigureMessage(Guid.NewGuid(), TestName, TestCategory);

			Assert.IsEmpty(_createProductMessageValidator.Validate(_createProductMessage));
		}

		[Test]
		public void MessageWithoutProductIdReturnsValidationError()
		{
			ConfigureMessage(Guid.Empty, TestName, TestCategory);

			var validationErrors = _createProductMessageValidator.Validate(_createProductMessage);

			Assert.IsTrue(validationErrors.Any(error => error.PropertyName == "ProductId" && !string.IsNullOrEmpty(error.ErrorMessage)));
		}

		[Test]
		[Row(null)]
		[Row("")]
		[Row(" ")]
		public void MessageWithoutNameReturnsValidationError(string productName)
		{
			ConfigureMessage(Guid.NewGuid(), productName, TestCategory);

			var validationErrors = _createProductMessageValidator.Validate(_createProductMessage);

			Assert.IsTrue(validationErrors.Any(error => error.PropertyName == "Name" && !string.IsNullOrEmpty(error.ErrorMessage)));
		}

		[Test]
		[Row(null)]
		[Row("")]
		[Row(" ")]
		public void MessageWithoutCategoryReturnsValidationError(string category)
		{
			ConfigureMessage(Guid.NewGuid(), TestName, category);

			var validationErrors = _createProductMessageValidator.Validate(_createProductMessage);

			Assert.IsTrue(validationErrors.Any(error => error.PropertyName == "Category" && !string.IsNullOrEmpty(error.ErrorMessage)));
		}

		private void ConfigureMessage(Guid productId, string productName, string category)
		{
			_createProductMessage.ProductId = productId;
			_createProductMessage.Name = productName;
			_createProductMessage.Category = category;
		}
	}
}