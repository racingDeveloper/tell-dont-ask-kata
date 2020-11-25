from abc import ABCMeta, abstractmethod

from src.domain.Order import Order


class OrderRepository(metaclass=ABCMeta):
    def __init(self):
        raise NotImplementedError

    @abstractmethod
    def save(self, order: Order):
        pass

    @abstractmethod
    def get_by_id(self, order_id: int):
        pass
