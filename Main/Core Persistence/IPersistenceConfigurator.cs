using System;
using System.Collections.Generic;
using System.Reflection;

using NHibernate.Cfg;

namespace AbstractAir.Persistence
{
	public interface IPersistenceConfigurator
	{
		Configuration Configuration { get; }

		void ConfigurePersistence(IEnumerable<Assembly> assemblies);
	}
}