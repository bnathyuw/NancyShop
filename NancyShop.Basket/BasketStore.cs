using System.Collections.Generic;
using System.Linq;

namespace NancyShop.Basket
{
	public static class BasketStore
	{
		private static readonly IList<Basket> Baskets = new List<Basket>();

		public static Basket Get(int basketId)
		{
			return Baskets.Single(b => b.Id == basketId);
		}

		public static void Add(Basket basket)
		{
			basket.Id = Baskets.Any() ? Baskets.Max(b => b.Id) + 1 : 1;
			Baskets.Add(basket);
		}
	}
}