import Order from "../../src/domain/Order";
import ShipmentService from "../../src/service/ShipmentService";

export default class TestShipmentService implements ShipmentService {
    private _shippedOrder: Order;

    ship(order: Order): void {
        this._shippedOrder = order;
    }

    public getShippedOrder(): Order {
        return this._shippedOrder;
    }
}