using System;

using AbstractAir.Persistence;

namespace AbstractAir.Utilities.DatabaseGenerator
{
	public class UpdatablePersistenceConfiguration : IPersistenceConfiguration
	{
		public string ConnectionString { get; set; }
	}
}