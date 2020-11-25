from src.repository.ProductCatalog import ProductCatalog


class InMemoryProductCatalog(ProductCatalog):
    def __init__(self, products: list):
        self.products = products

    def get_by_name(self, name: str):
        return next(filter(lambda product: product.get_name() is name, self.products), None)
