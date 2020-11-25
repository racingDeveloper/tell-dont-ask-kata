import OrderItem from "./OrderItem";
import OrderStatus from "./OrderStatus";
import bigDecimal from "js-big-decimal";

export default class Order {
    private _total: bigDecimal;
    private _currency: string;
    private _items: OrderItem[];
    private _tax: bigDecimal;
    private _status: OrderStatus;
    private _id: number;

    constructor() {
    }


    get total(): bigDecimal {
        return this._total;
    }

    set total(value: bigDecimal) {
        this._total = value;
    }

    get currency(): string {
        return this._currency;
    }

    set currency(value: string) {
        this._currency = value;
    }

    get items(): OrderItem[] {
        return this._items;
    }

    set items(value: OrderItem[]) {
        this._items = value;
    }

    get tax(): bigDecimal {
        return this._tax;
    }

    set tax(value: bigDecimal) {
        this._tax = value;
    }

    get status(): OrderStatus {
        return this._status;
    }

    set status(value: OrderStatus) {
        this._status = value;
    }

    get id(): number {
        return this._id;
    }

    set id(value: number) {
        this._id = value;
    }
}