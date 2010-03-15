using System;
using System.Configuration;

namespace AbstractAir.Persistence
{
	public class PersistenceConfiguration : ConfigurationSection, IPersistenceConfiguration
	{
		private const string ConnectionStringKey = "connectionString";

		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection
			{
				new ConfigurationProperty(ConnectionStringKey,
					typeof(string),
					null,
					ConfigurationPropertyOptions.None),
			};

		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}

		public string ConnectionString
		{
			get { return (string)this[ConnectionStringKey]; }
			set { this[ConnectionStringKey] = value; }
		}
	}
}