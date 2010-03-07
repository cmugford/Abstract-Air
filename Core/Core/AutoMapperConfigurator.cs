using System;

using AutoMapper;

namespace AbstractAir
{
	public class AutoMapperConfigurator : IAutoMapperConfigurator
	{
		private readonly IConfiguration _configuration;
		private readonly IMapCreator[] _mapCreators;

		[CLSCompliant(false)]
		public AutoMapperConfigurator(IConfiguration configuration, IMapCreator[] mapCreators)
		{
			_configuration = ArgumentValidation.IsNotNull(configuration, "configuration");
			_mapCreators = ArgumentValidation.IsNotNull(mapCreators, "mapCreators");
		}

		public void ConfigureAutoMapping()
		{
			foreach (var mapCreator in _mapCreators)
			{
				mapCreator.CreateMaps(_configuration);
			}
		}
	}
}