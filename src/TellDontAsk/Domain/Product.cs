using System;

namespace TellDontAsk.Domain
{
    public class Product
    {
        private String name;
        private decimal price;
        private Category category;

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public decimal getPrice()
        {
            return price;
        }

        public void setPrice(decimal price)
        {
            this.price = price;
        }

        public Category getCategory()
        {
            return category;
        }

        public void setCategory(Category category)
        {
            this.category = category;
        }
    }
}
