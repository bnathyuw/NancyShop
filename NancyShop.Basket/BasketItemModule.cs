using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketItemModule:NancyModule
	{
		private readonly IBasketItemLogic _basketItemLogic;

		public BasketItemModule(IBasketItemLogic basketItemLogic)
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