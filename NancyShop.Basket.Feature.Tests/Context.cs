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
		public static Browser Browser { get; set; }

		[SetUp]
		public void SetUp()
		{
			var basketsModule = new BasketsModule();
			Bootstrapper = new DefaultNancyBootstrapper();
			Browser = new Browser(Bootstrapper);
		}
	}
}