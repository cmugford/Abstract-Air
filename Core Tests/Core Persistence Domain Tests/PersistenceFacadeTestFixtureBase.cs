using NHibernate;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Domain.Tests
{
	public class PersistenceFacadeTestFixtureBase
	{
		public ISession Session { get; private set; }
		public PersistenceFacade PersistenceFacade { get; private set; }
		public IContainer Container { get; private set; }

		public virtual void Setup()
		{
			Session = MockRepository.GenerateStub<ISession>();
			Container = MockRepository.GenerateStub<IContainer>();

			PersistenceFacade = new PersistenceFacade(Container);
		}
	}
}