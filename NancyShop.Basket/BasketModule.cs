using System.Linq;
using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketModule:NancyModule
	{
		private readonly IBasketLogic _basketLogic;

		public BasketModule(IBasketLogic basketLogic)
		{
			_basketLogic = basketLogic;

			Post["/baskets"] = HandlePostBasket;

			Get[@"/baskets/(?<Id>\d*)"] = HandleGetBasket;
		}

		private dynamic HandlePostBasket(dynamic parameters)
		{
			var request = this.Bind<BasketResource>();
			var response = _basketLogic.PostBasket(request);
			return Negotiate.WithModel(response)
			                .WithStatusCode(HttpStatusCode.Created)
			                .WithHeader("Location", response.Url());
		}

		private dynamic HandleGetBasket(dynamic parameters)
		{
			BasketResource basket = _basketLogic.GetBasket(parameters.Id);
			return Negotiate.WithModel(basket)
			                .WithStatusCode(HttpStatusCode.OK)
			                .WithHeader("Links", string.Join(",", basket.Items.Select(bi => bi.Url() + "; rel=basketitem")));
		}
	}
}