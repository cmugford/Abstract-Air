using System;

using MbUnit.Framework;

namespace AbstractAir.Persistence.Tests
{
	public class PersistenceMessageModuleWithoutAmbientTransactionTestFixture : PersistenceMessageModuleTestFixtureBase
	{
		[SetUp]
		public override void Setup()
		{
			base.Setup();
		}

		[Test]
		public void HandleBeginMessageFailsWithoutAmbientTransaction()
		{
			Assert.Throws<InvalidOperationException>(() => PersistenceMessageModule.HandleBeginMessage());
		}
	}
}