using System;

using MbUnit.Framework;

namespace AbstractAir.Tests
{
	public class StringHelpersTestFixture
	{
		[Test]
		[Row(null, true)]
		[Row("", true)]
		[Row(" ", true)]
		[Row("  ", true)]
		[Row("\t", true)]
		[Row("\n", true)]
		[Row("valid", false)]
		[Row("valid string", false)]
		public void CanDetermineIfStringNullOrWhitespace(string value, bool isNullOrWhitespace)
		{
			Assert.AreEqual(isNullOrWhitespace, StringHelpers.IsNullOrWhitespace(value));
		}
	}
}