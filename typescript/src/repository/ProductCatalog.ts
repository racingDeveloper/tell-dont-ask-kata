import Product from "../domain/Product";

export default interface ProductCatalog {
    getByName(name: string): Product;
}