using System;

using Norm;
using Norm.Linq;

using StructureMap;

namespace AbstractAir.Queries
{
	public class QueryConfigurator : IQueryConfigurator
	{
		private readonly IQueryConfiguration _queryConfiguration;

		public QueryConfigurator(IQueryConfiguration queryConfiguration)
		{
			_queryConfiguration = ArgumentValidation.IsNotNull(queryConfiguration, "queryConfiguration");
		}

		public void ConfigureQuerying()
		{
			var mongoQueryProvider = new MongoQueryProvider(_queryConfiguration.DatabaseName,
				_queryConfiguration.Server,
				_queryConfiguration.ServerPort,
				_queryConfiguration.Options);

			ObjectFactory.Configure(configure =>
				{
					configure.For<IMongoQueryProvider>().Use(mongoQueryProvider);
					configure.For<IMongoDatabase>().Use(mongoQueryProvider.Db);
				});
		}
	}
}