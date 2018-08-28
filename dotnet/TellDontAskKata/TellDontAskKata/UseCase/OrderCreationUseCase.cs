using System;
using System.Collections.Generic;
using TellDontAskKata.Domain;
using TellDontAskKata.Repository;

namespace TellDontAskKata.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductCatalog productCatalog;

        public OrderCreationUseCase(IOrderRepository orderRepository,
            IProductCatalog productCatalog)
        {
            this.orderRepository = orderRepository;
            this.productCatalog = productCatalog;
        }

        public void Run(SellItemsRequest request)
        {
            var order = new Order
            {
                Status = OrderStatus.Created,
                Items = new List<OrderItem>(),
                Currency = "EUR",
                Total = 0.0m,
                Tax = 0.0m
            };

            foreach (var itemRequest in request.Requests)
            {
                var product = productCatalog.GetByName(itemRequest.ProductName);
                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else

                {

                    var unitaryTax = Math.Round(product.Price / 100 * product.Category.TaxPercentage, 2,
                        MidpointRounding.AwayFromZero);

                    var unitaryTaxAmount = Math.Round(product.Price + unitaryTax, 2, MidpointRounding.AwayFromZero);

                    var taxedAmount = Math.Round(unitaryTaxAmount * itemRequest.Quantity, 2,
                        MidpointRounding.AwayFromZero);

                    var taxAmount = Math.Round(unitaryTax * itemRequest.Quantity, 2, MidpointRounding.AwayFromZero);

                    var orderItem = new OrderItem
                    {
                        Product = product,
                        Quantity = itemRequest.Quantity,
                        Tax = taxAmount,
                        TaxedAmount = taxedAmount
                    };

                    order.Items.Add(orderItem);
                    order.Total += taxedAmount;
                    order.Tax += taxAmount;
                }
            }
            orderRepository.Save(order);
        }
    }
}