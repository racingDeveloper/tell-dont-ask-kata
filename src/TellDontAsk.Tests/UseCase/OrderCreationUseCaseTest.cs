using System;
using System.Collections.Generic;
using System.Linq;
using TellDontAsk.Domain;
using TellDontAsk.Repository;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
{
    public class OrderCreationUseCaseTest
    {
        private readonly Category food;
        private readonly TestOrderRepository orderRepository;
        private readonly ProductCatalog productCatalog;

        private readonly OrderCreationUseCase useCase;

        public OrderCreationUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            food = new Category();

            food.setName("food");
            food.setTaxPercentage(new decimal(10));


            var salad = new Product();
            salad.setName("salad");
            salad.setPrice(new decimal(3.56));
            salad.setCategory(food);

            var tomato = new Product();
            tomato.setName("tomato");
            tomato.setPrice(new decimal(4.65));
            tomato.setCategory(food);

            productCatalog = new InMemoryProductCatalog(new List<Product> { salad, tomato });


            useCase = new OrderCreationUseCase(orderRepository, productCatalog);
        }

        [Fact]
        public void sellMultipleItems()
        {
            SellItemRequest saladRequest = new SellItemRequest();
            saladRequest.setProductName("salad");
            saladRequest.setQuantity(2);

            SellItemRequest tomatoRequest = new SellItemRequest();
            tomatoRequest.setProductName("tomato");
            tomatoRequest.setQuantity(3);

            SellItemsRequest request = new SellItemsRequest();
            request.setRequests(new List<SellItemRequest>());
            request.getRequests().Add(saladRequest);
            request.getRequests().Add(tomatoRequest);

            useCase.run(request);

            Order insertedOrder = orderRepository.getSavedOrder();
            Assert.Equal(OrderStatus.CREATED, insertedOrder.getStatus());
            Assert.Equal(new decimal(23.20), insertedOrder.getTotal());
            Assert.Equal(new decimal(2.13), insertedOrder.getTax());
            Assert.Equal("EUR", insertedOrder.getCurrency());
            Assert.Equal(2, insertedOrder.getItems().Count);


            static void Assertions(OrderItem product0, string name, decimal price, int quantity, decimal taxedAmount, decimal tax)
            {
                Assert.Equal(name, product0.getProduct().getName());
                Assert.Equal(price, product0.getProduct().getPrice());
                Assert.Equal(quantity, product0.getQuantity());
                Assert.Equal(taxedAmount, product0.getTaxedAmount());
                Assert.Equal(tax, product0.getTax());
            }

            Assertions(insertedOrder.getItems().ElementAt(0), "salad", 3.56m, 2, 7.84m, 0.72m);
            Assertions(insertedOrder.getItems().ElementAt(1), "tomato", 4.65m, 3, 15.36m, 1.41m);
        }

        [Fact]
        public void unknownProduct()
        {
            SellItemsRequest request = new SellItemsRequest();
            request.setRequests(new List<SellItemRequest>());
            SellItemRequest unknownProductRequest = new SellItemRequest();
            unknownProductRequest.setProductName("unknown product");
            request.getRequests().Add(unknownProductRequest);

            Assert.Throws<UnknownProductException>(() => useCase.run(request));
        }
    }
}