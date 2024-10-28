import Category from '../../src/domain/Category';
import Order from '../../src/domain/Order';
import { OrderStatus } from '../../src/domain/OrderStatus';
import Product from '../../src/domain/Product';
import { ProductCatalog } from '../../src/repository/ProductCatalog';
import OrderCreationUseCase from '../../src/useCase/OrderCreationUseCase';
import SellItemRequest from '../../src/request/SellItemRequest';
import SellItemsRequest from '../../src/request/SellItemsRequest';
import UnknownProductException from '../../src/exception/UnknownProductException';
import InMemoryProductCatalog from '../doubles/InMemoryProductCatalog';
import TestOrderRepository from '../doubles/TestOrderRepository';

describe('OrderApprovalUseCase', () => {
  const orderRepository: TestOrderRepository = new TestOrderRepository();
  const food: Category = new Category();
  food.setName('food');
  food.setTaxPercentage(10);

  const saladProduct = new Product();
  saladProduct.setName('salad');
  saladProduct.setPrice(3.56);
  saladProduct.setCategory(food);
  const tomatoProduct = new Product();
  tomatoProduct.setName('tomato');
  tomatoProduct.setPrice(4.65);
  tomatoProduct.setCategory(food);
  const productCatalog: ProductCatalog = new InMemoryProductCatalog([ saladProduct, tomatoProduct]);
  const useCase: OrderCreationUseCase = new OrderCreationUseCase(orderRepository, productCatalog);

  it('sellMultipleItems', () => {
    const saladRequest: SellItemRequest = new SellItemRequest();
    saladRequest.setProductName('salad');
    saladRequest.setQuantity(2);

    const tomatoRequest: SellItemRequest = new SellItemRequest();
    tomatoRequest.setProductName('tomato');
    tomatoRequest.setQuantity(3);

    const request: SellItemsRequest = new SellItemsRequest();
    request.setRequests([]);
    request.getRequests().push(saladRequest);
    request.getRequests().push(tomatoRequest);

    useCase.run(request);

    const insertedOrder: Order = orderRepository.getSavedOrder();
    expect(insertedOrder.getStatus()).toBe(OrderStatus.CREATED);
    expect(insertedOrder.getTotal()).toBe(23.20);
    expect(insertedOrder.getTax()).toBe((2.13));
    expect(insertedOrder.getCurrency()).toBe(('EUR'));
    expect(insertedOrder.getItems().length).toBe(2);
    expect(insertedOrder.getItems()[0].getProduct().getName()).toBe('salad');
    expect(insertedOrder.getItems()[0].getProduct().getPrice()).toBe(3.56);
    expect(insertedOrder.getItems()[0].getQuantity()).toBe(2);
    expect(insertedOrder.getItems()[0].getTaxedAmount()).toBe(7.84);
    expect(insertedOrder.getItems()[0].getTax()).toBe(0.72);
    expect(insertedOrder.getItems()[1].getProduct().getName()).toBe('tomato');
    expect(insertedOrder.getItems()[1].getProduct().getPrice()).toBe(4.65);
    expect(insertedOrder.getItems()[1].getQuantity()).toBe(3);
    expect(insertedOrder.getItems()[1].getTaxedAmount()).toBe(15.36);
    expect(insertedOrder.getItems()[1].getTax()).toBe(1.41);
  });

  it('unknownProduct', () => {
    const request: SellItemsRequest = new SellItemsRequest();
    request.setRequests([]);
    const unknownProductRequest: SellItemRequest = new SellItemRequest();
    unknownProductRequest.setProductName('unknown product');
    request.getRequests().push(unknownProductRequest);

    expect(() => useCase.run(request)).toThrow(UnknownProductException);
  });
});
