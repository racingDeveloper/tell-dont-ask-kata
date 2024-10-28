import SellItemRequest from './SellItemRequest';

class SellItemsRequest {
  private requests: SellItemRequest[];

  public setRequests(requests: SellItemRequest[]): void {
    this.requests = requests;
  }

  public getRequests(): SellItemRequest[] {
    return this.requests;
  }
}

export default SellItemsRequest;
