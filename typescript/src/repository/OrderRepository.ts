import Order from "../domain/Order";

export default interface OrderRepository {
    save(order: Order): void;

    getById(id: number): Order;
}