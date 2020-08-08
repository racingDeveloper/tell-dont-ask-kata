using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;

namespace TellDontAsk.Tests.UseCase
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
        public void shipApprovedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.APPROVED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            useCase.run(request);

            Assert.Equal(OrderStatus.SHIPPED, orderRepository.getSavedOrder().getStatus());
            Assert.Equal(initialOrder, shipmentService.getShippedOrder());
        }

        [Fact]
        public void createdOrdersCannotBeShipped()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.CREATED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.Throws<OrderCannotBeShippedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
            Assert.Null(shipmentService.getShippedOrder());
        }

        [Fact]
        public void rejectedOrdersCannotBeShipped()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.REJECTED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.Throws<OrderCannotBeShippedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
            Assert.Null(shipmentService.getShippedOrder());
        }

        [Fact]
        public void shippedOrdersCannotBeShippedAgain()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.SHIPPED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.Throws<OrderCannotBeShippedTwiceException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
            Assert.Null(shipmentService.getShippedOrder());
        }
    }
}