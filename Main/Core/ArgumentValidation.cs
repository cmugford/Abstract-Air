using System;
using System.Globalization;

namespace AbstractAir
{
	public static class ArgumentValidation
	{
		public static TValidate IsNotNull<TValidate>(TValidate instance, string argumentName)
			where TValidate : class
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");

			if (instance == null)
			{
				throw new ArgumentNullException(argumentName);
			}

			return instance;
		}

		public static TValidate? IsNotNull<TValidate>(TValidate? instance, string argumentName)
			where TValidate : struct
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");

			if (!instance.HasValue)
			{
				throw new ArgumentNullException(argumentName);
			}

			return instance;
		}

		public static int IsGreaterThanZero(int value, string argumentName)
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");

			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					value,
					string.Format(CultureInfo.InvariantCulture,
						Properties.Resources.ArgumentMustBeGreaterThanZeroFormat,
						argumentName));
			}

			return value;
		}

		public static int IsGreaterThanZero(int value, string argumentName, string message)
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");
			StringNotNullOrEmptyInternal(message, "message");

			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(argumentName, value, message);
			}

			return value;
		}

		public static int IsGreaterThanZero(int value, string argumentName, Func<string> message)
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");
			IsNotNull(message, "message");

			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(argumentName, value, message());
			}

			return value;
		}

		public static int IsGreaterThanOrEqualToZero(int value, string argumentName)
		{
			StringNotNullOrEmptyInternal(argumentName, "argumentName");

			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(argumentName,
					value,
					string.Format(CultureInfo.InvariantCulture,
						Properties.Resources.ArgumentMustBeGreaterThanOrEqualToZeroFormat,
						argumentName));
			}

			return value;
		}

		public static string StringNotNullOrEmpty(string value, string name)
		{
			StringNotNullOrEmptyInternal(name, "name");
			StringNotNullOrEmptyInternal(value, name);

			return value;
		}

		private static void StringNotNullOrEmptyInternal(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
					Properties.Resources.StringArgumentEmptyFormat,
					name));
			}
		}
	}
}