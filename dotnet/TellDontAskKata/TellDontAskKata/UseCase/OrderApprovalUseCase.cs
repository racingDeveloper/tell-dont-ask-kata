using TellDontAskKata.Domain;
using TellDontAskKata.Repository;

namespace TellDontAskKata.UseCase
{
    public class OrderApprovalUseCase
    {
        private readonly IOrderRepository orderRepository;

        public OrderApprovalUseCase(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void Run(OrderApprovalRequest request)
        {
            var order = orderRepository.GetById(request.OrderId);

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
            orderRepository.Save(order);
        }
    }
}