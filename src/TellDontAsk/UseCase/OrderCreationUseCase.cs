
using System.Collections.Generic;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.UseCase
{
    public class OrderCreationUseCase
    {
        private OrderRepository orderRepository;
        private ProductCatalog productCatalog;

        public OrderCreationUseCase(OrderRepository orderRepository, ProductCatalog productCatalog)
        {
            this.orderRepository = orderRepository;
            this.productCatalog = productCatalog;
        }

        public void run(SellItemsRequest request)
        {
            Order order = new Order();
            order.setStatus(OrderStatus.CREATED);
            order.setItems(new List<OrderItem>());
            order.setCurrency("EUR");
            order.setTotal(new decimal(0));
            order.setTax(new decimal(0));

            foreach (SellItemRequest itemRequest in request.getRequests())
            {
                Product product = productCatalog.getByName(itemRequest.getProductName());

                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else
                {
                    decimal unitaryTax = product.getPrice().divide(100).multiply(product.getCategory().getTaxPercentage()).setScale(2);
                    decimal unitaryTaxedAmount = product.getPrice().add(unitaryTax).setScale(2);
                    decimal taxedAmount = unitaryTaxedAmount.multiply(new decimal(itemRequest.getQuantity())).setScale(2);
                    decimal taxAmount = unitaryTax.multiply(new decimal(itemRequest.getQuantity()));

                    OrderItem orderItem = new OrderItem();
                    orderItem.setProduct(product);
                    orderItem.setQuantity(itemRequest.getQuantity());
                    orderItem.setTax(taxAmount);
                    orderItem.setTaxedAmount(taxedAmount);
                    order.getItems().Add(orderItem);

                    order.setTotal(order.getTotal().add(taxedAmount));
                    order.setTax(order.getTax().add(taxAmount));
                }
            }

            orderRepository.save(order);
        }
    }
}
