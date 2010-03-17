using System;

using MbUnit.Framework;

namespace AbstractAir.Examples.Domain.Tests
{
	public class ProductTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestNewName = "New Product Name";
		private const string TestCategory = "Product Category";

		private Guid _productId;

		[SetUp]
		public void Setup()
		{
			_productId = Guid.NewGuid();
		}

		[Test]
		public void ProductSupportsAssignmentOfCoreDetails()
		{
			var product = new Product();

			product.AssignCoreDetails(_productId, TestName, TestCategory);

			Assert.AreEqual(TestName, product.Name);
		}

		[Test]
		public void CannotAssignDetailsWhenIdAlreadyAssigned()
		{
			var product = new Product();

			product.AssignCoreDetails(_productId, TestName, TestCategory);
			Assert.Throws<InvalidOperationException>(() =>  product.AssignCoreDetails(_productId, TestName, TestCategory));
		}

		[Test]
		public void ProductSupportsRename()
		{
			var product = new Product();
			product.AssignCoreDetails(_productId, TestName, TestCategory);

			product.Rename(TestNewName);

			Assert.AreEqual(TestNewName, product.Name);
		}
	}
}