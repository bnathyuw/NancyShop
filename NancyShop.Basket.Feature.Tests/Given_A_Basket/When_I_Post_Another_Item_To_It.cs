using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.Given_A_Basket
{
	[TestFixture]
	public class When_I_Post_Another_Item_To_It
	{
		private const string ProductCode = "cde345";
		private BrowserResponse _response;
		private string _basketLocation;
		private BasketResource _basketResource;

		[SetUp]
		public void SetUp()
		{
			var response = BasketFacade.Post_Basket(new BasketResource {Items = new List<BasketItemResource> {new BasketItemResource {ProductCode = "abc123"}}});
			_basketResource = response.Body.DeserializeJson<BasketResource>();
			_basketLocation = response.GetLocation();

			_response = Context.Browser.Post(_basketLocation + "/items", with =>
				                                                      {
					                                                      with.JsonBody(new BasketItemResource {ProductCode = ProductCode});
					                                                      with.Accept(Context.NancyShopBasketItemJsonContentType);
					                                                      with.Header("Content-Type", Context.NancyShopBasketItemJsonContentType);
				                                                      });
		}

		[Test]
		public void Then_I_Receive_A_Created_Code()
		{
			_response.AssertStatusCode(HttpStatusCode.Created);
		}

		[Test]
		public void Then_I_Receive_Its_Location()
		{
			_response.AssertLocation(_basketLocation + @"/items/\d+");
		}

		[Test]
		public void Then_I_Receive_A_Faithful_Representation_Of_It()
		{
			var basketItem = _response.Body.DeserializeJson<BasketItemResource>();
			Assert.That(basketItem, Is.Not.Null, "basketItem");
			Assert.That(basketItem.ProductCode, Is.EqualTo(ProductCode), "basketItem.ProductCode");
			Assert.That(basketItem.BasketId, Is.EqualTo(_basketResource.Id), "basketItem.BasketId");
		}

		[Test]
		public void Then_I_Receive_A_Link_To_Its_Basket()
		{
			_response.AssertLinkValue("basket", _basketLocation);
		}
	}
}