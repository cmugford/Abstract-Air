using System;

using AutoMapper;

using MbUnit.Framework;

using Rhino.Mocks;

namespace AbstractAir.Tests
{
	public class AutoMapperConfiguratorTestFixture
	{
		private IConfiguration _configuration;

		[SetUp]
		public void Setup()
		{
			_configuration = MockRepository.GenerateStub<IConfiguration>();
		}

		[Test]
		public void ConfiguratorInvokesRegisterOnSingleRegisteredRegistry()
		{
			var mapCreator = MockRepository.GenerateMock<IMapCreator>();

			mapCreator.Expect(registry => registry.CreateMaps(_configuration));

			new AutoMapperConfigurator(_configuration, new[] {mapCreator}).ConfigureAutoMapping();

			mapCreator.VerifyAllExpectations();
		}

		[Test]
		public void ConfiguratorInvokesRegisterOnMultipleRegisteredRegistry()
		{
			var mapCreators = new[]
				{
					MockRepository.GenerateMock<IMapCreator>(), MockRepository.GenerateMock<IMapCreator>(), MockRepository.GenerateMock<IMapCreator>()
				};

			mapCreators.Apply(creator => creator.Expect(registry => registry.CreateMaps(_configuration)));

			new AutoMapperConfigurator(_configuration, mapCreators).ConfigureAutoMapping();

			mapCreators.Apply(mapCreator => mapCreator.VerifyAllExpectations());
		}
	}
}