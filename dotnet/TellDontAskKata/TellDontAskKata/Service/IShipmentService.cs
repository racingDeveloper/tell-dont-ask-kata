using TellDontAskKata.Domain;

namespace TellDontAskKata.Service
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}
