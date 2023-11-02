# rewrite the following code from typescript to python

from abc import abstractmethod, ABC

from ..domain.product import Product


class ProductCatalog(ABC):
    @abstractmethod
    def get_by_name(self, name: str) -> Product:
        pass
