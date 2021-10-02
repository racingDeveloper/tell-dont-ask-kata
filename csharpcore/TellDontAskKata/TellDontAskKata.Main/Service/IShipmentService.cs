using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Service
{
    public interface IShipmentService
    {
        void Ship(Order order);
    }
}
