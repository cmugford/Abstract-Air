using System;

using AbstractAir.Examples.Domain.Properties;
using AbstractAir.Persistence;

namespace AbstractAir.Examples.Domain
{
	public class Product : ICreateProduct, IRenameProduct
	{
		public virtual Guid Id { get; protected set; }
		public virtual int Version { get; protected set; }
		public virtual string Name { get; private set; }
		public virtual string Category { get; private set; }

		public virtual void Initialise(Guid productId, string productName, string productCategory)
		{
			if (Id != Guid.Empty)
			{
				throw new InvalidOperationException(Resources.IdAlreadAssignedError);
			}

			Id = productId;
			Name = ArgumentValidation.StringNotNullOrEmpty(productName, "productName");
			Category = ArgumentValidation.StringNotNullOrEmpty(productCategory, "productCategory");

			DomainEvents.Raise(new ProductCreatedEvent { Product = this });
		}

		public virtual void Rename(string productName)
		{
			Name = ArgumentValidation.StringNotNullOrEmpty(productName, "productName");

			DomainEvents.Raise(new ProductRenamedEvent { Product = this });
		}
	}
}