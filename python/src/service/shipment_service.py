from abc import ABC, abstractmethod


class ShipmentService(ABC):
    @abstractmethod
    def ship(self, order):
        pass
