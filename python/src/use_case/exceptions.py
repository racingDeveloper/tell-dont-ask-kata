class ApprovedOrderCannotBeRejectedException(Exception):
    pass


class OrderCannotBeShippedException(Exception):
    pass


class OrderCannotBeShippedTwiceException(Exception):
    pass


class RejectedOrderCannotBeApprovedException(Exception):
    pass


class ShippedOrdersCannotBeChangedException(Exception):
    pass


class UnknownProductException(Exception):
    pass
