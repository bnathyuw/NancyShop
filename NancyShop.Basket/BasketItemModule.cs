using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketItemModule:NancyModule
	{
		private readonly BasketItemLogic _basketItemLogic;

		public BasketItemModule(BasketItemLogic basketItemLogic)
		{
			_basketItemLogic = basketItemLogic;
			Post[@"/baskets/(?<BasketId>\d+)/items"] = HandlePostBasketItem;
		}

		private dynamic HandlePostBasketItem(dynamic parameters)
		{
			var request = this.Bind<BasketItemResource>();
			var response = _basketItemLogic.PostBasketItem(request);
			return Negotiate.WithModel(response)
			                .WithStatusCode(HttpStatusCode.Created)
			                .WithHeader("Location", request.Url())
			                .WithHeader("Links", "/baskets/" + request.BasketId + "; rel=basket");
		}
	}
}