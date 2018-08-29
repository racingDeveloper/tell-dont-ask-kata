using System;

using TellDontAskKata.Domain;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderShipmentUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;

        private readonly TestShipmentService shipmentService;

        private readonly OrderShipmentUseCase useCase;

        public OrderShipmentUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            shipmentService = new TestShipmentService();
            useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
        }

        [Fact]
        public void ShipApprovedOrder()
        {
            Order initialOrder = new Order
            {
                Id = 1,
                Status = OrderStatus.Approved
            };
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            useCase.Run(request);

            Assert.Equal(OrderStatus.Shipped, orderRepository.SavedOrder.Status);
            Assert.Equal(shipmentService.ShippedOrder, initialOrder);
        }

        [Fact]
        public void CreatedOrdersCannotBeShipped()
        {
            Order initialOrder = new Order { Id = 1, Status = OrderStatus.Created };
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(runAction);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }

        [Fact]
        public void RejectedOrdersCannotBeShipped()
        {
            Order initialOrder = new Order { Id = 1, Status = OrderStatus.Rejected };
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest { OrderId = 1 };

            Action runAction = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(runAction);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }

        [Fact]
        public void ShippedOrdersCannotBeShippedAgain()
        {
            Order initialOrder = new Order
            {
                Id = 1,
                Status = OrderStatus.Shipped
            };
            orderRepository.AddOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            Action actionRun = () => useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedTwiceException>(actionRun);
            Assert.Null(orderRepository.SavedOrder);
            Assert.Null(shipmentService.ShippedOrder);
        }
    }
}
