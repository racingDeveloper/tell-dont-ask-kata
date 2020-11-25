class SellItemRequest(object):
    def set_quantity(self, quantity: int):
        self.quantity = quantity

    def get_quantity(self):
        return self.quantity

    def set_product_name(self, product_name):
        self.product_name = product_name

    def get_product_name(self):
        return self.product_name