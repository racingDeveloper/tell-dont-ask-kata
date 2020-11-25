import decimal

from src.domain.Category import Category


class Product(object):
    def get_name(self):
        return self.name

    def set_name(self, name: str):
        self.name = name

    def get_price(self):
        return self.price

    def set_price(self, price: decimal.Decimal):
        self.price = price

    def get_category(self):
        return self.category

    def set_category(self, category: Category):
        self.category = category
