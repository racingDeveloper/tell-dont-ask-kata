package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderItem;
import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
import it.gabrieletondi.telldontaskkata.domain.Product;
import it.gabrieletondi.telldontaskkata.repository.OrderRepository;
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Optional;

import static java.math.BigDecimal.valueOf;
import static java.math.RoundingMode.HALF_UP;

public class OrderCreationUseCase {
    private final OrderRepository orderRepository;
    private final ProductCatalog productCatalog;

    public OrderCreationUseCase(OrderRepository orderRepository, ProductCatalog productCatalog) {
        this.orderRepository = orderRepository;
        this.productCatalog = productCatalog;
    }

    public void run(SellItemsRequest request) {

        Order order = new Order(new BigDecimal("0.00"), "EUR", new ArrayList<>(), new BigDecimal("0.00"), OrderStatus.CREATED);

        for (SellItemRequest itemRequest : request.getRequests()) {
            addOrderItemAndTax(order, itemRequest);
        }

        orderRepository.save(order);
    }

    private void addOrderItemAndTax(Order order, SellItemRequest itemRequest) {
        Product product = productCatalog.getByName(itemRequest.getProductName());

        Optional.ofNullable(product).orElseThrow(UnknownProductException::new);

        BigDecimal productPrice = product.getPrice();

        final BigDecimal unitaryTax = product.getUnitaryTax(productPrice);
        final BigDecimal unitaryTaxedAmount = productPrice.add(unitaryTax).setScale(2, HALF_UP);
        final BigDecimal taxedAmount = getTaxedAmount(unitaryTaxedAmount, BigDecimal.valueOf(itemRequest.getQuantity()))
                .setScale(2, HALF_UP);
        final BigDecimal taxAmount = getTaxedAmount(unitaryTax, BigDecimal.valueOf(itemRequest.getQuantity()));

        OrderItem orderItem = new OrderItem(product, itemRequest.getQuantity(), taxedAmount, taxAmount);
        order.addOrderItem(orderItem);
        order.addTotal(taxedAmount);
        order.addTax(taxAmount);
    }

    public BigDecimal getTaxedAmount(BigDecimal amount, BigDecimal quantity) {
        return amount.multiply(quantity);
    }


}
