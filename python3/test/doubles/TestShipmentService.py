from src.domain.Order import Order
from src.service.ShipmentService import ShipmentService


class TestShipmentService(ShipmentService):
    def __init__(self):
        self.shipped_order = None

    def ship(self, order: Order):
        self.shipped_order = order

    def get_shipped_order(self):
        return self.shipped_order
