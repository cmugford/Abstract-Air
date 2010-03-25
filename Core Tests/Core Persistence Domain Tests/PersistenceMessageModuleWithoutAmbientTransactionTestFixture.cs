using System;

using MbUnit.Framework;

namespace AbstractAir.Persistence.Domain.Tests
{
	[TestFixture]
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