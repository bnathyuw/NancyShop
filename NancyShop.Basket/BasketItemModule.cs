using Nancy;
using Nancy.ModelBinding;
using NancyShop.Basket.Domain;

namespace NancyShop.Basket
{
	public class BasketItemModule:NancyModule
	{
		private readonly IBasketItemStore _basketItemStore;

		public BasketItemModule(IBasketItemStore basketItemStore)
		{
			_basketItemStore = basketItemStore;

			Post[@"/baskets/(?<BasketId>\d+)/items"] = parameters =>
				                                           {
					                                           var basketItemResource = this.Bind<BasketItemResource>();
					                                           PostBasketItem(basketItemResource);
					                                           return Negotiate.WithModel(basketItemResource)
					                                                           .WithStatusCode(HttpStatusCode.Created)
					                                                           .WithHeader("Location", basketItemResource.Url())
																			   .WithHeader("Links", "/baskets/" + basketItemResource.BasketId + "; rel=basket");
				                                           };
		}

		private void PostBasketItem(BasketItemResource basketItemResource)
		{
			var basketItem = basketItemResource.ToBasketItem();
			_basketItemStore.Add(basketItem);
			basketItemResource.Id = basketItem.Id;
		}
	}
}