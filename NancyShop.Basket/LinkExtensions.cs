namespace NancyShop.Basket
{
	public static class LinkExtensions
	{
		public static string Url(this BasketResource basketResource)
		{
			return "/baskets/" + basketResource.Id;
		}

		public static string Url(this BasketItemResource basketItemResource)
		{
			return "/baskets/" + basketItemResource.BasketId + "/items/" + basketItemResource.Id;
		}
	}
}