using System;

namespace AbstractAir.Queries
{
	public interface IQueryConfiguration
	{
		string DatabaseName { get; }
		string Server { get; }
		string ServerPort { get; }
		string Options { get; }
	}
}