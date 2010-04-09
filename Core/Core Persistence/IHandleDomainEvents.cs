using System;

namespace AbstractAir.Persistence
{
	public interface IHandleDomainEvents<TEvent>
		where TEvent : IDomainEvent
	{
		void Handle(TEvent instance);
	}
}