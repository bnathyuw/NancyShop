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

			Post["/baskets"] = parameters =>
				                   {
					                   var basketResource = PostBasket(this.Bind<BasketResource>());
					                   return Negotiate.WithModel(basketResource)
					                                   .WithStatusCode(HttpStatusCode.Created)
					                                   .WithHeader("Location", basketResource.Url());
				                   };

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

		private BasketResource PostBasket(BasketResource basketResource)
		{
			var basket = basketResource.ToBasket();
			_basketStore.Add(basket);
			basketResource.Id = basket.Id;
			foreach (var basketItemResource in basketResource.Items)
			{
				basketItemResource.BasketId = basket.Id;
				var basketItem = basketItemResource.ToBasketItem();
				_basketItemStore.Add(basketItem);
			}
			return basketResource;
		}
	}
}