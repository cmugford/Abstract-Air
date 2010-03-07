using System;

namespace AbstractAir.Persistence
{
	public interface IPersistenceConfiguration
	{
		string ConnectionString { get; }
	}
}