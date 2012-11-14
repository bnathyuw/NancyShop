using System.Collections.Generic;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests
{
	[TestFixture]
	public class When_I_Post_A_New_Basket
	{
		private BrowserResponse _response;

		[TestFixtureSetUp]
		public void SetUp()
		{
			_response = Given_A_Browser.Browser.Post("/baskets", with =>
			{
				var basket = new Basket {Items = new List<BasketItem> {new BasketItem {ProductCode = "abc123"}}};
				with.JsonBody(basket);
				with.Header("Content-Type", "application/vnd.nancyshop+json");
				with.Accept("application/vnd.nancyshop+json");
			});
		}

		[Test]
		public void Then_I_Receive_A_Created_Code()
		{
			Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Then_I_Receive_The_Location_Of_The_New_Basket()
		{
			Assert.That(_response.Headers["Location"], Is.StringMatching(@"/baskets/\d*"));
		}
	}
}