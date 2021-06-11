import Product from '../domain/Product';

export interface ProductCatalog {
  getByName(name: string): Product;
}
