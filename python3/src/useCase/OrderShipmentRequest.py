class OrderShipmentRequest(object):
    def set_order_id(self, order_id: int):
        self.order_id = order_id

    def get_order_id(self):
        return self.order_id
