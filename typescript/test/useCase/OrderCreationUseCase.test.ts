import TestOrderRepository from "../doubles/TestOrderRepository";
import Category from "../../src/domain/Category";
import InMemoryProductCatalog from "../doubles/InMemoryProductCatalog";
import Product from "../../src/domain/Product";
import OrderCreationUseCase from "../../src/useCase/OrderCreationUseCase";
import SellItemRequest from "../../src/useCase/SellItemRequest";
import SellItemsRequest from "../../src/useCase/SellItemsRequest";
import UnknownProductException from "../../src/useCase/UnknownProductException";
import OrderStatus from "../../src/domain/OrderStatus";
import bigDecimal from "js-big-decimal";

describe('OrderCreationUseCase should', () => {
    let orderRepository: TestOrderRepository;
    let productCatalog: InMemoryProductCatalog;
    let useCase: OrderCreationUseCase;
    beforeEach(() => {
        orderRepository = new TestOrderRepository();

        let food = new Category();
        food.name = "food";
        food.taxPercentage = new bigDecimal("10");

        let product1 = new Product();
        product1.name = "salad";
        product1.price = new bigDecimal("3.56");
        product1.category = food;
        let product2 = new Product();
        product2.name = "tomato";
        product2.price = new bigDecimal("4.65");
        product2.category = food;

        productCatalog = new InMemoryProductCatalog([product1, product2]);
        useCase = new OrderCreationUseCase(orderRepository, productCatalog);
    });

    test('sell multiple items', () => {
        let saladRequest = new SellItemRequest();
        saladRequest.productName = "salad";
        saladRequest.quantity = 2;

        let tomatoRequest = new SellItemRequest();
        tomatoRequest.productName = "tomato";
        tomatoRequest.quantity = 3;

        let request = new SellItemsRequest();
        request.requests = [saladRequest, tomatoRequest];

        useCase.run(request);

        const insertedOrder = orderRepository.getSavedOrder();
        expect(insertedOrder.status).toBe(OrderStatus.CREATED);
        expect(insertedOrder.total).toEqual(new bigDecimal("23.17"));
        expect(insertedOrder.tax).toEqual(new bigDecimal("2.10"));
        expect(insertedOrder.currency).toBe("EUR");
        expect(insertedOrder.items.length).toBe(2);
        expect(insertedOrder.items[0].product.name).toBe("salad");
        expect(insertedOrder.items[0].product.price).toEqual(new bigDecimal("3.56"));
        expect(insertedOrder.items[0].quantity).toBe(2);
        expect(insertedOrder.items[0].taxedAmount).toEqual(new bigDecimal("7.84"));
        expect(insertedOrder.items[0].tax).toEqual(new bigDecimal("0.72"));
        expect(insertedOrder.items[1].product.name).toEqual("tomato");
        expect(insertedOrder.items[1].product.price).toEqual(new bigDecimal("4.65"));
        expect(insertedOrder.items[1].quantity).toBe(3);
        expect(insertedOrder.items[1].taxedAmount).toEqual(new bigDecimal("15.33"));
        expect(insertedOrder.items[1].tax).toEqual(new bigDecimal("1.38"));
    });

    test('unknown product', () => {
        let request = new SellItemsRequest();
        request.requests = [];
        let unknownProductRequest = new SellItemRequest();
        unknownProductRequest.productName = "unknown product";
        request.requests.push(unknownProductRequest);

        expect(() => {useCase.run(request)}).toThrowError(UnknownProductException);
    });
});