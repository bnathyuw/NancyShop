using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketModule:NancyModule
	{
		private readonly IBasketStore _basketStore;
		private readonly IBasketItemStore _basketItemStore;

		public BasketModule(IBasketStore basketStore, IBasketItemStore basketItemStore)
		{
			_basketStore = basketStore;
			_basketItemStore = basketItemStore;

			Post["/baskets"] = parameters => PostBasket(this.Bind<BasketResource>());

			Get[@"/baskets/(?<Id>\d*)"] = parameters => GetBasket(parameters.Id);
		}

		private BasketResource GetBasket(int basketId)
		{
			var basket = _basketStore.Get(basketId);
			var basketResource = BasketResource.FromBasket(basket);
			var basketItems = _basketItemStore.GetForBasket(basketId).Select(BasketItemResource.FromBasketItem);
			basketResource.Items = basketItems;
			return basketResource;
		}

		private Response PostBasket(BasketResource basketResource)
		{
			var basket = basketResource.ToBasket();
			_basketStore.Add(basket);
			basketResource.Id = basket.Id;
			foreach (var basketItemResource in basketResource.Items)
			{
				var basketItem = basketItemResource.ToBasketItem(basket.Id);
				_basketItemStore.Add(basketItem);
			}
			return new Response
				       {
					       StatusCode = HttpStatusCode.Created, 
						   Headers = new Dictionary<string, string> {{"Location", basketResource.Url()}}
				       };
		}
	}
}