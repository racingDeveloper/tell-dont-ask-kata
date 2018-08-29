using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Domain;
using TellDontAskKata.Repository;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestOrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>();

        public void Save(Order order)
        {
            SavedOrder = order;
        }

        public Order SavedOrder { get; private set; }

        public Order GetById(int orderId)
        {
            return this.orders.SingleOrDefault(order => order.Id == orderId);
        }

        public void AddOrder(Order order)
        {
            this.orders.Add(order);
        }
    }
}
