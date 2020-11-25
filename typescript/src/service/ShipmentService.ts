import Order from "../domain/Order";

export default interface ShipmentService {
    ship(order: Order): void;
}