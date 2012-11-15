using Nancy.Testing;

namespace NancyShop.Basket.Feature.Tests
{
	static internal class BasketFacade
	{
		internal static BrowserResponse Post_Basket(Basket basket)
		{
			var response = Context.Browser.Post("/baskets", with =>
				                                                {
					                                                with.JsonBody(basket);
					                                                with.Header("Content-Type", Context.NancyShopBasketJsonContentType);
					                                                with.Accept(Context.NancyShopBasketJsonContentType);
				                                                });

			return response;
		}
	}
}