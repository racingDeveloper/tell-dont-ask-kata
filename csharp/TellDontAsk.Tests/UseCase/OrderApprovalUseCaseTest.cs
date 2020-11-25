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
    public class OrderApprovalUseCaseTest
    {
        private TestOrderRepository orderRepository;
        private OrderApprovalUseCase useCase;

        [SetUp]
        public void setUp()
        {
            orderRepository = new TestOrderRepository();
            useCase = new OrderApprovalUseCase(orderRepository);
        }

        [Test]
        public void approvedExistingOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.CREATED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(true);

            useCase.run(request);

            Order savedOrder = orderRepository.getSavedOrder();
            Assert.That(savedOrder.getStatus(), Is.EqualTo(OrderStatus.APPROVED));
        }

        [Test]
        public void rejectedExistingOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.CREATED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(false);

            useCase.run(request);

            Order savedOrder = orderRepository.getSavedOrder();
            Assert.That(savedOrder.getStatus(), Is.EqualTo(OrderStatus.REJECTED));
        }

        [Test]
        public void cannotApproveRejectedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.REJECTED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(true);

            Assert.That(() => useCase.run(request),
                Throws.TypeOf<RejectedOrderCannotBeApprovedException>());
        }

        [Test]
        public void cannotRejectApprovedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.APPROVED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(false);

            Assert.That(() => useCase.run(request),
                Throws.TypeOf<ApprovedOrderCannotBeRejectedException>());
        }

        [Test]
        public void shippedOrdersCannotBeApproved()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.SHIPPED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(true);

            Assert.That(() => useCase.run(request),
                Throws.TypeOf<ShippedOrdersCannotBeChangedException>());
        }

        [Test]
        public void shippedOrdersCannotBeRejected()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.SHIPPED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(false);

            Assert.That(() => useCase.run(request),
                           Throws.TypeOf<ShippedOrdersCannotBeChangedException>());
        }
    }
}
