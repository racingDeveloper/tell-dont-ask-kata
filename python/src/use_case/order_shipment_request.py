from dataclasses import dataclass


@dataclass
class OrderShipmentRequest:
    order_id: int = 0
