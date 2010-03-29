using System;

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

			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer();

			SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
		}

		private static void ConfigureStructureMap()
		{
			ObjectFactory.Configure(configure => configure.AddRegistry<CoreRegistry>());
		}
	}
}