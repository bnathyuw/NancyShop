using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using NancyShop.Basket.Feature.Tests.Given_A_Basket;

namespace NancyShop.Basket.Feature.Tests
{
	static internal class ResponseExtensions
	{
		internal static string GetLocation(this BrowserResponse response)
		{
			var location = response.Headers["Location"];
			Assert.That(location, Is.Not.Null, "location");
			return location;
		}

		public static void AssertLinkValue(this BrowserResponse response, string rel, string expectedUrl)
		{
			var linksHeader = response.Headers["Links"];
			Assert.That(linksHeader, Is.Not.Null, rel + " links");
			var links = linksHeader.Split(',').Select(HttpLink.FromString).ToArray();
			Assert.That(links.Count(), Is.AtLeast(1), rel + " links.Count()");
			var basketLink = links.SingleOrDefault(l => l.Rel == rel);
			Assert.That(basketLink != null, rel + "link != null");
			Assert.That(basketLink.Url, Is.EqualTo(expectedUrl), rel + "link.Url");
		}

		public static void AssertLocation(this BrowserResponse response, string expectedPattern)
		{
			var location = GetLocation(response);
			Assert.That(location, Is.StringMatching(expectedPattern));
		}

		public static void AssertStatusCode(this BrowserResponse response, HttpStatusCode expectedStatusCode)
		{
			Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode), "StatusCode");
		}
	}
}