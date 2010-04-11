using System;

namespace AbstractAir.Commands
{
	public class ValidationError
	{
		private readonly string _errorMessage;
		private readonly string _propertyName;

		public ValidationError(string propertyName, string errorMessage)
		{
			_propertyName = ArgumentValidation.StringNotNullOrEmpty(propertyName, "propertyName");
			_errorMessage = ArgumentValidation.StringNotNullOrEmpty(errorMessage, "errorMessage");
		}

		public string PropertyName
		{
			get { return _propertyName; }
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
		}
	}
}