using System;
using System.Collections.Generic;

using NServiceBus;

namespace AbstractAir.Commands
{
	[CLSCompliant(false)]
	public interface IValidator
	{
		IEnumerable<ValidationError> Validate(IMessage message);
	}
}