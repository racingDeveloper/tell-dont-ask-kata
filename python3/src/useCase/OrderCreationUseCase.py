from decimal import Decimal, ROUND_HALF_UP

from src.domain.Order import Order
from src.domain.OrderItem import OrderItem
from src.domain.OrderStatus import OrderStatus
from src.repository.OrderRepository import OrderRepository
from src.repository.ProductCatalog import ProductCatalog
from src.useCase.SellItemsRequest import SellItemsRequest
from src.useCase.UnknownProductError import UnknownProductError


class OrderCreationUseCase:
    def __init__(self, order_repository: OrderRepository, product_catalog: ProductCatalog):
        self.order_repository = order_repository
        self.product_catalog = product_catalog

    def run(self, request: SellItemsRequest):
        order = Order()
        order.set_status(OrderStatus.CREATED)
        order.set_items([])
        order.set_currency("EUR")
        order.set_total(Decimal("0.00"))
        order.set_tax(Decimal("0.00"))

        for item_request in request.get_requests():
            product = self.product_catalog.get_by_name(item_request.get_product_name())

            if product is None:
                raise UnknownProductError()
            else:
                unitary_tax = Decimal(product.get_price() / Decimal(100) * product.get_category().get_tax_percentage()).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
                unitary_taxed_amount = Decimal(product.get_price() + unitary_tax).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
                taxed_amount = Decimal(unitary_taxed_amount * Decimal(item_request.get_quantity())).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
                tax_amount = unitary_tax * (Decimal(item_request.get_quantity()))

                orderItem = OrderItem()
                orderItem.set_product(product)
                orderItem.set_quantity(item_request.get_quantity())
                orderItem.set_tax(tax_amount)
                orderItem.set_taxed_amount(taxed_amount)
                order.get_items().append(orderItem)

                order.set_total(order.get_total() + taxed_amount)
                order.set_tax(order.get_tax() + tax_amount)

        self.order_repository.save(order)
