using System;
using System.Collections.Generic;
using System.Globalization;

using AbstractAir.Commands.Properties;

namespace AbstractAir.Commands
{
	public static class ValidationExtensions
	{
		public static void AssertNotNullOrWhitespace(this ICollection<ValidationError> validationErrors, string propertyName, string value)
		{
			if (value != null && value.Trim().Length != 0)
			{
				return;
			}

			validationErrors.Add(new ValidationError(propertyName,
				string.Format(CultureInfo.CurrentCulture, Resources.PropertyNullOrWhitespaceValidationErrorFormat, propertyName)));
		}

		public static void AssertNotDefault<TValue>(this ICollection<ValidationError> validationErrors, string propertyName, TValue value)
			where TValue : struct, IEquatable<TValue>
		{
			if (!value.Equals(default(TValue)))
			{
				return;
			}

			validationErrors.Add(new ValidationError(propertyName,
				string.Format(CultureInfo.CurrentCulture, Resources.PropertyIsDefaultValidationErrorFormat, propertyName, default(TValue))));
		}
	}
}
