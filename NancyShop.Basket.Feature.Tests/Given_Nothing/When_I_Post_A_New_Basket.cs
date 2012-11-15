using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.Given_Nothing
{
	[TestFixture]
	public class When_I_Post_A_New_Basket
	{
		private const string ProductCode = "abc123";
		private BrowserResponse _response;

		[TestFixtureSetUp]
		public void SetUp()
		{
			var basket = new BasketResource {Items = new List<BasketItemResource> {new BasketItemResource {ProductCode = ProductCode}}};
			_response = BasketFacade.Post_Basket(basket);
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

		[Test]
		public void Then_I_Receive_A_Faithful_Representation_Of_It()
		{
			var basketResource = _response.Body.DeserializeJson<BasketResource>();
			Assert.That(basketResource, Is.Not.Null, "BasketResource");
			Assert.That(basketResource.Items.Count(), Is.EqualTo(basketResource.Items.Count()), "BasketResource.Items.Count()");
			Assert.That(basketResource.Items.First().ProductCode, Is.EqualTo(ProductCode), "BasketResource.Items.First().ProductCode");
		}
	}
}