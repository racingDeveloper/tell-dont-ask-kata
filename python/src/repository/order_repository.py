from abc import abstractmethod, ABC

from ..domain.order import Order


class OrderRepository(ABC):
    @abstractmethod
    def save(self, order: Order) -> None:
        pass

    @abstractmethod
    def get_by_id(self, order_id: int) -> Order:
        pass
