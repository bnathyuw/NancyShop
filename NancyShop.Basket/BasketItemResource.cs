using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketItemResource
	{
		public int Id { get; set; }
		public int BasketId { get; set; }
		public string ProductCode { get; set; }

		public BasketItem ToBasketItem()
		{
			return new BasketItem
				       {
						   Id = Id,
						   BasketId = BasketId, 
						   ProductCode = ProductCode
				       };
		}

		public static BasketItemResource FromBasketItem(BasketItem basketItem)
		{
			return new BasketItemResource
				       {
					       Id = basketItem.Id,
						   BasketId = basketItem.BasketId,
						   ProductCode = basketItem.ProductCode
				       };
		}
	}
}