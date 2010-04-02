using System;
using System.Configuration;

using AbstractAir.Queries;

using log4net.Config;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Examples.QueryUpdateService
{
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
	{
		public void Init()
		{
			ConfigureStructureMap();

			ObjectFactory.GetInstance<IQueryConfigurator>().ConfigureQuerying();

			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer();

			SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
		}

		private static void ConfigureStructureMap()
		{
			ObjectFactory.Configure(configure =>
				{
					configure.AddRegistry<CoreRegistry>();
					configure.AddRegistry<QueryRegistry>();

					configure.For<IQueryConfiguration>().Use((IQueryConfiguration) ConfigurationManager.GetSection("queries"));
				});
		}
	}
}