using System;

namespace AbstractAir
{
	public static class StringHelpers
	{
		public static bool IsNullOrWhitespace(string value)
		{
			return value == null || value.Trim().Length == 0;
		}
	}
}