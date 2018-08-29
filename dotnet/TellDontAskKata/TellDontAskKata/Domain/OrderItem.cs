using System;

namespace TellDontAskKata.Domain
{
    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Decimal TaxedAmount { get; set; }
        public Decimal Tax { get; set; }
    }
}