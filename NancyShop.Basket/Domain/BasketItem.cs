namespace NancyShop.Basket.Domain
{
	public class BasketItem
	{
		public int Id { get; set; }
		public int BasketId { get; set; }
		public string ProductCode { get; set; }
	}
}