using System;

using AbstractAir.Persistence.Domain;

using MbUnit.Framework;

namespace AbstractAir.Examples.Domain.Tests
{
	public class CreateProductTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestCategory = "Product Category";

		private Guid _productId;
		private Product _product;

		[SetUp]
		public void Setup()
		{
			_productId = Guid.NewGuid();

			_product = new Product();
		}

		[Test]
		public void ProductSupportsAssignmentOfCoreDetails()
		{
			_product.Initialise(_productId, TestName, TestCategory);

			Assert.AreEqual(TestName, _product.Name);
		}

		[Test]
		public void CannotAssignDetailsWhenIdAlreadyAssigned()
		{
			_product.Initialise(_productId, TestName, TestCategory);
			Assert.Throws<InvalidOperationException>(() =>  _product.Initialise(_productId, TestName, TestCategory));
		}

		[Test]
		public void ProductCreationRaisedProductCreatedEvent()
		{
			Product created = null;

			DomainEvents.Register<ProductCreatedEvent>(c => created = c.Product);

			_product.Initialise(_productId, TestName, TestCategory);

			Assert.AreSame(_product, created);
		}
	}
}