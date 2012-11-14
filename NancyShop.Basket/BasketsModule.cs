using Nancy;

namespace NancyShop.Basket
{
	public class BasketsModule:NancyModule
	{
		public BasketsModule():base("/baskets")
		{
			Post["/"] = parameters => HttpStatusCode.Created;
		}
	}
}