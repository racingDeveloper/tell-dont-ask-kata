using TellDontAsk.Domain;
using TellDontAsk.Service;

namespace TellDontAsk.Tests.Doubles
{
    public class TestShipmentService : ShipmentService
    {
        private Order shippedOrder = null;

        public Order getShippedOrder()
        {
            return shippedOrder;
        }

        public void ship(Order order)
        {
            this.shippedOrder = order;
        }
    }
}
