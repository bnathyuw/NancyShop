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
		private readonly BasketResource _basketResource = new BasketResource {Items = new List<BasketItemResource> {new BasketItemResource {ProductCode = ProductCode}}};

		[SetUp]
		public void SetUp()
		{
			var location = BasketFacade.Post_Basket(_basketResource).GetLocation();

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
			var basket = _response.Body.DeserializeJson<BasketResource>();
			Assert.That(basket, Is.Not.Null, "BasketResource");
			Assert.That(basket.Items.Count(), Is.EqualTo(_basketResource.Items.Count()), "BasketResource.Items.Count()");
			Assert.That(basket.Items.First().ProductCode, Is.EqualTo(ProductCode), "BasketResource.Items.First().ProductCode");
		}
	}
}