using System;

namespace AbstractAir.TestDomainObjects
{
	public class TestObject : ITestObject
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
	}
}