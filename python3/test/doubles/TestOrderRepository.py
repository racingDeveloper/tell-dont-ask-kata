from src.domain.Order import Order
from src.repository.OrderRepository import OrderRepository


class TestOrderRepository(OrderRepository):
    def __init__(self):
        self.orders = []
        self.inserted_order = None

    def save(self, order: Order):
        self.inserted_order = order

    def get_by_id(self, order_id: int):
        return next(filter(lambda order: order.get_id() is order_id, self.orders), None)

    def get_saved_order(self):
        return self.inserted_order

    def add_order(self, order: Order):
        self.orders.append(order)
