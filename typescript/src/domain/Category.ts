import bigDecimal from "js-big-decimal";

export default class Category {
    private _name: string;
    private _taxPercentage: bigDecimal;

    constructor() {}

    get name(): string {
        return this._name;
    }

    set name(name: string) {
        this._name = name;
    }


    get taxPercentage(): bigDecimal {
        return this._taxPercentage;
    }

    set taxPercentage(taxPercentage: bigDecimal) {
        this._taxPercentage = taxPercentage;
    }
}