import Product from './Product';
import SellItemRequest from '../request/SellItemRequest';

class OrderItem {
  private readonly product: Product;
  private quantity: number;
  private readonly taxedAmount: number;
  private readonly tax: number;

  constructor(product: Product, itemRequest: SellItemRequest) {
    this.product = product;
    this.quantity = itemRequest.getQuantity();
    this.tax = product.getUnitaryTax() * this.quantity;
    this.taxedAmount = product.getUnitaryTaxedAmount() * this.quantity;
  }

  public getProduct(): Product {
    return this.product;
  }

  public getQuantity(): number {
    return this.quantity;
  }

  public setQuantity(quantity: number): void {
    this.quantity = quantity;
  }

  public getTaxedAmount(): number {
    return this.taxedAmount;
  }

  public getTax(): number {
    return this.tax;
  }
}

export default OrderItem;

