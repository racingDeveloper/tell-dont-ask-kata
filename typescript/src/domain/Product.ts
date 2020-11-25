import Category from "./Category";
import bigDecimal from "js-big-decimal";

export default class Product {
    private _product: Product;
    private _name: string;
    private _price: bigDecimal;
    private _category: Category;

    constructor() {}

    get product(): Product {
        return this._product;
    }

    set product(value: Product) {
        this._product = value;
    }

    get name(): string {
        return this._name;
    }

    set name(value: string) {
        this._name = value;
    }

    get price(): bigDecimal {
        return this._price;
    }

    set price(value: bigDecimal) {
        this._price = value;
    }

    get category(): Category {
        return this._category;
    }

    set category(value: Category) {
        this._category = value;
    }
}