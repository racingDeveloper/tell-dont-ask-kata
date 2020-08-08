using System;

namespace TellDontAsk.Domain
{
    public class Product
    {
        public Product()
        {
        }
        public Product(string name, decimal price, Category category)
        {
            Name = name;
            Price = price;
            Category = category;
        }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }

        public String getName()
        {
            return Name;
        }

        public void setName(String name)
        {
            this.Name = name;
        }

        public decimal getPrice()
        {
            return Price;
        }

        public void setPrice(decimal price)
        {
            this.Price = price;
        }

        public Category getCategory()
        {
            return Category;
        }

        public void setCategory(Category category)
        {
            this.Category = category;
        }
    }
}
