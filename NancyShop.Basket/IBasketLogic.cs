namespace NancyShop.Basket
{
	public interface IBasketLogic
	{
		BasketResource PostBasket(BasketResource basketResource);
		BasketResource GetBasket(int basketId);
	}
}