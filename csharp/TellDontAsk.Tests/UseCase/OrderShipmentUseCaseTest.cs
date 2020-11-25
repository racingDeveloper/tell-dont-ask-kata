using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;

namespace TellDontAsk.Tests.UseCase
{
    [TestFixture]
    public class OrderShipmentUseCaseTest
    {
        private TestOrderRepository orderRepository;
        private TestShipmentService shipmentService;
        private OrderShipmentUseCase useCase;

        [SetUp]
        public void SetUp()
        {
            orderRepository = new TestOrderRepository();
            shipmentService = new TestShipmentService();
            useCase = new OrderShipmentUseCase(orderRepository, shipmentService);
        }

        [Test]
        public void shipApprovedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.APPROVED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            useCase.run(request);

            Assert.That(orderRepository.getSavedOrder().getStatus(), Is.EqualTo(OrderStatus.SHIPPED));
            Assert.That(shipmentService.getShippedOrder(), Is.EqualTo(initialOrder));
        }

        [Test]
        public void createdOrdersCannotBeShipped()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.CREATED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.That(() => useCase.run(request),
              Throws.TypeOf<OrderCannotBeShippedException>());


            Assert.That(orderRepository.getSavedOrder(), Is.Null);
            Assert.That(shipmentService.getShippedOrder(), Is.Null);
        }

        [Test]
        public void rejectedOrdersCannotBeShipped()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.REJECTED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.That(() => useCase.run(request),
                         Throws.TypeOf<OrderCannotBeShippedException>());
        }

        [Test]
        public void shippedOrdersCannotBeShippedAgain()
        {
            Order initialOrder = new Order();
            initialOrder.setId(1);
            initialOrder.setStatus(OrderStatus.SHIPPED);
            orderRepository.addOrder(initialOrder);

            OrderShipmentRequest request = new OrderShipmentRequest();
            request.setOrderId(1);

            Assert.That(() => useCase.run(request),
                          Throws.TypeOf<OrderCannotBeShippedTwiceException>());
        }
    }
}
