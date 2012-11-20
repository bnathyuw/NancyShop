using NUnit.Framework;
using NancyShop.Basket.Domain;
using Rhino.Mocks;

namespace NancyShop.Basket.Unit.Tests
{
	[TestFixture]
	public class BasketLogicTests
	{
		[Test]
		public void Get_Basket_Retrieves_Basket_From_Store()
		{
			var basketStore = MockRepository.GenerateStub<IBasketStore>();
			const int basketId = 1;
			var basket = new Domain.Basket();
			basketStore.Stub(bs => bs.Get(basketId)).Return(basket);
			var basketItemStore = MockRepository.GenerateStub<IBasketItemStore>();
			basketItemStore.Stub(bis => bis.GetForBasket(basketId)).Return(new BasketItem[]{});
			var basketLogic = new BasketLogic(basketStore, basketItemStore);

			basketLogic.GetBasket(basketId);

			basketStore.AssertWasCalled(bs => bs.Get(basketId));
		}
	}
}