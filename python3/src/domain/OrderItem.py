import decimal

from src.domain.Product import Product


class OrderItem(object):
    def get_product(self):
        return self.product

    def set_product(self, product: Product):
        self.product = product

    def get_quantity(self):
        return self.quantity

    def set_quantity(self, quantity: int):
        self.quantity = quantity

    def get_taxed_amount(self):
        return self.taxed_amount

    def set_taxed_amount(self, taxed_amount: decimal.Decimal):
        self.taxed_amount = taxed_amount

    def get_tax(self):
        return self.tax

    def set_tax(self, tax: decimal.Decimal):
        self.tax = tax
