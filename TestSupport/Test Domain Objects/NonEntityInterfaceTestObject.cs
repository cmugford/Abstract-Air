using System;

namespace AbstractAir.TestDomainObjects
{
	public class NonEntityInterfaceTestObject : IEntityInterfaceForNonEntityTestCase, INonEntityInterface
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
	}
}