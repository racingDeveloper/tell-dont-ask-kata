import Category from './Category';

class Product {
  private name: string;
  private price: number;
  private category: Category;

  public getName(): string {
    return this.name;
  }

  public setName(name: string): void {
    this.name = name;
  }

  public getPrice(): number {
    return this.price;
  }

  public setPrice(price: number): void {
    this.price = price;
  }

  public getCategory(): Category {
    return this.category;
  }

  public setCategory(category: Category): void {
    this.category = category;
  }

  public getUnitaryTax(): number {
    return Math.round(this.getPrice() / 100 * this.getCategory().getTaxPercentage() * 100) / 100;
  }

  public getUnitaryTaxedAmount(): number {
    return Math.round((this.getPrice() + this.getUnitaryTax()) * 100) / 100;
  }
}

export default Product;

