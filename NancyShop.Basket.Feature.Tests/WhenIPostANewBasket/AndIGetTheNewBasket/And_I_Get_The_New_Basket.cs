using NUnit.Framework;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.WhenIPostANewBasket.AndIGetTheNewBasket
{
	[SetUpFixture]
	public class And_I_Get_The_New_Basket
	{
		public static BrowserResponse Response { get; private set; }

		[SetUp]
		public void SetUp()
		{
			var location = When_I_Post_A_New_Basket.Response.Headers["Location"];

			Response =  Given_A_Browser.Browser.Get(location, with => with.Accept("application/vnd.nancyshop+json"));
		}
	}
}