using System.Collections.Generic;
using NUnit.Framework;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.WhenIPostANewBasket
{
	[SetUpFixture]
	public class When_I_Post_A_New_Basket
	{
		public static BrowserResponse Response { get; private set; }

		[SetUp]
		public void SetUp()
		{
			Response = Given_A_Browser.Browser.Post("/baskets", with =>
			{
				var basket = new Basket {Items = new List<BasketItem> {new BasketItem {ProductCode = "abc123"}}};
				with.JsonBody(basket);
				with.Header("Content-Type", "application/vnd.nancyshop+json");
				with.Accept("application/vnd.nancyshop+json");
			});
		}
	}
}