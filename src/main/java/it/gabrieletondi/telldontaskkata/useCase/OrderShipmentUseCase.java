package it.gabrieletondi.telldontaskkata.useCase;

import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
import it.gabrieletondi.telldontaskkata.repository.OrderRepository;
import it.gabrieletondi.telldontaskkata.service.ShipmentService;

import java.util.Optional;

import static it.gabrieletondi.telldontaskkata.domain.OrderStatus.CREATED;
import static it.gabrieletondi.telldontaskkata.domain.OrderStatus.REJECTED;
import static it.gabrieletondi.telldontaskkata.domain.OrderStatus.SHIPPED;

public class OrderShipmentUseCase {
    private final OrderRepository orderRepository;
    private final ShipmentService shipmentService;

    public OrderShipmentUseCase(OrderRepository orderRepository, ShipmentService shipmentService) {
        this.orderRepository = orderRepository;
        this.shipmentService = shipmentService;
    }

    public void run(OrderShipmentRequest request) {
        final Order order = Optional.ofNullable(orderRepository.getById(request.getOrderId()))
                .orElseThrow(() -> new RuntimeException("Order Id does not exist"));

        validateOrder(order);

        shipmentService.ship(order);

        order.setStatus(OrderStatus.SHIPPED);
        orderRepository.save(order);
    }

    public void validateOrder(Order order) {
        if (isOrderShippable(order)) {
            throw new OrderCannotBeShippedException();
        }

        if (order.isOrderStatus(SHIPPED)) {
            throw new OrderCannotBeShippedTwiceException();
        }
    }

    public boolean isOrderShippable(Order order){
        return order.isOrderStatus(CREATED) || order.isOrderStatus(REJECTED);
    }
}
