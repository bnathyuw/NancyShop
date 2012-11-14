using System.Collections.Generic;

namespace NancyShop.Basket
{
	public class Basket
	{
		public IEnumerable<BasketItem> Items { get; set; }

		public int Id { get; set; }
	}
}