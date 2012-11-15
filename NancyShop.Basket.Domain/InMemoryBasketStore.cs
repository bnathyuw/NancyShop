using System.Collections.Generic;
using System.Linq;

namespace NancyShop.Basket.Domain
{
	public class InMemoryBasketStore : IBasketStore
	{
		private static readonly IList<Basket> Baskets = new List<Basket>();

		public Basket Get(int basketId)
		{
			return Baskets.Single(b => b.Id == basketId);
		}

		public void Add(Basket basket)
		{
			basket.Id = Baskets.Any() ? Baskets.Max(b => b.Id) + 1 : 1;
			Baskets.Add(basket);
		}
	}
}