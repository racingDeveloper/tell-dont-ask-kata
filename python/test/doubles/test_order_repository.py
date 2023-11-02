from ...src.repository.order_repository import OrderRepository


class TestOrderRepository(OrderRepository):
    def __init__(self):
        self.inserted_order = None
        self.orders = []

    def save(self, order):
        self.inserted_order = order

    def get_by_id(self, order_id):
        return next((o for o in self.orders if o.id == order_id), None)

    def add_order(self, order):
        self.orders.append(order)
