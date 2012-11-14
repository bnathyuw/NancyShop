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
		public void I_Receive_A_Created_Code()
		{
			Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
		}
	}
}