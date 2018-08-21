using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellDontAskKata.Domain;
using TellDontAskKata.Repository;
using TellDontAskKata.Service;

namespace TellDontAskKata.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository orderRepository;

        private readonly IShipmentService shipmentService;

        public OrderShipmentUseCase(IOrderRepository orderRepository, IShipmentService shipmentService)
        {
            this.orderRepository = orderRepository;
            this.shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            //Order order = this.orderRepository.GetById(request.OrderId);

            //if (order.Status = OrderStatus.Created || order.Status == OrderStatus.Rejected)
            //{
            //    throw new OrderCannotBeShippedException();
            //}
            //
            // if (order.getStatus().equals(SHIPPED))
            // {
            //     throw new OrderCannotBeShippedTwiceException();
            // }
            //
            // shipmentService.ship(order);

            // order.setStatus(OrderStatus.SHIPPED);
            // orderRepository.save(order);
        }
    }
}
