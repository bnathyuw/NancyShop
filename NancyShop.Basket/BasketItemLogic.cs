using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketItemLogic : IBasketItemLogic
	{
		private readonly IBasketItemStore _basketItemStore;

		public BasketItemLogic(IBasketItemStore basketItemStore)
		{
			_basketItemStore = basketItemStore;
		}

		public BasketItemResource PostBasketItem(BasketItemResource basketItemResource)
		{
			var basketItem = basketItemResource.ToBasketItem();
			_basketItemStore.Add(basketItem);
			basketItemResource.Id = basketItem.Id;
			return basketItemResource;
		}
	}
}