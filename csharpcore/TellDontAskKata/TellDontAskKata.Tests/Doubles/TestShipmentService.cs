using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Service;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestShipmentService : IShipmentService
    {
        private Order _shippedOrder = null;

        public void Ship(Order order)
        {
            _shippedOrder = order;
        }

        public Order GetShippedOrder()
        {
            return _shippedOrder;
        }


    }
}
