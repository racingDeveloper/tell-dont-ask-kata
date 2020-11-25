import OrderRepository from "../repository/OrderRepository";
import ProductCatalog from "../repository/ProductCatalog";
import SellItemsRequest from "./SellItemsRequest";
import Order from "../domain/Order";
import UnknownProductException from "./UnknownProductException";
import OrderItem from "../domain/OrderItem";
import OrderStatus from "../domain/OrderStatus";
import bigDecimal from "js-big-decimal";

export default class OrderCreationUseCase {
    private readonly _orderRepository: OrderRepository;
    private readonly _productCatalog: ProductCatalog;

    constructor(orderRepository: OrderRepository, productCatalog: ProductCatalog) {
        this._orderRepository = orderRepository;
        this._productCatalog = productCatalog;
    }

    public run(request: SellItemsRequest): void {
        let order = new Order();
        order.status = OrderStatus.CREATED;
        order.items = [];
        order.currency = "EUR";
        order.total = new bigDecimal("0.00");
        order.tax = new bigDecimal("0.00");

        for (let itemRequest of request.requests) {
            let product = this._productCatalog.getByName(itemRequest.productName);

            if (product === undefined) {
                throw new UnknownProductException()
            } else {
                const unitaryTax = product.price.divide(new bigDecimal(100)).multiply(product.category.taxPercentage).round(2);
                const unitaryTaxedAmount = product.price.add(unitaryTax).round(2);
                const taxedAmount = unitaryTaxedAmount.multiply(new bigDecimal(itemRequest.quantity)).round(2);
                const taxAmount = unitaryTax.multiply(new bigDecimal(itemRequest.quantity));

                let orderItem = new OrderItem();
                orderItem.product = product;
                orderItem.quantity = itemRequest.quantity;
                orderItem.tax = taxAmount;
                orderItem.taxedAmount = taxedAmount;
                order.items.push(orderItem);

                order.total = order.total.add(taxedAmount);
                order.tax = order.tax.add(taxAmount);
            }
        }
        this._orderRepository.save(order);
    }
}