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

			Post[@"/baskets/(?<BasketId>\d+)/items"] = HandlePostBasketItem;
		}

		private dynamic HandlePostBasketItem(dynamic parameters)
		{
			var request = this.Bind<BasketItemResource>();
			var response = PostBasketItem(request);
			return Negotiate.WithModel(response)
			                .WithStatusCode(HttpStatusCode.Created)
			                .WithHeader("Location", request.Url())
			                .WithHeader("Links", "/baskets/" + request.BasketId + "; rel=basket");
		}

		private BasketItemResource PostBasketItem(BasketItemResource basketItemResource)
		{
			var basketItem = basketItemResource.ToBasketItem();
			_basketItemStore.Add(basketItem);
			basketItemResource.Id = basketItem.Id;
			return basketItemResource;
		}
	}
}