

namespace TellDontAsk.Domain
{
    public class OrderItem {
        private Product product;
        private int quantity;
        private decimal taxedAmount;
        private decimal tax;

        public Product getProduct() {
            return product;
        }

        public void setProduct(Product product) {
            this.product = product;
        }

        public int getQuantity() {
            return quantity;
        }

        public void setQuantity(int quantity) {
            this.quantity = quantity;
        }

        public decimal getTaxedAmount() {
            return taxedAmount;
        }

        public void setTaxedAmount(decimal taxedAmount) {
            this.taxedAmount = taxedAmount;
        }

        public decimal getTax() {
            return tax;
        }

        public void setTax(decimal tax) {
            this.tax = tax;
        }
    }
}
