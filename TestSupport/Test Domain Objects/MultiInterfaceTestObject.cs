using System;

namespace AbstractAir.TestDomainObjects
{
	public class MultiInterfaceTestObject : IFirstTestInterface, ISecondTestInterface, IThirdTestInterface
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
	}
}