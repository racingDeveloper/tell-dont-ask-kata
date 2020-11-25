import Product from "./Product";
import bigDecimal from "js-big-decimal";

export default class OrderItem {
    private _product: Product;
    private _quantity: number;
    private _taxedAmount: bigDecimal;
    private _tax: bigDecimal;

    constructor() {}

    get product(): Product {
        return this._product;
    }

    set product(value: Product) {
        this._product = value;
    }

    get quantity(): number {
        return this._quantity;
    }

    set quantity(value: number) {
        this._quantity = value;
    }

    get taxedAmount(): bigDecimal {
        return this._taxedAmount;
    }

    set taxedAmount(value: bigDecimal) {
        this._taxedAmount = value;
    }

    get tax(): bigDecimal {
        return this._tax;
    }

    set tax(value: bigDecimal) {
        this._tax = value;
    }
}