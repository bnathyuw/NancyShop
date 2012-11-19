using System.Linq;
using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketLogic : IBasketLogic
	{
		private readonly IBasketStore _basketStore;
		private readonly IBasketItemStore _basketItemStore;

		public BasketLogic(IBasketStore basketStore, IBasketItemStore basketItemStore)
		{
			_basketStore = basketStore;
			_basketItemStore = basketItemStore;
		}

		public BasketResource PostBasket(BasketResource basketResource)
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

		public BasketResource GetBasket(int basketId)
		{
			var basket = _basketStore.Get(basketId);
			var basketResource = BasketResource.FromBasket(basket);
			var basketItems = _basketItemStore.GetForBasket(basketId).Select(BasketItemResource.FromBasketItem);
			basketResource.Items = basketItems;
			return basketResource;
		}
	}
}