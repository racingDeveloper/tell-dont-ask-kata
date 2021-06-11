import Order from '../domain/Order';
import { OrderStatus } from '../domain/OrderStatus';
import OrderRepository from '../repository/OrderRepository';
import ApprovedOrderCannotBeRejectedException from './ApprovedOrderCannotBeRejectedException';
import OrderApprovalRequest from './OrderApprovalRequest';
import RejectedOrderCannotBeApprovedException from './RejectedOrderCannotBeApprovedException';
import ShippedOrdersCannotBeChangedException from './ShippedOrdersCannotBeChangedException';

class OrderApprovalUseCase {
  private readonly orderRepository: OrderRepository;

  public constructor(orderRepository: OrderRepository){
      this.orderRepository = orderRepository;
  }

  public run(request: OrderApprovalRequest): void {
      const order: Order = this.orderRepository.getById(request.getOrderId());

      if (order.getStatus() === OrderStatus.SHIPPED) {
          throw new ShippedOrdersCannotBeChangedException();
      }

      if (request.isApproved() && order.getStatus() === OrderStatus.REJECTED) {
          throw new RejectedOrderCannotBeApprovedException();
      }

      if (!request.isApproved() && order.getStatus() === OrderStatus.APPROVED) {
          throw new ApprovedOrderCannotBeRejectedException();
      }

      order.setStatus(request.isApproved() ? OrderStatus.APPROVED : OrderStatus.REJECTED);
      this.orderRepository.save(order);
  }
}

export default OrderApprovalUseCase
