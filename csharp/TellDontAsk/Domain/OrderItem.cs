using Deveel.Math;

namespace TellDontAsk.Domain
{
    public class OrderItem
    {
        private Product product;
        private int quantity;
        private BigDecimal taxedAmount;
        private BigDecimal tax;

        public Product getProduct()
        {
            return product;
        }

        public void setProduct(Product product)
        {
            this.product = product;
        }

        public int getQuantity()
        {
            return quantity;
        }

        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public BigDecimal getTaxedAmount()
        {
            return taxedAmount;
        }

        public void setTaxedAmount(BigDecimal taxedAmount)
        {
            this.taxedAmount = taxedAmount;
        }

        public BigDecimal getTax()
        {
            return tax;
        }

        public void setTax(BigDecimal tax)
        {
            this.tax = tax;
        }
    }

}
