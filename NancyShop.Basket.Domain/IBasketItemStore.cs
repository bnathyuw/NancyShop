using System.Collections.Generic;

namespace NancyShop.Basket.Domain
{
	public interface IBasketItemStore
	{
		IEnumerable<BasketItem> GetForBasket(int basketId);
		void Add(BasketItem basketItem);
	}
}