using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketModule:NancyModule
	{
		public BasketModule()
		{
			Post["/baskets"] = PostBasket;

			Get[@"/baskets/(?<Id>\d*)"] = GetBasket;
		}

		private dynamic GetBasket(dynamic parameters)
		{
			var basket = BasketStore.Get(parameters.Id);
			return basket;
		}

		private dynamic PostBasket(dynamic parameters)
		{
			var basket = this.Bind<Basket>();
			BasketStore.Add(basket);
			return new Response
				       {
					       StatusCode = HttpStatusCode.Created, Headers = new Dictionary<string, string> {{"Location", "/baskets/" + basket.Id}}
				       };
		}
	}
}