using System;
using System.Runtime.Remoting.Messaging;

using NHibernate;

namespace AbstractAir.Persistence.Domain
{
	public class CallContextSessionContextStrategy : ISessionContextStrategy
	{
		internal const string SessionCallContextKey = "AbstractAir.Persistence.Domain.Session";

		public void Store(ISession session)
		{
			ArgumentValidation.IsNotNull(session, "session");

			CallContext.SetData(SessionCallContextKey, session);
		}

		public ISession Retrieve()
		{
			return (ISession) CallContext.GetData(SessionCallContextKey);
		}

		public void Clear()
		{
			CallContext.FreeNamedDataSlot(SessionCallContextKey);
		}
	}
}