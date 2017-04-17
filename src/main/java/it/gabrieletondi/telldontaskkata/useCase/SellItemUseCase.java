package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderItem;
import it.gabrieletondi.telldontaskkata.domain.Product;
import it.gabrieletondi.telldontaskkata.repository.OrderRepository;
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog;

import java.math.BigDecimal;
import java.util.ArrayList;

public class SellItemUseCase {
    private final OrderRepository orderRepository;
    private final ProductCatalog productCatalog;

    public SellItemUseCase(OrderRepository orderRepository, ProductCatalog productCatalog) {
        this.orderRepository = orderRepository;
        this.productCatalog = productCatalog;
    }

    public void run(SellItemRequest request) {
        Order order = new Order();

        Product product = productCatalog.getByName(request.getProductName());

        order.setItems(new ArrayList<>());

        order.setCurrency("EUR");
        order.setTotal(product.getPrice().multiply(BigDecimal.valueOf(request.getQuantity())));
        final OrderItem orderItem = new OrderItem();
        orderItem.setProduct(product);
        orderItem.setQuantity(request.getQuantity());
        order.getItems().add(orderItem);

        orderRepository.save(order);
    }
}
