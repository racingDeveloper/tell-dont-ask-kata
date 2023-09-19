from dataclasses import dataclass


@dataclass
class Category:
    name: str = ""
    tax_percentage: float = 0
