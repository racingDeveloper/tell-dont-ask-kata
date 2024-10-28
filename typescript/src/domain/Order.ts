import OrderItem from './OrderItem';
import {OrderStatus} from './OrderStatus';
import ShippedOrdersCannotBeChangedException from '../exception/ShippedOrdersCannotBeChangedException';
import RejectedOrderCannotBeApprovedException from '../exception/RejectedOrderCannotBeApprovedException';
import ApprovedOrderCannotBeRejectedException from '../exception/ApprovedOrderCannotBeRejectedException';
import OrderCannotBeShippedTwiceException from '../exception/OrderCannotBeShippedTwiceException';
import OrderCannotBeShippedException from '../exception/OrderCannotBeShippedException';

class Order {
  private total: number;
  private currency: string;
  private items: OrderItem[];
  private tax: number;
  private status: OrderStatus;
  private id: number;

  constructor() {
    this.total = 0;
    this.currency = 'EUR';
    this.items = [];
    this.tax = 0;
    this.status = OrderStatus.CREATED;
    this.id = Math.floor(Math.random() * 1000000);
  }

  public getTotal(): number {
    return this.total;
  }

  public getCurrency(): string {
    return this.currency;
  }

  public getItems(): OrderItem[] {
    return this.items;
  }

  public getTax(): number {
    return this.tax;
  }

  public getStatus(): OrderStatus {
    return this.status;
  }

  public setStatus(status: OrderStatus): void {
    this.status = status;
  }

  public getId(): number {
    return this.id;
  }

  public addItem(item: OrderItem): void {
    this.items.push(item);

    this.total += item.getTaxedAmount();
    this.tax += item.getTax();
  }

  public approve(): void {
    if (this.isShipped()) {
      throw new ShippedOrdersCannotBeChangedException();
    } else if (this.isRejected()) {
      throw new RejectedOrderCannotBeApprovedException();
    } else {
      this.status = OrderStatus.APPROVED;
    }
  }

  public reject(): void {
    if (this.isShipped()) {
      throw new ShippedOrdersCannotBeChangedException();
    } else if (this.isApproved()) {
      throw new ApprovedOrderCannotBeRejectedException();
    } else {
      this.status = OrderStatus.REJECTED;
    }
  }

  public canBeShipped(): void {
    if (this.isShipped()) {
      throw new OrderCannotBeShippedTwiceException();
    }

    if (!this.isApproved()) {
      throw new OrderCannotBeShippedException();
    }
  }

  public ship(): void {
    this.status = OrderStatus.SHIPPED;
  }

  public isRejected(): boolean {
    return this.status === OrderStatus.REJECTED;
  }

  public isApproved(): boolean {
    return this.status === OrderStatus.APPROVED;
  }

  public isShipped(): boolean {
    return this.status === OrderStatus.SHIPPED;
  }
}

export default Order;

