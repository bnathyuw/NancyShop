using System.Collections.Generic;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.Given_A_Basket
{
	[TestFixture]
	public class When_I_Post_Another_Item_To_It
	{
		private BrowserResponse _response;
		private string _basketLocation;

		[SetUp]
		public void SetUp()
		{
			var basket = new BasketResource {Items = new List<BasketItemResource> {new BasketItemResource {ProductCode = "abc123"}}};

			_basketLocation = BasketFacade.Post_Basket(basket).GetLocation();

			_response = Context.Browser.Post(_basketLocation + "/items", with =>
				                                                      {
					                                                      with.JsonBody(new BasketItemResource {ProductCode = "cde345"});
					                                                      with.Accept(Context.NancyShopBasketItemJsonContentType);
					                                                      with.Header("Content-Type", Context.NancyShopBasketItemJsonContentType);
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
			Assert.That(_response.Headers["Location"], Is.StringMatching(_basketLocation + @"/items/\d+"));
		}
	}
}