using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketItemResource
	{
		public int Id { get; set; }
		public string ProductCode { get; set; }

		public BasketItem ToBasketItem(int basketId)
		{
			return new BasketItem
				       {
						   Id = Id,
						   BasketId = basketId, 
						   ProductCode = ProductCode
				       };
		}

		public static BasketItemResource FromBasketItem(BasketItem basketItem)
		{
			return new BasketItemResource
				       {
					       Id = basketItem.Id,
						   ProductCode = basketItem.ProductCode
				       };
		}
	}
}