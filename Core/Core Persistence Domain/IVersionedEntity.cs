using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IVersionedEntity : IEntity
	{
		int Version { get; }
	}
}