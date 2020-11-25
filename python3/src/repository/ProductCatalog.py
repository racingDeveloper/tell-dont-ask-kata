from abc import ABCMeta, abstractmethod


class ProductCatalog(metaclass=ABCMeta):
    def __init(self):
        raise NotImplementedError

    @abstractmethod
    def get_by_name(self, name: str):
        pass
