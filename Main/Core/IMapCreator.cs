using System;

using AutoMapper;

namespace AbstractAir
{
	[CLSCompliant(false)]
	public interface IMapCreator
	{
		void CreateMaps(IConfiguration configuration);
	}
}