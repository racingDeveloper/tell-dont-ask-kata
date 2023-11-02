from ...src.service.shipment_service import ShipmentService


class TestShipmentService(ShipmentService):
    def __init__(self):
        self.shipped_order = None

    def ship(self, order):
        self.shipped_order = order
