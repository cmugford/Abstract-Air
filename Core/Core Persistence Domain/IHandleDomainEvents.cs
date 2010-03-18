using System;

namespace AbstractAir.Persistence.Domain
{
	public interface IHandleDomainEvents<TEvent>
		where TEvent : IDomainEvent
	{
		void Handle(TEvent instance);
	}
}