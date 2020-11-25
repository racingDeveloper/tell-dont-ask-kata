class OrderCannotBeShippedTwiceError(Exception):
    def __repr__(self):
        return "OrderCannotBeShippedTwiceException"