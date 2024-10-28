import Order from '../domain/Order';

export interface ShipmentService {
  ship(order: Order): void;
}
