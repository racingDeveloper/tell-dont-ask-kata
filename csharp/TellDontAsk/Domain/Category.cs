using Deveel.Math;

namespace TellDontAsk.Domain
{
    public class Category
    {
        private string name;
        private BigDecimal taxPercentage;

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public BigDecimal getTaxPercentage()
        {
            return taxPercentage;
        }

        public void setTaxPercentage(BigDecimal taxPercentage)
        {
            this.taxPercentage = taxPercentage;
        }

    }
}
