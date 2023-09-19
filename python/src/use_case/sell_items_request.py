from dataclasses import dataclass, field

from .sell_item_request import SellItemRequest


@dataclass
class SellItemsRequest:
    requests: list[SellItemRequest] = field(default_factory=list)
