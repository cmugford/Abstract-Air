using System;

namespace AbstractAir.TestDomainObjects
{
	public class VersionedTestObject : IVersionedTestObject
	{
		public virtual Guid Id { get; set; }
		public virtual int Version { get; set; }
		public virtual string Name { get; set; }
	}
}
