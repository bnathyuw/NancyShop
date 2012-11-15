using System.Collections.Generic;
using System.Linq;

namespace NancyShop.Basket.Domain
{
	public class InMemoryBasketItemStore:IBasketItemStore
	{
		private static readonly IList<BasketItem> BasketItems = new List<BasketItem>();

		public IEnumerable<BasketItem> GetForBasket(int basketId)
		{
			return BasketItems.Where(bi => bi.BasketId == basketId);
		}

		public void Add(BasketItem basketItem)
		{
			basketItem.Id = BasketItems.Any() ? BasketItems.Max(bi => bi.Id) + 1 : 1;
			BasketItems.Add(basketItem);
		}
	}
}