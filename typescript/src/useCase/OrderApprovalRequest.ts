class OrderApprovalRequest {
  private orderId: number;
  private approved: boolean;

  public  setOrderId(orderId: number): void {
    this.orderId = orderId;
  }

  public getOrderId(): number {
    return this.orderId;
  }

  public setApproved(approved: boolean): void {
    this.approved = approved;
  }

  public isApproved(): boolean{
    return this.approved;
  }
}

export default OrderApprovalRequest;

