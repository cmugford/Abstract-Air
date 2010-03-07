using System;

namespace AbstractAir
{
	public static class ActionExtensions
	{
		public static void Times(this int numberOfTimes, Action action)
		{
			ArgumentValidation.IsGreaterThanOrEqualToZero(numberOfTimes, "numberOfTimes");

			for (var number = 0; number < numberOfTimes; number++)
			{
				action();
			}
		}

		public static void Times(this int numberOfTimes, Action<int> action)
		{
			ArgumentValidation.IsGreaterThanOrEqualToZero(numberOfTimes, "numberOfTimes");

			for (var number = 1; number <= numberOfTimes; number++)
			{
				action(number);
			}
		}
	}
}