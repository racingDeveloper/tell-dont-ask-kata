import Order from '../domain/Order';

interface OrderRepository {
  save(order: Order): void;
  getById(orderId: number): Order;
}

export default OrderRepository;
