from dataclasses import dataclass


@dataclass
class SellItemRequest:
    quantity: int = 0
    product_name: str = ""
