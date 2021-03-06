﻿using System;
using System.Web.Mvc;

using StructureMap.Configuration.DSL;

namespace AbstractAir.Example.Web.StandAlone
{
	public class StandaloneRegistry : Registry
	{
		public StandaloneRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.AddAllTypesOf<IController>().NameBy(type => type.Name.Replace("Controller", ""));
				});
		}
	}
}