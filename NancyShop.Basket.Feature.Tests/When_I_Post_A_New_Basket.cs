using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests
{
	[TestFixture]
	public class When_I_Post_A_New_Basket
	{
		private BrowserResponse _response;

		[SetUp]
		public void SetUp()
		{
			_response = Context.Browser.Post("/baskets", with =>
			{
				with.Body("{'items':[{'productcode':'abc123'}]}");
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

		[Test]
		public void Then_The_Basket_At_The_Created_Location_Exists()
		{
			var location = _response.Headers["Location"];

			var newBasketResponse = Context.Browser.Get(location, with => with.Accept("application/vnd.nancyshop+json"));

			Assert.That(newBasketResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
		}
	}
}