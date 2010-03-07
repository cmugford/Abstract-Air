using System;

using MbUnit.Framework;

namespace AbstractAir.Examples.Domain.Tests
{
	[TestFixture]
	public class ProductTestFixture
	{
		private const string TestName = "Product Name";
		private const string TestNewName = "New Product Name";
		private const string TestCategory = "Product Category";

		[Test]
		public void ProductSupportsAssignmentOfCoreDEtails()
		{
			var product = new Product();

			product.AssignCoreDetails(TestName, TestCategory);

			Assert.AreEqual(TestName, product.Name);
		}

		[Test]
		public void ProductSupportsRename()
		{
			var product = new Product();
			product.AssignCoreDetails(TestName, TestCategory);

			product.Rename(TestNewName);

			Assert.AreEqual(TestNewName, product.Name);
		}
	}
}