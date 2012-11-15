using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketModule:NancyModule
	{
		private readonly BasketStore _basketStore;

		public BasketModule()
		{
			_basketStore = new BasketStore();
			
			Post["/baskets"] = PostBasket;

			Get[@"/baskets/(?<Id>\d*)"] = GetBasket;
		}

		private dynamic GetBasket(dynamic parameters)
		{
			var basket = _basketStore.Get(parameters.Id);
			return basket;
		}

		private dynamic PostBasket(dynamic parameters)
		{
			var basket = this.Bind<Basket>();
			_basketStore.Add(basket);
			return new Response
				       {
					       StatusCode = HttpStatusCode.Created, Headers = new Dictionary<string, string> {{"Location", "/baskets/" + basket.Id}}
				       };
		}
	}
}