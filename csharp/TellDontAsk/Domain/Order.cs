using Deveel.Math;
using System;
using System.Collections.Generic;

namespace TellDontAsk.Domain
{
    public class Order
    {
        private BigDecimal total;
        private String currency;
        private List<OrderItem> items;
        private BigDecimal tax;
        private OrderStatus status;
        private int id;

        public BigDecimal getTotal()
        {
            return total;
        }

        public void setTotal(BigDecimal total)
        {
            this.total = total;
        }

        public String getCurrency()
        {
            return currency;
        }

        public void setCurrency(String currency)
        {
            this.currency = currency;
        }

        public List<OrderItem> getItems()
        {
            return items;
        }

        public void setItems(List<OrderItem> items)
        {
            this.items = items;
        }

        public BigDecimal getTax()
        {
            return tax;
        }

        public void setTax(BigDecimal tax)
        {
            this.tax = tax;
        }

        public OrderStatus getStatus()
        {
            return status;
        }

        public void setStatus(OrderStatus status)
        {
            this.status = status;
        }

        public int getId()
        {
            return id;
        }

        public void setId(int id)
        {
            this.id = id;
        }
    }
}
