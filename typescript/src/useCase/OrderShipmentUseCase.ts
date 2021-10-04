import Order from '../domain/Order';
import { OrderStatus } from '../domain/OrderStatus';
import OrderRepository from '../repository/OrderRepository';
import { ShipmentService } from '../service/ShipmentService';
import OrderCannotBeShippedException from './OrderCannotBeShippedException';
import OrderCannotBeShippedTwiceException from './OrderCannotBeShippedTwiceException';
import OrderShipmentRequest from './OrderShipmentRequest';

class OrderShipmentUseCase {
  private readonly orderRepository: OrderRepository;
  private readonly shipmentService: ShipmentService;

  public constructor (orderRepository: OrderRepository, shipmentService: ShipmentService) {
    this.orderRepository = orderRepository;
    this.shipmentService = shipmentService;
  }

  public run(request: OrderShipmentRequest): void {
    const order: Order = this.orderRepository.getById(request.getOrderId());

    if (order.getStatus() === OrderStatus.CREATED || order.getStatus() === OrderStatus.REJECTED) {
      throw new OrderCannotBeShippedException();
    }

    if (order.getStatus() === OrderStatus.SHIPPED) {
      throw new OrderCannotBeShippedTwiceException();
    }

    this.shipmentService.ship(order);

    order.setStatus(OrderStatus.SHIPPED);
    this.orderRepository.save(order);
  }
}

export default OrderShipmentUseCase;
