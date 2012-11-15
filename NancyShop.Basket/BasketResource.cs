using System.Collections.Generic;

namespace NancyShop.Basket
{
	public class BasketResource
	{
		public int Id { get; set; }
		public IEnumerable<BasketItemResource> Items { get; set; }

		public Domain.Basket ToBasket()
		{
			return new Domain.Basket
				       {
					       Id = Id
				       };
		}

		public static BasketResource FromBasket(Domain.Basket basket)
		{
			return new BasketResource
				       {
					       Id = basket.Id
				       };
		}
	}
}