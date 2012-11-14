using System.Collections.Generic;
using Nancy;
using Nancy.Responses;

namespace NancyShop.Basket
{
	public class BasketsModule:NancyModule
	{
		public BasketsModule():base("/baskets")
		{
			Post["/"] = parameters => new Response
				                          {
					                          StatusCode = HttpStatusCode.Created,
					                          Headers = new Dictionary<string, string> {{"Location", "/baskets/123"}}
				                          };

			Get[@"/(?<id>\d*)"] = parameters => HttpStatusCode.OK;
		}
	}
}