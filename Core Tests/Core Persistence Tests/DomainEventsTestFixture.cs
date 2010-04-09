using System;
using System.Collections.Generic;

using MbUnit.Framework;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Persistence.Tests
{
	public class DomainEventsTestFixture
	{
		private ITestDomainEvent _testDomainEvent;

		[SetUp]
		public void Setup()
		{
			_testDomainEvent = MockRepository.GenerateStub<ITestDomainEvent>();

			DomainEvents.ClearCallbacks();
		}

		[Test]
		public void RegisteredCallbackInvokedOnRaise()
		{
			var callback = MockRepository.GenerateStub<Action<ITestDomainEvent>>();

			DomainEvents.Register(callback);

			DomainEvents.Raise(_testDomainEvent);

			callback.AssertWasCalled(c => c(_testDomainEvent));
		}

		[Test]
		public void RegisteredCallbackNotInvokedOnRaiseAfterClear()
		{
			var callback = MockRepository.GenerateStub<Action<ITestDomainEvent>>();

			DomainEvents.Register(callback);

			DomainEvents.ClearCallbacks();

			DomainEvents.Raise(_testDomainEvent);

			callback.AssertWasNotCalled(c => c(Arg<ITestDomainEvent>.Is.Anything));
		}

		[Test]
		public void RegisteredCallbackNotInvokedOnRaiseWhenTypeDoesntMatch()
		{
			var callback = MockRepository.GenerateStub<Action<IAlternateDomainEvent>>();

			DomainEvents.Register(callback);

			DomainEvents.Raise(_testDomainEvent);

			callback.AssertWasNotCalled(c => c(Arg<IAlternateDomainEvent>.Is.Anything));
		}

		[Test]
		public void HandlersFromContainerInvokedWhenContainerRegistered()
		{
			var container = MockRepository.GenerateStub<IContainer>();
			var testHandler = MockRepository.GenerateStub<IHandleDomainEvents<ITestDomainEvent>>();
			var handlers = new List<IHandleDomainEvents<ITestDomainEvent>> {testHandler};

			container.Stub(c => c.GetAllInstances<IHandleDomainEvents<ITestDomainEvent>>()).Return(handlers);

			DomainEvents.Container = container;

			DomainEvents.Raise(_testDomainEvent);

			testHandler.AssertWasCalled(handler => handler.Handle(_testDomainEvent));
		}
	}
}