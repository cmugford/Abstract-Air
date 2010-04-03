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
		private const int NumberOfMessages = 1000000;

		public static void Main()
		{
			Console.WriteLine("Press any key to start. Make sure the other services are started.");
			Console.ReadKey();

			ConfigureNServiceBus();

			var bus = ObjectFactory.GetInstance<IBus>();

			for (var count = 0; count < NumberOfMessages; count++)
			{
				var productId = Guid.NewGuid();
				var localCount = count;

				bus.Send<ICreateProductMessage>(message =>
					{
						message.ProductId = productId;
						message.Name = string.Format(CultureInfo.CurrentCulture, "Product {0} {1:G}", localCount, DateTime.UtcNow);
						message.Category = string.Format(CultureInfo.CurrentCulture, "Product Category {0}", localCount);
					});

				//bus.Send<IRenameProductMessage>(message =>
				//    {
				//        message.ProductId = productId;
				//        message.Name = string.Format(CultureInfo.CurrentCulture, "Rename Product {0} {1:G}", localCount, DateTime.UtcNow);
				//    });

				if (count % 100 == 0)
				{
					Console.Write(".");
				}
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