import SellItemRequest from "./SellItemRequest";

export default class SellItemsRequest {
    private _requests: SellItemRequest[];

    constructor() {
    }

    get requests(): SellItemRequest[] {
        return this._requests;
    }

    set requests(value: SellItemRequest[]) {
        this._requests = value;
    }
}