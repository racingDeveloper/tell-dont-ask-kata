using TellDontAskKata.Domain;
using TellDontAskKata.Service;

namespace TellDontAskKata.Tests.Doubles
{
    public class TestShipmentService: IShipmentService
    {
        public void Ship(Order order)
        {
            this.ShippedOrder = order;
        }

        public Order ShippedOrder { get; set; }
    }
}
