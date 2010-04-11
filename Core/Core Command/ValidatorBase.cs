using System;
using System.Collections.Generic;

using NServiceBus;

namespace AbstractAir.Commands
{
	[CLSCompliant(false)]
	public abstract class ValidatorBase<TMessage> : IValidator<TMessage>
		where TMessage : IMessage
	{
		public IEnumerable<ValidationError> Validate(IMessage message)
		{
			return Validate((TMessage)message);
		}

		public abstract IEnumerable<ValidationError> Validate(TMessage message);
	}
}
