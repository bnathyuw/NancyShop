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

			Post["/baskets"] = HandlePostBasket;

			Get[@"/baskets/(?<Id>\d*)"] = HandleGetBasket;
		}

		private dynamic HandlePostBasket(dynamic parameters)
		{
			var request = this.Bind<BasketResource>();
			var response = PostBasket(request);
			return Negotiate.WithModel(response)
			                .WithStatusCode(HttpStatusCode.Created)
			                .WithHeader("Location", response.Url());
		}

		private dynamic HandleGetBasket(dynamic parameters)
		{
			BasketResource basket = GetBasket(parameters.Id);
			return Negotiate.WithModel(basket)
			                .WithStatusCode(HttpStatusCode.OK)
			                .WithHeader("Links", string.Join(",", basket.Items.Select(bi => bi.Url() + "; rel=basketitem")));
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

		private BasketResource GetBasket(int basketId)
		{
			var basket = _basketStore.Get(basketId);
			var basketResource = BasketResource.FromBasket(basket);
			var basketItems = _basketItemStore.GetForBasket(basketId).Select(BasketItemResource.FromBasketItem);
			basketResource.Items = basketItems;
			return basketResource;
		}
	}
}