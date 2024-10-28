import Order from '../../src/domain/Order';
import { OrderStatus } from '../../src/domain/OrderStatus';
import ApprovedOrderCannotBeRejectedException from '../../src/exception/ApprovedOrderCannotBeRejectedException';
import OrderApprovalRequest from '../../src/request/OrderApprovalRequest';
import OrderApprovalUseCase from '../../src/useCase/OrderApprovalUseCase';
import RejectedOrderCannotBeApprovedException from '../../src/exception/RejectedOrderCannotBeApprovedException';
import ShippedOrdersCannotBeChangedException from '../../src/exception/ShippedOrdersCannotBeChangedException';
import TestOrderRepository from '../doubles/TestOrderRepository';

describe('OrderApprovalUseCase', () => {
  let orderRepository: TestOrderRepository;
  let useCase: OrderApprovalUseCase;

  beforeEach( () => {
    orderRepository = new TestOrderRepository();
    useCase = new OrderApprovalUseCase(orderRepository);
  });
  it('approvedExistingOrder', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.CREATED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(true);

    useCase.run(request);

    const savedOrder: Order = orderRepository.getSavedOrder();
    expect(savedOrder.getStatus()).toBe(OrderStatus.APPROVED);
  });

  it('rejectedExistingOrder', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.CREATED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(false);

    useCase.run(request);

    const savedOrder: Order = orderRepository.getSavedOrder();
    expect(savedOrder.getStatus()).toBe(OrderStatus.REJECTED);
  });

  it('cannotApproveRejectedOrder', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.REJECTED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(true);

    expect(() => useCase.run(request)).toThrow(RejectedOrderCannotBeApprovedException);
    expect(orderRepository.getSavedOrder()).toBe(null);
  });

  it('cannotRejectApprovedOrder', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.APPROVED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(false);

    expect(() =>  useCase.run(request)).toThrow(ApprovedOrderCannotBeRejectedException);
    expect(orderRepository.getSavedOrder()).toBe(null);
  });

  it('shippedOrdersCannotBeApproved', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.SHIPPED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(true);

    expect(() => useCase.run(request)).toThrow(ShippedOrdersCannotBeChangedException);
    expect(orderRepository.getSavedOrder()).toBe(null);
  });

  it('shippedOrdersCannotBeRejected', () => {
    const initialOrder: Order = new Order();
    initialOrder.setStatus(OrderStatus.SHIPPED);
    orderRepository.addOrder(initialOrder);

    const request: OrderApprovalRequest = new OrderApprovalRequest();
    request.setOrderId(initialOrder.getId());
    request.setApproved(false);

    expect(() => useCase.run(request)).toThrow(ShippedOrdersCannotBeChangedException);
    expect(orderRepository.getSavedOrder()).toBe(null);
  });
});
