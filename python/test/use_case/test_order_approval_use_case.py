import pytest

from python.src.domain.order import Order
from python.src.domain.order_status import OrderStatus
from python.src.use_case.exceptions import ApprovedOrderCannotBeRejectedException, \
    ShippedOrdersCannotBeChangedException, RejectedOrderCannotBeApprovedException
from python.src.use_case.order_approval_request import OrderApprovalRequest
from python.src.use_case.order_approval_use_case import OrderApprovalUseCase
from python.test.doubles.test_order_repository import TestOrderRepository


class TestOrderApprovalUseCase:
    order_repository: TestOrderRepository
    use_case: OrderApprovalUseCase

    def setup_method(self):
        self.order_repository = TestOrderRepository()
        self.use_case = OrderApprovalUseCase(self.order_repository)

    def test_approved_existing_order(self):
        initial_order = Order(id=1, status=OrderStatus.CREATED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=True)

        self.use_case.run(request)

        saved_order = self.order_repository.inserted_order
        assert saved_order.status == OrderStatus.APPROVED

    def test_rejected_existing_order(self):
        initial_order = Order(id=1, status=OrderStatus.CREATED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=False)

        self.use_case.run(request)

        saved_order = self.order_repository.inserted_order
        assert saved_order.status == OrderStatus.REJECTED

    def test_cannot_approve_rejected_order(self):
        initial_order = Order(id=1, status=OrderStatus.REJECTED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=True)

        with pytest.raises(RejectedOrderCannotBeApprovedException):
            self.use_case.run(request)

        assert self.order_repository.inserted_order is None

    def test_cannot_reject_approved_order(self):
        initial_order = Order(id=1, status=OrderStatus.APPROVED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=False)

        with pytest.raises(ApprovedOrderCannotBeRejectedException):
            self.use_case.run(request)

        assert self.order_repository.inserted_order is None

    def test_shipped_orders_cannot_be_approved(self):
        initial_order = Order(id=1, status=OrderStatus.SHIPPED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=True)

        with pytest.raises(ShippedOrdersCannotBeChangedException):
            self.use_case.run(request)

        assert self.order_repository.inserted_order is None

    def test_shipped_orders_cannot_be_rejected(self):
        initial_order = Order(id=1, status=OrderStatus.SHIPPED)
        self.order_repository.add_order(initial_order)

        request = OrderApprovalRequest(order_id=1, approved=False)

        with pytest.raises(ShippedOrdersCannotBeChangedException):
            self.use_case.run(request)

        assert self.order_repository.inserted_order is None
