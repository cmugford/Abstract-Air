using System;

using AbstractAir.Persistence;

using MbUnit.Framework;

namespace AbstractAir.Examples.Domain.Tests
{
	public class RenameProductTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestNewName = "New Product Name";
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
		public void ProductSupportsRename()
		{
			_product.Initialise(_productId, TestName, TestCategory);

			_product.Rename(TestNewName);

			Assert.AreEqual(TestNewName, _product.Name);
		}

		[Test]
		public void RenameRaisesProductRenamedEvent()
		{
			Product renamed = null;

			DomainEvents.Register<ProductRenamedEvent>(c => renamed = c.Product);

			_product.Rename(TestNewName);

			Assert.AreSame(_product, renamed);
		}
	}
}