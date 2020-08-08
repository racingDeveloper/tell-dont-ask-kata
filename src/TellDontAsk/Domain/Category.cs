using System;

namespace TellDontAsk.Domain
{
    public class Category
    {
        private String name;
        private decimal taxPercentage;

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public decimal getTaxPercentage()
        {
            return taxPercentage;
        }

        public void setTaxPercentage(decimal taxPercentage)
        {
            this.taxPercentage = taxPercentage;
        }
    }

}