using NUnit.Framework;
using NancyShop.Basket.Domain;
using Rhino.Mocks;

namespace NancyShop.Basket.Unit.Tests
{
	[TestFixture]
	public class BasketLogicTests
	{
		private IBasketStore _basketStore;
		private IBasketItemStore _basketItemStore;
		private BasketLogic _basketLogic;

		[SetUp]
		public void SetUp()
		{
			_basketStore = MockRepository.GenerateStub<IBasketStore>();
			_basketItemStore = MockRepository.GenerateStub<IBasketItemStore>();
			_basketLogic = new BasketLogic(_basketStore, _basketItemStore);
		}

		[Test]
		public void Get_Basket_Retrieves_Basket_From_Store()
		{
			const int basketId = 1;
			var basket = new Domain.Basket();
			_basketStore.Stub(bs => bs.Get(basketId)).Return(basket);
			_basketItemStore.Stub(bis => bis.GetForBasket(basketId)).Return(new BasketItem[]{});

			_basketLogic.GetBasket(basketId);

			_basketStore.AssertWasCalled(bs => bs.Get(basketId));
		}
	}
}