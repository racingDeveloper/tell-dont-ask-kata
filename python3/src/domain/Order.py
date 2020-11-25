import decimal

from src.domain.OrderStatus import OrderStatus


class Order(object):
    def get_total(self):
        return self.total

    def set_total(self, total: decimal.Decimal):
        self.total = total

    def get_currency(self):
        return self.currency

    def set_currency(self, currency: str):
        self.currency = currency

    def get_items(self):
        return self.items

    def set_items(self, items: list):
        self.items = items

    def get_tax(self):
        return self.tax

    def set_tax(self, tax: decimal.Decimal):
        self.tax = tax

    def get_status(self):
        return self.status

    def set_status(self, status: OrderStatus):
        self.status = status

    def get_id(self):
        return self.id

    def set_id(self, id: int):
        self.id = id
