namespace NancyShop.Basket
{
	public static class LinkExtensions
	{
		public static string Url(this BasketResource basketResource)
		{
			return "/baskets/" + basketResource.Id;
		}
	}
}