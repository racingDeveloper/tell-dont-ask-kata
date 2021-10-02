using System;
using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Tests.Doubles;
using Xunit;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderCreationUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;
        private readonly OrderCreationUseCase _useCase;

        public OrderCreationUseCaseTest()
        {
            var food = new Category { 
                Name = "food",
                TaxPercentage = 10m
            };

            _productCatalog = new InMemoryProductCatalog(new List<Product>
            {
                new Product
                {
                    Name = "salad",
                    Price = 3.56m,
                    Category = food
                },
                new Product
                {
                    Name = "tomato",
                    Price = 4.65m,
                    Category = food
                }
            });

            _orderRepository = new TestOrderRepository();

            _useCase = new OrderCreationUseCase(_orderRepository, _productCatalog);
        }


        [Fact]
        public void SellMultipleItems()
        {
            var saladRequest = new SellItemRequest
            {
                ProductName = "salad",
                Quantity = 2
            };

            var tomatoRequest = new SellItemRequest
            {
                ProductName = "tomato",
                Quantity = 3
            };

            var request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest> { saladRequest, tomatoRequest }
            };

            _useCase.Run(request);

            Order insertedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Created, insertedOrder.Status);
            Assert.Equal(23.20m, insertedOrder.Total);
            Assert.Equal(2.13m, insertedOrder.Tax);
            Assert.Equal("EUR", insertedOrder.Currency);
            Assert.Equal(2, insertedOrder.Items.Count);
            Assert.Equal("salad", insertedOrder.Items[0].Product.Name);
            Assert.Equal(3.56m, insertedOrder.Items[0].Product.Price);
            Assert.Equal(2, insertedOrder.Items[0].Quantity);
            Assert.Equal(7.84m, insertedOrder.Items[0].TaxedAmount);
            Assert.Equal(0.72m, insertedOrder.Items[0].Tax);
            Assert.Equal("tomato", insertedOrder.Items[1].Product.Name);
            Assert.Equal(4.65m, insertedOrder.Items[1].Product.Price);
            Assert.Equal(3, insertedOrder.Items[1].Quantity);
            Assert.Equal(15.36m, insertedOrder.Items[1].TaxedAmount);
            Assert.Equal(1.41m, insertedOrder.Items[1].Tax);
        }

        [Fact]
        public void UnknownProduct()
        {
            var request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest> { 
                    new SellItemRequest { ProductName = "unknown product"}
                }
            };

            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<UnknownProductException>(actionToTest);
        }



    }
}
