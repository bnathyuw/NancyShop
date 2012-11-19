namespace NancyShop.Basket.Domain
{
	public interface IBasketStore
	{
		Basket Get(int basketId);
		void Add(Basket basket);
	}
}