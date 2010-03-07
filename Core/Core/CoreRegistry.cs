using System;
using System.Collections.Generic;

using AutoMapper;
using AutoMapper.Mappers;

using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace AbstractAir
{
	[CLSCompliant(false)]
	public class CoreRegistry : Registry
	{
		public CoreRegistry()
		{
			Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

			For<ITypeMapFactory>().LifecycleIs(new SingletonLifecycle()).Use<TypeMapFactory>();

			For<Configuration>()
				.LifecycleIs(new SingletonLifecycle())
				.Use<Configuration>()
				.Ctor<IEnumerable<IObjectMapper>>().Is(expression => expression.ConstructedBy(MapperRegistry.AllMappers));

			For<IConfigurationProvider>().Use(context => context.GetInstance<Configuration>());

			For<IConfiguration>().Use(context => context.GetInstance<Configuration>());

			For<IMappingEngine>().Use<MappingEngine>();
		}
	}
}