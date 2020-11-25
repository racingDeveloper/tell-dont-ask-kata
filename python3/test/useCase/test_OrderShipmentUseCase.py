import unittest

from hamcrest import is_, assert_that, calling, raises, none

from src.domain.Order import Order
from src.domain.OrderStatus import OrderStatus
from src.useCase.OrderCannotBeShippedError import OrderCannotBeShippedError
from src.useCase.OrderCannotBeShippedTwiceError import OrderCannotBeShippedTwiceError
from src.useCase.OrderShipmentRequest import OrderShipmentRequest
from src.useCase.OrderShipmentUseCase import OrderShipmentUseCase
from test.doubles.TestOrderRepository import TestOrderRepository
from test.doubles.TestShipmentService import TestShipmentService


class TestOrderShipmentUseCase(unittest.TestCase):
    def setUp(self):
        self.order_repository = TestOrderRepository()
        self.shipment_service = TestShipmentService()
        self.use_case = OrderShipmentUseCase(self.order_repository, self.shipment_service)

    def test_ship_approved_order(self):
        initial_order = Order()
        initial_order.set_id(1)
        initial_order.set_status(OrderStatus.APPROVED)
        self.order_repository.add_order(initial_order)

        request = OrderShipmentRequest()
        request.set_order_id(1)

        self.use_case.run(request)

        assert_that(self.order_repository.get_saved_order().get_status(), is_(OrderStatus.SHIPPED))
        assert_that(self.shipment_service.get_shipped_order(), is_(initial_order))

    def test_created_orders_cannot_be_shipped(self):
        initialOrder = Order()
        initialOrder.set_id(1)
        initialOrder.set_status(OrderStatus.CREATED)
        self.order_repository.add_order(initialOrder)

        request = OrderShipmentRequest()
        request.set_order_id(1)

        assert_that(calling(self.use_case.run).with_args(request), raises(OrderCannotBeShippedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))
        assert_that(self.shipment_service.get_shipped_order(), is_(none()))

    def test_rejected_orders_cannot_be_shipped(self):
        initialOrder = Order()
        initialOrder.set_id(1)
        initialOrder.set_status(OrderStatus.REJECTED)
        self.order_repository.add_order(initialOrder)

        request = OrderShipmentRequest()
        request.set_order_id(1)

        assert_that(calling(self.use_case.run).with_args(request), raises(OrderCannotBeShippedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))
        assert_that(self.shipment_service.get_shipped_order(), is_(none()))

    def test_shipped_orders_cannot_be_shipped_again(self):
        initialOrder = Order()
        initialOrder.set_id(1)
        initialOrder.set_status(OrderStatus.SHIPPED)
        self.order_repository.add_order(initialOrder)

        request = OrderShipmentRequest()
        request.set_order_id(1)

        assert_that(calling(self.use_case.run).with_args(request), raises(OrderCannotBeShippedTwiceError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))
        assert_that(self.shipment_service.get_shipped_order(), is_(none()))


if __name__ == '__main__':
    unittest.main()
