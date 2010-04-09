using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace AbstractAir.Persistence
{
	public static class DomainEvents
	{
		[ThreadStatic]
		private static List<Delegate> _actions;

		[CLSCompliant(false)]
		public static IContainer Container { get; set; }

		public static void Register<TEvent>(Action<TEvent> callback)
			where TEvent : IDomainEvent
		{
			ArgumentValidation.IsNotNull(callback, "callback");

			if (_actions == null)
			{
				_actions = new List<Delegate>();
			}

			_actions.Add(callback);
		}

		public static void ClearCallbacks()
		{
			_actions = null;
		}

		public static void Raise<TEvent>(TEvent instance)
			where TEvent : IDomainEvent
		{
			if (Container != null)
			{
				Container.GetAllInstances<IHandleDomainEvents<TEvent>>()
					.Apply(handler => handler.Handle(instance));
			}

			if (_actions != null)
			{
				_actions.Select(action => action as Action<TEvent>)
					.Where(action => action != null)
					.Apply(action => action(instance));
			}
		}
	}
}