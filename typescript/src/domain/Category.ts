class Category {
  private name: string;
  private taxPercentage: number;

  public getName(): string {
    return this.name;
  }

  public setName(name: string): void {
    this.name = name;
  }

  public getTaxPercentage(): number {
    return this.taxPercentage;
  }

  public setTaxPercentage(taxPercentage: number) {
    this.taxPercentage = taxPercentage;
  }
}

export default Category;

