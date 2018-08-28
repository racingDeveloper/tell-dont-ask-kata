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
            SellItemRequest saladRequest = new SellItemRequest();
            saladRequest.ProductName = "salad";
            saladRequest.Quantity = 2;

            SellItemRequest tomatoRequest = new SellItemRequest();
            tomatoRequest.ProductName = "tomato";
            tomatoRequest.Quantity = 3;

            SellItemsRequest request = new SellItemsRequest();
            request.Requests = new List<SellItemRequest> { saladRequest, tomatoRequest };

            useCase.Run(request);

            Order insertedOrder = orderRepository.SavedOrder;
            Assert.Equal(insertedOrder.Status, OrderStatus.Created);
            Assert.Equal(insertedOrder.Total, new decimal(23.20));
            Assert.Equal(insertedOrder.Tax, new decimal(2.13));
            Assert.Equal(insertedOrder.Currency, "EUR");
            Assert.Equal(insertedOrder.Items.Count, 2);
            Assert.Equal(insertedOrder.Items[0].Product.Name, "salad");
            Assert.Equal(insertedOrder.Items[0].Product.Price, new decimal(3.56));
            Assert.Equal(insertedOrder.Items[0].Quantity, 2);
            Assert.Equal(insertedOrder.Items[0].TaxedAmount, new decimal(7.84));
            Assert.Equal(insertedOrder.Items[0].Tax, new decimal(0.72));
            Assert.Equal(insertedOrder.Items[1].Product.Name, "tomato");
            Assert.Equal(insertedOrder.Items[1].Product.Price, new decimal(4.65));
            Assert.Equal(insertedOrder.Items[1].Quantity, 3);
            Assert.Equal(insertedOrder.Items[1].TaxedAmount, new decimal(15.36));
            Assert.Equal(insertedOrder.Items[1].Tax, new decimal(1.41));
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
