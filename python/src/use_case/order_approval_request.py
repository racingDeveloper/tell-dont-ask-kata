from dataclasses import dataclass


@dataclass
class OrderApprovalRequest:
      order_id: int = 0
      approved: bool = False
