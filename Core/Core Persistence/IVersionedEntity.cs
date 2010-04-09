using System;

namespace AbstractAir.Persistence
{
	public interface IVersionedEntity : IEntity
	{
		int Version { get; }
	}
}