using System;

using NHibernate;

namespace AbstractAir.Persistence
{
	public class ThreadStaticSessionContextStrategy : ISessionContextStrategy
	{
		[ThreadStatic]
		private ISession _session;
		private readonly Guid _contextId;

		public ThreadStaticSessionContextStrategy()
		{
			_contextId = Guid.NewGuid();
		}

		public Guid ContextId
		{
			get { return _contextId; }
		}

		public void Store(ISession session)
		{
			_session = ArgumentValidation.IsNotNull(session, "session");
		}

		public ISession Retrieve()
		{
			return _session;
		}

		public void Clear()
		{
			_session = null;
		}
	}
}