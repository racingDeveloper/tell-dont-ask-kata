using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderApprovalUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Run(OrderApprovalRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);

            if (order.Status == OrderStatus.Shipped)
            {
                throw new ShippedOrdersCannotBeChangedException();
            }

            if (request.Approved && order.Status == OrderStatus.Rejected)
            {
                throw new RejectedOrderCannotBeApprovedException();
            }

            if (!request.Approved && order.Status == OrderStatus.Approved)
            {
                throw new ApprovedOrderCannotBeRejectedException();
            }

            order.Status = request.Approved ? OrderStatus.Approved : OrderStatus.Rejected;
            _orderRepository.Save(order);
        }
    }
}
