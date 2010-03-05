using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IPersistenceFactory
	{
		IPersistenceScope CreateScope();
	}
}