class OrderShipmentRequest {
  private orderId: number;

  public setOrderId(orderId: number): void {
      this.orderId = orderId;
  }

  public getOrderId(): number {
      return this.orderId;
  }
}

export default OrderShipmentRequest;
