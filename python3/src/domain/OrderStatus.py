from enum import Enum


class OrderStatus(Enum):
    APPROVED = 1
    REJECTED = 2
    SHIPPED = 3
    CREATED = 4
