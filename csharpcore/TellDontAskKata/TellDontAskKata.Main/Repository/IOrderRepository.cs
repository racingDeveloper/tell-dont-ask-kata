using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Main.Repository
{
    public interface IOrderRepository
    {
        void Save(Order order);

        Order GetById(int orderId);
    }
}
