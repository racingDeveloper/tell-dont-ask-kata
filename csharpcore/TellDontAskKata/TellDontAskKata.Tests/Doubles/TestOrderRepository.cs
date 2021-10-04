using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestOrderRepository : IOrderRepository
    {
        private Order _insertedOrder;
        private IList<Order> _orders = new List<Order>();

        public void Save(Order order)
        {
            _insertedOrder = order;
        }

        public Order GetById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }


        public Order GetSavedOrder()
        {
            return _insertedOrder;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
    }
}
