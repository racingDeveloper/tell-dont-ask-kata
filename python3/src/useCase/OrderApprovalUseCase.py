from src.domain.OrderStatus import OrderStatus
from src.repository.OrderRepository import OrderRepository
from src.useCase.ApprovedOrderCannotBeRejectedError import ApprovedOrderCannotBeRejectedError
from src.useCase.OrderApprovalRequest import OrderApprovalRequest
from src.useCase.RejectedOrderCannotBeApprovedError import RejectedOrderCannotBeApprovedError
from src.useCase.ShippedOrdersCannotBeChangedError import ShippedOrdersCannotBeChangedError


class OrderApprovalUseCase:
    def __init__(self, order_repository: OrderRepository):
        self.order_repository = order_repository

    def run(self, request: OrderApprovalRequest):
        order = self.order_repository.get_by_id(request.get_order_id())

        if order.get_status() is OrderStatus.SHIPPED:
            raise ShippedOrdersCannotBeChangedError()

        if request.is_approved() and order.get_status() is OrderStatus.REJECTED:
            raise RejectedOrderCannotBeApprovedError()

        if not request.is_approved() and order.get_status() is OrderStatus.APPROVED:
            raise ApprovedOrderCannotBeRejectedError()

        order.set_status(OrderStatus.APPROVED if request.is_approved() else OrderStatus.REJECTED)
        self.order_repository.save(order)
