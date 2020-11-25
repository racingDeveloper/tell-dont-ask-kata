export default class OrderApprovalRequest {
    private _orderId: number;
    private _approved: boolean;


    get orderId(): number {
        return this._orderId;
    }

    set orderId(value: number) {
        this._orderId = value;
    }

    get approved(): boolean {
        return this._approved;
    }

    set approved(value: boolean) {
        this._approved = value;
    }
}