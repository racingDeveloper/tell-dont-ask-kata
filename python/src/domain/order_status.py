from enum import Enum, auto


class OrderStatus(Enum):
    APPROVED = auto()
    REJECTED = auto()
    SHIPPED = auto()
    CREATED = auto()
