using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketItemModule:NancyModule
	{
		public BasketItemModule()
		{
			Post[@"/baskets/(?<BasketId>\d+)/items"] = parameters =>
				                                           {
					                                           var basketId = parameters.BasketId;
					                                           var basketItemResource = this.Bind<BasketItemResource>();
					                                           return Negotiate.WithModel(basketItemResource)
					                                                           .WithStatusCode(HttpStatusCode.Created)
					                                                           .WithHeader("Location", basketItemResource.Url());
				                                           };
		}
	}
}