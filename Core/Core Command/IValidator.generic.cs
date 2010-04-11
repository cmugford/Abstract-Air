using System;
using System.Collections.Generic;

using NServiceBus;

namespace AbstractAir.Commands
{
	[CLSCompliant(false)]
	public interface IValidator<TMessage> : IValidator
		where TMessage : IMessage
	{
		IEnumerable<ValidationError> Validate(TMessage message);
	}
}