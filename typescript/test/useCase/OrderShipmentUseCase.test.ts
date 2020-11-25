import Order from "../../src/domain/Order";
import OrderShipmentRequest from "../../src/useCase/OrderShipmentRequest";
import TestOrderRepository from "../doubles/TestOrderRepository";
import OrderShipmentUseCase from "../../src/useCase/OrderShipmentUseCase";
import OrderCannotBeShippedException from "../../src/useCase/OrderCannotBeShippedException";
import OrderCannotBeShippedTwiceException from "../../src/useCase/OrderCannotBeShippedTwiceException";
import OrderStatus from "../../src/domain/OrderStatus";
import ShipmentService from "../../src/service/ShipmentService";
import OrderRepository from "../../src/repository/OrderRepository";
import TestShipmentService from "../doubles/TestShipmentService";

describe('OrderShipmentUseCase should', () => {
    const orderRepository: OrderRepository;
    const shipmentService: ShipmentService;
    const useCase: OrderShipmentUseCase;

    beforeEach(() => {
        orderRepository = new TestOrderRepository();
        shipmentService = new TestShipmentService();
        useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
    });

    test('ship approved order', () => {
        let initialOrder = new Order();
        initialOrder.id = 1;
        initialOrder.status = OrderStatus.APPROVED;
        orderRepository.addOrder(initialOrder);

        let request = new OrderShipmentRequest();
        request.orderId = 1;

        useCase.run(request);

        expect(orderRepository.getSavedOrder().status).toBe(OrderStatus.SHIPPED);
        expect(shipmentService.getShippedOrder()).toBe(initialOrder);
    });

    test('not ship created orders', () => {
        let initialOrder = new Order();
        initialOrder.id = 1;
        initialOrder.status = OrderStatus.CREATED;
        orderRepository.addOrder(initialOrder);

        let request = new OrderShipmentRequest();
        request.orderId = 1;

        expect(() => {
            useCase.run(request)
        }).toThrowError(OrderCannotBeShippedException);
        expect(orderRepository.getSavedOrder()).toBeUndefined();
        expect(shipmentService.getShippedOrder()).toBeUndefined();
    });

    test('not ship rejected orders', () => {
        let initialOrder = new Order();
        initialOrder.id = 1;
        initialOrder.status = OrderStatus.REJECTED;
        orderRepository.addOrder(initialOrder);

        let request = new OrderShipmentRequest();
        request.orderId = 1;

        expect(() => {
            useCase.run(request)
        }).toThrowError(OrderCannotBeShippedException);
        expect(orderRepository.getSavedOrder()).toBeUndefined();
        expect(shipmentService.getShippedOrder()).toBeUndefined();
    });

    test('not ship shipped orders', () => {
        let initialOrder = new Order();
        initialOrder.id = 1;
        initialOrder.status = OrderStatus.SHIPPED;
        orderRepository.addOrder(initialOrder);

        let request = new OrderShipmentRequest();
        request.orderId = 1;

        expect(() => {
            useCase.run(request)
        }).toThrowError(OrderCannotBeShippedTwiceException);
        expect(orderRepository.getSavedOrder()).toBeUndefined();
        expect(shipmentService.getShippedOrder()).toBeUndefined();
    });
});