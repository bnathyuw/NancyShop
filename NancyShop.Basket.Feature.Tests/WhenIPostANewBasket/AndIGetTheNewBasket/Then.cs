using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.WhenIPostANewBasket.AndIGetTheNewBasket
{
	[TestFixture]
	public class Then
	{
		[Test]
		public void Then_The_Basket_At_The_Created_Location_Exists()
		{
			Assert.That(And_I_Get_The_New_Basket.Response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Then_The_Basket_At_The_Created_Location_Contains_The_Same_Stuff()
		{
			var basket = And_I_Get_The_New_Basket.Response.Body.DeserializeJson<Basket>();
			Assert.That(basket, Is.Not.Null, "basket");
			Assert.That(basket.Items.Count(), Is.EqualTo(1), "basket.Items.Count()");
			Assert.That(basket.Items.First().ProductCode, Is.EqualTo("abc123"), "basket.Items.First().ProductCode");
		}
	}
}