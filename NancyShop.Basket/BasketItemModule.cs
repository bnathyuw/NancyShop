using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

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
			var basketId = arg.BasketId;
			var basketItemResource = this.Bind<BasketItemResource>();
			return new Response
				       {
					       StatusCode = HttpStatusCode.Created,
						   Headers = new Dictionary<string, string>{{"Location", basketItemResource.Url()}}
				       };
		}
	}
}