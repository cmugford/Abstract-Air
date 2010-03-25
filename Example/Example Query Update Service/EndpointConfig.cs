using System;

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
				.XmlSerializer()
				.MsmqSubscriptionStorage();
		}

		private static void ConfigureStructureMap()
		{
			ObjectFactory.Configure(configure => configure.AddRegistry<CoreRegistry>());
		}
	}
}