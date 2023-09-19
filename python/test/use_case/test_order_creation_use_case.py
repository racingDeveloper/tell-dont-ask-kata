import pytest

from python.src.domain.category import Category
from python.src.domain.order_status import OrderStatus
from python.src.domain.product import Product
from python.src.use_case.exceptions import UnknownProductException
from python.src.use_case.order_creation_use_case import OrderCreationUseCase
from python.src.use_case.sell_item_request import SellItemRequest
from python.src.use_case.sell_items_request import SellItemsRequest
from python.test.doubles.in_memory_product_catalog import InMemoryProductCatalog
from python.test.doubles.test_order_repository import TestOrderRepository


class TestOrderCreationUseCase:
    def setup_method(self):
        self.order_repository = TestOrderRepository()
        self.product_catalog = InMemoryProductCatalog([
            Product(name='salad', price=3.56, category=Category(name='food', tax_percentage=10)),
            Product(name='tomato', price=4.65, category=Category(name='food', tax_percentage=10))
        ])
        self.use_case = OrderCreationUseCase(self.order_repository, self.product_catalog)

    def test_sell_multiple_items(self):
        request = SellItemsRequest()
        request.requests.append(SellItemRequest(product_name='salad', quantity=2))
        request.requests.append(SellItemRequest(product_name='tomato', quantity=3))

        self.use_case.run(request)

        inserted_order = self.order_repository.inserted_order
        assert inserted_order.status == OrderStatus.CREATED
        assert inserted_order.total == 23.20
        assert inserted_order.tax == 2.13
        assert inserted_order.currency == 'EUR'
        assert len(inserted_order.items) == 2
        assert inserted_order.items[0].product.name == 'salad'
        assert inserted_order.items[0].product.price == 3.56
        assert inserted_order.items[0].quantity == 2
        assert inserted_order.items[0].taxed_amount == 7.84
        assert inserted_order.items[0].tax == 0.72
        assert inserted_order.items[1].product.name == 'tomato'
        assert inserted_order.items[1].product.price == 4.65
        assert inserted_order.items[1].quantity == 3
        assert inserted_order.items[1].taxed_amount == 15.36
        assert inserted_order.items[1].tax == 1.41

    def test_unknown_product(self):
        request = SellItemsRequest()
        request.requests.append(SellItemRequest(product_name='unknown product', quantity=1))

        with pytest.raises(UnknownProductException):
            self.use_case.run(request)

