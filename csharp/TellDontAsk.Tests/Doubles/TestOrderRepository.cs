using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Repository;

namespace TellDontAsk.Tests.Doubles
{
    public class TestOrderRepository : OrderRepository
    {
        private Order insertedOrder;
        private List<Order> orders = new List<Order>();

        public Order getSavedOrder()
        {
            return insertedOrder;
        }

        public void save(Order order)
        {
            this.insertedOrder = order;
        }

        public Order getById(int orderId)
        {
            return orders.FirstOrDefault(o => o.getId() == orderId);
        }

        public void addOrder(Order order)
        {
            this.orders.Add(order);
        }
    }
}
