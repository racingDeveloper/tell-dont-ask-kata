using TellDontAsk.Domain;
using TellDontAsk.Tests.Doubles;
using TellDontAsk.UseCase;
using Xunit;
namespace TellDontAsk.Tests.UseCase
{


    public class OrderApprovalUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;
        private readonly OrderApprovalUseCase useCase;

        public OrderApprovalUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
            useCase = new OrderApprovalUseCase(orderRepository);
        }

        [Fact]
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
            Assert.Equal(OrderStatus.APPROVED, savedOrder.getStatus());
        }

        [Fact]
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
            Assert.Equal(OrderStatus.REJECTED, savedOrder.getStatus());
        }

        [Fact]// (expected = RejectedOrderCannotBeApprovedException.class)
        public void cannotApproveRejectedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.REJECTED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(true);

            Assert.Throws<RejectedOrderCannotBeApprovedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
        }

        [Fact]
        public void cannotRejectApprovedOrder()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.APPROVED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(false);


            Assert.Throws<ApprovedOrderCannotBeRejectedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
        }

        [Fact]
        public void shippedOrdersCannotBeApproved()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.SHIPPED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(true);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
        }

        [Fact]
        public void shippedOrdersCannotBeRejected()
        {
            Order initialOrder = new Order();
            initialOrder.setStatus(OrderStatus.SHIPPED);
            initialOrder.setId(1);
            orderRepository.addOrder(initialOrder);

            OrderApprovalRequest request = new OrderApprovalRequest();
            request.setOrderId(1);
            request.setApproved(false);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(() => useCase.run(request));

            Assert.Null(orderRepository.getSavedOrder());
        }
    }
}