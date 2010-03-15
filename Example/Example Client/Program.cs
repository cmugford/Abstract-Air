using System;
using System.Globalization;

using AbstractAir.Examples.InternalMessages;

using NServiceBus;

using StructureMap;

namespace AbstractAir.Examples.ExampleClient
{
	public static class Program
	{
		public static void Main()
		{
			ConfigureNServiceBus();

			var bus = ObjectFactory.GetInstance<IBus>();

			var count = 0;
			while (Console.ReadLine() != "q")
			{
				var localCount = count++;
				bus.Send<ICreateProductMessage>(message =>
					{
						message.Name = string.Format(CultureInfo.CurrentCulture, "Product {0}", localCount);
						message.Category = string.Format(CultureInfo.CurrentCulture, "Product Category {0}", localCount);
					});
			}
		}

		private static void ConfigureNServiceBus()
		{
			Configure.With()
				.StructureMapBuilder()
				.XmlSerializer()
				.MsmqTransport()
					.IsTransactional(false)
					.PurgeOnStartup(false)
				.UnicastBus()
					.ImpersonateSender(false)
					.LoadMessageHandlers()
				.CreateBus()
				.Start();
		}
	}
}