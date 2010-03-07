using System;

namespace AbstractAir.Examples.Domain
{
	public class Product : ICreateProduct
	{
		public virtual Guid Id { get; protected set; }
		public virtual int Version { get; protected set; }
		public virtual string Name { get; private set; }
		public virtual string Category { get; private set; }

		public virtual void AssignCoreDetails(string productName, string productCategory)
		{
			Name = ArgumentValidation.StringNotNullOrEmpty(productName, "productName");
			Category = ArgumentValidation.StringNotNullOrEmpty(productCategory, "productCategory");
		}

		public virtual void Rename(string productName)
		{
			Name = ArgumentValidation.StringNotNullOrEmpty(productName, "productName");
		}
	}
}