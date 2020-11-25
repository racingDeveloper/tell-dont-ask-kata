using TellDontAsk.Domain;

namespace TellDontAsk.Repository
{
    public interface OrderRepository
    {
        void save(Order order);

        Order getById(int orderId);
    }
}
