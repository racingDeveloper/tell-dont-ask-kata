from dataclasses import dataclass, field

from .product import Product


@dataclass
class OrderItem:
    product: Product = field(default_factory=Product)
    quantity: int = 0
    taxed_amount: float = 0
    tax: float = 0
