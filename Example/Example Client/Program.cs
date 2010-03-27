using System;
using System.Globalization;

using AbstractAir.Examples.InternalMessages;

using log4net.Config;

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
				var productId = Guid.NewGuid();
				var localCount = count++;

				bus.Send<ICreateProductMessage>(message =>
					{
						message.ProductId = productId;
						message.Name = string.Format(CultureInfo.CurrentCulture, "Product {0} {1:G}", localCount, DateTime.UtcNow);
						message.Category = string.Format(CultureInfo.CurrentCulture, "Product Category {0}", localCount);
					});

				bus.Send<IRenameProductMessage>(message =>
					{
						message.ProductId = productId;
						message.Name = string.Format(CultureInfo.CurrentCulture, "Rename Product {0} {1:G}", localCount, DateTime.UtcNow);
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

			SetLoggingLibrary.Log4Net(XmlConfigurator.Configure);
		}
	}
}