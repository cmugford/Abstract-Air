using System;
using System.Collections.Generic;

using AbstractAir.Commands;
using AbstractAir.Examples.InternalMessages;

namespace AbstractAir.Example.Validators
{
	public class CreateProductMessageValidator : ValidatorBase<ICreateProductMessage>
	{
		public override IEnumerable<ValidationError> Validate(ICreateProductMessage message)
		{
			ArgumentValidation.IsNotNull(message, "message");

			var validationErrors = new List<ValidationError>();

			validationErrors.AssertNotDefault("ProductId", message.ProductId);
			validationErrors.AssertNotNullOrWhitespace("Name", message.Name);
			validationErrors.AssertNotNullOrWhitespace("Category", message.Category);

			return validationErrors;
		}
	}
}