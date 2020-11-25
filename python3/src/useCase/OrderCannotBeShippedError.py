class OrderCannotBeShippedError(Exception):
    def __repr__(self):
        return "OrderCannotBeShippedError"
