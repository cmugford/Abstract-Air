using System;
using System.Collections.Generic;

namespace AbstractAir
{
	public static class SetOperations
	{
		public static void Apply<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			ArgumentValidation.IsNotNull(source, "source");
			ArgumentValidation.IsNotNull(action, "action");

			foreach (var item in source)
			{
				action(item);
			}
		}
	}
}