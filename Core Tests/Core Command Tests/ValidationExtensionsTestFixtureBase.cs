using System.Collections.Generic;

namespace AbstractAir.Commands.Tests
{
	public class ValidationExtensionsTestFixtureBase
	{
		protected const string PropertyName = "TestProperty";

		public List<ValidationError> ValidationErrors { get; private set; }

		public virtual void Setup()
		{
			ValidationErrors = new List<ValidationError>();
		}
	}
}