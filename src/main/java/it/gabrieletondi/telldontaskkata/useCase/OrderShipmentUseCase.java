package it.gabrieletondi.telldontaskkata.useCase;

import com.sun.tools.corba.se.idl.constExpr.Or;
import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
import it.gabrieletondi.telldontaskkata.repository.OrderRepository;
import it.gabrieletondi.telldontaskkata.service.ShipmentService;

import static it.gabrieletondi.telldontaskkata.domain.OrderStatus.REJECTED;

public class OrderShipmentUseCase {
    private final OrderRepository orderRepository;
    private final ShipmentService shipmentService;

    public OrderShipmentUseCase(OrderRepository orderRepository, ShipmentService shipmentService) {
        this.orderRepository = orderRepository;
        this.shipmentService = shipmentService;
    }

    public void run(OrderShipmentRequest request) {
        final Order order = orderRepository.getById(request.getOrderId());

        if (order.getStatus().equals(OrderStatus.CREATED) || order.getStatus().equals(REJECTED)) {
            throw new OrderCannotBeShippedException();
        }

        shipmentService.ship(order);

        order.setStatus(OrderStatus.SHIPPED);
        orderRepository.save(order);
    }
}
