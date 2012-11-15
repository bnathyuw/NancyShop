using System;
using NUnit.Framework;

namespace NancyShop.Basket.Feature.Tests.Given_A_Basket
{
	public class HttpLink
	{
		public string Url { get; set; }
		public string Rel { get; set; }

		public static HttpLink FromString(string linkString)
		{
			Assert.That(linkString, Is.Not.Null.Or.Empty, "linkString");
			var parts = linkString.Split(new[] {"; rel="}, StringSplitOptions.RemoveEmptyEntries);
			var url = parts[0];
			var rel = parts.Length > 1 ? parts[1] : null;
			return new HttpLink {Url = url, Rel = rel};
		}
	}
}