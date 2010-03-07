using System;

using NServiceBus;

namespace AbstractAir.Examples.ExampleClient
{
	public static class Program
	{
		public static void Main()
		{
			ConfigureNServiceBus();
		}

		private static void ConfigureNServiceBus()
		{
			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer()
				.UnicastBus()
				.CreateBus()
				.Start();
		}
	}
}