using System;

using NHibernate;

using StructureMap;

namespace AbstractAir.Persistence.Domain
{
	public class PersistenceFactory : IPersistenceFactory
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly IContainer _container;

		[CLSCompliant(false)]
		public PersistenceFactory(ISessionFactory sessionFactory, IContainer container)
		{
			_sessionFactory = ArgumentValidation.IsNotNull(sessionFactory, "sessionFactory");
			_container = ArgumentValidation.IsNotNull(container, "container");
		}

		public IPersistenceScope CreateScope()
		{
			return new PersistenceScope(_sessionFactory.OpenSession(), _container);
		}
	}
}