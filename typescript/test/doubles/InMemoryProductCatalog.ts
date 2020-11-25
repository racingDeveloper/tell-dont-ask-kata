import ProductCatalog from "../../src/repository/ProductCatalog";
import Product from "../../src/domain/Product";

export default class InMemoryProductCatalog implements ProductCatalog {
    private readonly _products: Product[];

    constructor(products: Product[]) {
        this._products = products;
    }

    getByName(name: string): Product {
        return this._products.find(p => p.name === name);
    }

}