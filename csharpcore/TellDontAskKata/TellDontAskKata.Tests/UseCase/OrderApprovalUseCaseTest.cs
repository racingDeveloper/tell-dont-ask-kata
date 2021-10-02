using System;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Tests.Doubles;
using Xunit;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderApprovalUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly OrderApprovalUseCase _useCase;

        public OrderApprovalUseCaseTest()
        {
            _orderRepository = new TestOrderRepository();
            _useCase = new OrderApprovalUseCase(_orderRepository);
        }


        [Fact]
        public void ApprovedExistingOrder()
        {
            var initialOrder = new Order
            {
                Status = OrderStatus.Created,
                Id = 1
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = true
            };

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Approved, savedOrder.Status);
        }

        [Fact]
        public void RejectedExistingOrder()
        {
            var initialOrder = new Order
            {
                Status = OrderStatus.Created,
                Id = 1
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Rejected, savedOrder.Status);
        }


        [Fact]
        public void CannotApproveRejectedOrder()
        {
            var initialOrder = new Order
            {
                Status = OrderStatus.Rejected,
                Id = 1
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = true
            };


            Action actionToTest = () => _useCase.Run(request);
      
            Assert.Throws<RejectedOrderCannotBeApprovedException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            var initialOrder = new Order
            {
                Status = OrderStatus.Approved,
                Id = 1
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };


            Action actionToTest = () => _useCase.Run(request);
            
            Assert.Throws<ApprovedOrderCannotBeRejectedException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            var initialOrder = new Order
            {
                Status = OrderStatus.Shipped,
                Id = 1
            };
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };


            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<ShippedOrdersCannotBeChangedException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
        }

    }
}
