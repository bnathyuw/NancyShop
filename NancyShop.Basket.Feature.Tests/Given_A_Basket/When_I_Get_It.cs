using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.Given_A_Basket
{
	[TestFixture]
	public class When_I_Get_It
	{
		private const string ProductCode = "abc123";
		private BrowserResponse _response;
		private readonly Basket _basket = new Basket {Items = new List<BasketItem> {new BasketItem {ProductCode = ProductCode}}};

		[SetUp]
		public void SetUp()
		{
			var response = Context.Browser.Post("/baskets", with =>
			{
				with.JsonBody(_basket);
				with.Header("Content-Type", Context.NancyShopBasketJsonContentType);
				with.Accept(Context.NancyShopBasketJsonContentType);
			});

			var location = response.Headers["Location"]; 

			_response = Context.Browser.Get(location, with => with.Accept(Context.NancyShopBasketJsonContentType));
		}

		[Test]
		public void Then_I_Receive_An_Ok_Code()
		{
			Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Then_I_Receive_A_Faithful_Representation_Of_It()
		{
			var basket = _response.Body.DeserializeJson<Basket>();
			Assert.That(basket, Is.Not.Null, "basket");
			Assert.That(basket.Items.Count(), Is.EqualTo(_basket.Items.Count()), "basket.Items.Count()");
			Assert.That(basket.Items.First().ProductCode, Is.EqualTo(ProductCode), "basket.Items.First().ProductCode");
		}
	}
}