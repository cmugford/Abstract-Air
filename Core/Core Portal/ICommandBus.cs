using System;
using System.Web.Mvc;

using NServiceBus;

namespace AbstractAir.Portal
{
	[CLSCompliant(false)]
	public interface ICommandBus
	{
		bool Send<TCommand>(Controller controller, Action<TCommand> commandConstructor)
			where TCommand : IMessage;
	}
}