using System;
using System.Collections.Generic;

using TellDontAskKata.Domain;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.Repository;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderCreationUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;

        private readonly Category food;

        private readonly IProductCatalog productCatalogue;

        private readonly OrderCreationUseCase useCase;

        public OrderCreationUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            food = new Category
            {
                Name = "Food",
                TaxPercentage = new decimal(10)
            };
            productCatalogue = new InMemoryProductCatalog(
                new List<Product>()
                {
                    new Product { Category = this.food, Name = "salad", Price = new decimal(3.56) },
                    new Product { Category = this.food, Name = "tomato", Price = new decimal(4.65)}
                });
            useCase = new OrderCreationUseCase(orderRepository, productCatalogue);
        }

        [Fact]
        public void SellMultipleItems()
        {
            SellItemRequest saladRequest = new SellItemRequest
            {
                ProductName = "salad",
                Quantity = 2
            };

            SellItemRequest tomatoRequest = new SellItemRequest
            {
                ProductName = "tomato",
                Quantity = 3
            };

            SellItemsRequest request =
                new SellItemsRequest
                {
                    Requests = new List<SellItemRequest>
                    {
                        saladRequest, tomatoRequest
                    }
                };

            useCase.Run(request);

            Order insertedOrder = orderRepository.SavedOrder;
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
            SellItemsRequest request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest>()
            };
            SellItemRequest unknownProductRequest = new SellItemRequest
            {
                ProductName = "unknown product"
            };
            request.Requests.Add(unknownProductRequest);

            Action runAction = () => useCase.Run(request);
            Assert.Throws<UnknownProductException>(runAction);
        }
    }
}
