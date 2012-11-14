using NUnit.Framework;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests
{
	[SetUpFixture]
	public class Context
	{
		static INancyBootstrapper Bootstrapper { get; set; }
		public static Browser Browser { get; private set; }

		[SetUp]
		public void SetUp()
		{
			var basketsModule = new BasketModule();
			Bootstrapper = new DefaultNancyBootstrapper();
			Browser = new Browser(Bootstrapper);
		}

		public const string NancyShopBasketJsonContentType = "application/vnd.nancyshop.basket+json";
	}
}