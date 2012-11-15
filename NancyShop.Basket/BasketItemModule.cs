using Nancy;

namespace NancyShop.Basket
{
	public class BasketItemModule:NancyModule
	{
		public BasketItemModule()
		{
			Post[@"/baskets/(?<BasketId>\d+)/items"] = PostBasketItem;
		}

		private dynamic PostBasketItem(dynamic arg)
		{
			return HttpStatusCode.Created;
		}
	}
}