using System;
using System.Collections.Generic;

namespace AbstractAir.Utilities.DatabaseGenerator
{
	public class DomainInstance
	{
		public String ConnectionString { get; set; }
		public IList<string> Assemblies { get; set; }
	}
}