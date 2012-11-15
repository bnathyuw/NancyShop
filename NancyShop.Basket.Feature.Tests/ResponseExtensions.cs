using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests
{
	static internal class ResponseExtensions
	{
		internal static string GetLocation(this BrowserResponse postBasket)
		{
			return postBasket.Headers["Location"];
		}
	}
}