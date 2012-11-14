using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;

namespace NancyShop.Basket
{
	public class BasketModule:NancyModule
	{
		public BasketModule() : base("/baskets")
		{
			Post["/"] = parameters =>
				            {
					            var basket = this.Bind<Basket>();
					            BasketStore.Add(basket);
					            return new Response
						                   {
							                   StatusCode = HttpStatusCode.Created,
							                   Headers = new Dictionary<string, string> {{"Location", "/baskets/" + basket.Id}}
						                   };
				            };

			Get[@"/(?<Id>\d*)"] = parameters =>
				                      {
					                      var basket = BasketStore.Get(parameters.Id);
					                      return basket;
				                      };
		}
	}
}