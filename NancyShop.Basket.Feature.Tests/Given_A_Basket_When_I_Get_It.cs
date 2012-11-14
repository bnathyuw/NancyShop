using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.And_A_Basket
{
	[TestFixture]
	public class Given_A_Basket_When_I_Get_It
	{
		private const string ProductCode = "abc123";
		private BrowserResponse _response;
		private readonly Basket _basket = new Basket {Items = new List<BasketItem> {new BasketItem {ProductCode = ProductCode}}};

		[SetUp]
		public void SetUp()
		{
			var response = Given_A_Browser.Browser.Post("/baskets", with =>
			{
				with.JsonBody(_basket);
				with.Header("Content-Type", "application/vnd.nancyshop+json");
				with.Accept("application/vnd.nancyshop+json");
			});

			var location = response.Headers["Location"]; 

			_response = Given_A_Browser.Browser.Get(location, with => with.Accept("application/vnd.nancyshop+json"));
		}

		[Test]
		public void Then_It_Exists()
		{
			Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Then_It_Contains_The_Correct_Data()
		{
			var basket = _response.Body.DeserializeJson<Basket>();
			Assert.That(basket, Is.Not.Null, "basket");
			Assert.That(basket.Items.Count(), Is.EqualTo(_basket.Items.Count()), "basket.Items.Count()");
			Assert.That(basket.Items.First().ProductCode, Is.EqualTo(ProductCode), "basket.Items.First().ProductCode");
		}
	}
}