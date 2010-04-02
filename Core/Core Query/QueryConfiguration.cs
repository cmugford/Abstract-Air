using System;
using System.Configuration;

namespace AbstractAir.Queries
{
	public class QueryConfiguration : ConfigurationSection, IQueryConfiguration
	{
		private const string DatabaseNameKey = "database";
		private const string ServerKey = "server";
		private const string ServerPortKey = "serverPort";
		private const string OptionsKey = "options";

		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection
			{
				new ConfigurationProperty(DatabaseNameKey,
					typeof(string),
					null,
					ConfigurationPropertyOptions.IsRequired),
				new ConfigurationProperty(ServerKey,
					typeof(string),
					null,
					ConfigurationPropertyOptions.None),
				new ConfigurationProperty(ServerPortKey,
					typeof(string),
					null,
					ConfigurationPropertyOptions.None),
				new ConfigurationProperty(OptionsKey,
					typeof(string),
					null,
					ConfigurationPropertyOptions.IsRequired),
			};

		protected override ConfigurationPropertyCollection Properties
		{
			get { return _properties; }
		}

		public string DatabaseName
		{
			get { return (string)this[DatabaseNameKey]; }
			set { this[DatabaseNameKey] = value; }
		}

		public string Server
		{
			get { return (string)this[ServerKey]; }
			set { this[ServerKey] = value; }
		}

		public string ServerPort
		{
			get { return (string)this[ServerPortKey]; }
			set { this[ServerPortKey] = value; }
		}

		public string Options
		{
			get { return (string)this[OptionsKey]; }
			set { this[OptionsKey] = value; }
		}
	}
}