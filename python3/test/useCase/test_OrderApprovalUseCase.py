import unittest

from hamcrest import *

from src.domain.Order import Order
from src.domain.OrderStatus import OrderStatus
from src.useCase.ApprovedOrderCannotBeRejectedError import ApprovedOrderCannotBeRejectedError
from src.useCase.OrderApprovalRequest import OrderApprovalRequest
from src.useCase.OrderApprovalUseCase import OrderApprovalUseCase
from src.useCase.RejectedOrderCannotBeApprovedError import RejectedOrderCannotBeApprovedError
from src.useCase.ShippedOrdersCannotBeChangedError import ShippedOrdersCannotBeChangedError
from test.doubles.TestOrderRepository import TestOrderRepository


class TestOrderApprovalUseCase(unittest.TestCase):
    def setUp(self):
        self.order_repository = TestOrderRepository()
        self.use_case = OrderApprovalUseCase(self.order_repository)

    def test_approved_existing_Order(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.CREATED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(True)

        self.use_case.run(request)

        saved_order = self.order_repository.get_saved_order()
        assert_that(saved_order.get_status(), is_(OrderStatus.APPROVED))

    def test_rejected_existing_order(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.CREATED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(False)

        self.use_case.run(request)

        saved_order = self.order_repository.get_saved_order()
        assert_that(saved_order.get_status(), is_(OrderStatus.REJECTED))

    def test_cannot_approve_rejected_order(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.REJECTED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(True)

        assert_that(calling(self.use_case.run).with_args(request), raises(RejectedOrderCannotBeApprovedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))

    def test_cannot_reject_approved_order(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.APPROVED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(False)

        assert_that(calling(self.use_case.run).with_args(request), raises(ApprovedOrderCannotBeRejectedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))

    def test_shipped_orders_cannot_be_approved(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.SHIPPED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(True)

        assert_that(calling(self.use_case.run).with_args(request), raises(ShippedOrdersCannotBeChangedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))

    def test_shipped_orders_cannot_be_rejected(self):
        initial_order = Order()
        initial_order.set_status(OrderStatus.SHIPPED)
        initial_order.set_id(1)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest()
        request.set_order_id(1)
        request.set_approved(False)

        assert_that(calling(self.use_case.run).with_args(request), raises(ShippedOrdersCannotBeChangedError))
        assert_that(self.order_repository.get_saved_order(), is_(none()))


if __name__ == '__main__':
    unittest.main()
