import decimal


class Category(object):
    def __init__(self):
        self.name = ""
        self.tax_percentage = decimal.Decimal("0.00")

    def get_name(self):
        return self.name

    def set_name(self, name: str):
        self.name = name

    def get_tax_percentage(self):
        return self.tax_percentage

    def set_tax_percentage(self, tax_percentage: decimal.Decimal):
        self.tax_percentage = tax_percentage
