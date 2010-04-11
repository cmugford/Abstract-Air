using NServiceBus;

using Rhino.Mocks;

using StructureMap;

namespace AbstractAir.Portal.Tests
{
	public class CommandBusTestFixtureBase
	{
		public IBus Bus { get; private set; }
		public CommandBus CommandBus { get; private set; }
		public IContainer Container { get; private set; }
		public ITestCommandMessage TestCommandMessage { get; private set; }
		public TestController TestController { get; private set; }

		public virtual void Setup()
		{
			Bus = MockRepository.GenerateStub<IBus>();
			TestController = new TestController();
			Container = MockRepository.GenerateStub<IContainer>();
			TestCommandMessage = MockRepository.GenerateStub<ITestCommandMessage>();

			Bus.Stub(bus => bus.CreateInstance<ITestCommandMessage>()).Return(TestCommandMessage);

			CommandBus = new CommandBus(Bus, Container);
		}
	}
}