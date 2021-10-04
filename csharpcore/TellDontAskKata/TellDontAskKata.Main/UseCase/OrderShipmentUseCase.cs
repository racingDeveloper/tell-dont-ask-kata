using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.Service;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentService _shipmentService;

        public OrderShipmentUseCase(
            IOrderRepository orderRepository,
            IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);

            if (order.Status == OrderStatus.Created || order.Status == OrderStatus.Rejected)
            {
                throw new OrderCannotBeShippedException();
            }

            if (order.Status == OrderStatus.Shipped)
            {
                throw new OrderCannotBeShippedTwiceException();
            }

            _shipmentService.Ship(order);

            order.Status = OrderStatus.Shipped;
            _orderRepository.Save(order);
        }
    }
}
