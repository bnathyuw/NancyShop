using System.Collections.Generic;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.Given_Nothing
{
	[TestFixture]
	public class When_I_Post_A_New_Basket
	{
		private BrowserResponse _response;

		[TestFixtureSetUp]
		public void SetUp()
		{
			_response = Context.Browser.Post("/baskets", with =>
			{
				var basket = new Basket {Items = new List<BasketItem> {new BasketItem {ProductCode = "abc123"}}};
				with.JsonBody(basket);
				with.Header("Content-Type", Context.NancyShopBasketJsonContentType);
				with.Accept(Context.NancyShopBasketJsonContentType);
			});
		}

		[Test]
		public void Then_I_Receive_A_Created_Code()
		{
			Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Then_I_Receive_Its_Location()
		{
			Assert.That(_response.Headers["Location"], Is.StringMatching(@"/baskets/\d*"));
		}
	}
}