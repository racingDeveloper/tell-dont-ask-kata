using Deveel.Math;
using System.Collections.Generic;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

//import static java.math.BigDecimal.valueOf;
//import static java.math.RoundingMode.HALF_UP;

namespace TellDontAsk.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly OrderRepository orderRepository;
        private readonly ProductCatalog productCatalog;

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
            order.setTotal(new BigDecimal(0.0));
            order.setTax(new BigDecimal(0.0));

            foreach (SellItemRequest itemRequest in request.getRequests())
            {
                Product product = productCatalog.getByName(itemRequest.getProductName());

                if (product == null)
                {
                    throw new UnknownProductException();
                }
                else
                {
                    BigDecimal unitaryTax = product.getPrice().Divide(BigDecimal.ValueOf(100)).Multiply(product.getCategory().getTaxPercentage()).SetScale(2, RoundingMode.HalfUp);
                    BigDecimal unitaryTaxedAmount = product.getPrice().Add(unitaryTax).SetScale(2, RoundingMode.HalfUp);
                    BigDecimal taxedAmount = unitaryTaxedAmount.Multiply(BigDecimal.ValueOf(itemRequest.getQuantity())).SetScale(2, RoundingMode.HalfUp);
                    BigDecimal taxAmount = unitaryTax.Multiply(BigDecimal.ValueOf(itemRequest.getQuantity()));

                    OrderItem orderItem = new OrderItem();
                    orderItem.setProduct(product);
                    orderItem.setQuantity(itemRequest.getQuantity());
                    orderItem.setTax(taxAmount);
                    orderItem.setTaxedAmount(taxedAmount);
                    order.getItems().Add(orderItem);

                    order.setTotal(order.getTotal().Add(taxedAmount));
                    order.setTax(order.getTax().Add(taxAmount));
                }
            }

            orderRepository.save(order);
        }
    }
}
