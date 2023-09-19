from ...src.repository.product_catalog import ProductCatalog


class InMemoryProductCatalog(ProductCatalog):
    def __init__(self, products):
        self.products = products

    def get_by_name(self, name):
        return next((p for p in self.products if p.name == name), None)
