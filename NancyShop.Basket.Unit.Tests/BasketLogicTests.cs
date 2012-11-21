using System;
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

		[Test]
		public void Post_Basket_Adds_Basket_To_Store()
		{
			var basketResource = new BasketResource {Items = new BasketItemResource[]{}};

			_basketLogic.PostBasket(basketResource);

			_basketStore.AssertWasCalled(bs => bs.Add(Arg<Domain.Basket>.Is.Anything));
		}

		[Test]
		public void Post_Basket_Adds_All_Basket_Items_To_Store()
		{
			var basketResource = new BasketResource {Items = new[] {new BasketItemResource(), new BasketItemResource(), new BasketItemResource()}};

			_basketLogic.PostBasket(basketResource);

			_basketItemStore.AssertWasCalled(bis => bis.Add(Arg<BasketItem>.Is.Anything), x => x.Repeat.Times(3));
		}

		[Test]
		public void Post_Basket_Saves_Basket_Items_With_Correct_Basket_Id()
		{
			const int basketId = 1234;
			var basketResource = new BasketResource {Items = new[] {new BasketItemResource {ProductCode = "abc123"}}};
			_basketStore.Stub(bs => bs.Add(Arg<Domain.Basket>.Is.Anything)).Do(new Action<Domain.Basket>(b => { b.Id = basketId; }));

			_basketLogic.PostBasket(basketResource);

			_basketItemStore.AssertWasCalled(bis => bis.Add(Arg<BasketItem>.Matches(bi => bi.BasketId == basketId)));
		}
	}
}