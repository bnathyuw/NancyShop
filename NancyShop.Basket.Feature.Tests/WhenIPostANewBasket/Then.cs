using System.Linq;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests.WhenIPostANewBasket
{
	[TestFixture]
	public class Then
	{
		private static BrowserResponse Response { get { return When_I_Post_A_New_Basket.Response; } }

		[Test]
		public void Then_I_Receive_A_Created_Code()
		{
			Assert.That(Response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}

		[Test]
		public void Then_I_Receive_The_Location_Of_The_New_Basket()
		{
			Assert.That(Response.Headers["Location"], Is.StringMatching(@"/baskets/\d*"));
		}
	}
}