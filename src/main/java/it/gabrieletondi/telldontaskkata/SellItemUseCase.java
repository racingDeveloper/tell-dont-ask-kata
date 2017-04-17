package it.gabrieletondi.telldontaskkata;

import java.math.BigDecimal;

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

        order.setCurrency("EUR");
        order.setTotal(product.getPrice().multiply(BigDecimal.valueOf(request.getQuantity())));

        orderRepository.save(order);
    }
}
