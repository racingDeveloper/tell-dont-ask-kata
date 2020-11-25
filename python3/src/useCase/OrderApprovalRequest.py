class OrderApprovalRequest:
    def set_order_id(self, order_id: int):
        self.order_id = order_id

    def get_order_id(self):
        return self.order_id

    def set_approved(self, approved: bool):
        self.approved = approved

    def is_approved(self):
        return self.approved
