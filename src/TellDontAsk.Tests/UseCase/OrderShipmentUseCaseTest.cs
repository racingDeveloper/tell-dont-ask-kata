//namespace TellDontAsk.Tests.UseCase
//{package it.gabrieletondi.telldontaskkata.useCase;

//    import it.gabrieletondi.telldontaskkata.domain.Order;
//    import it.gabrieletondi.telldontaskkata.domain.OrderStatus;
//    import it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository;
//    import it.gabrieletondi.telldontaskkata.doubles.TestShipmentService;
//    import org.junit.Test;

//    import static org.hamcrest.Matchers.is;
//    import static org.hamcrest.Matchers.nullValue;
//    import static org.junit.Assert.assertThat;

//    public class OrderShipmentUseCaseTest {
//        private final TestOrderRepository orderRepository = new TestOrderRepository();
//        private final TestShipmentService shipmentService = new TestShipmentService();
//        private final OrderShipmentUseCase useCase = new OrderShipmentUseCase(orderRepository, shipmentService);

//        [Fact]
//        public void shipApprovedOrder() throws Exception {
//            Order initialOrder = new Order();
//        initialOrder.setId(1);
//        initialOrder.setStatus(OrderStatus.APPROVED);
//        orderRepository.addOrder(initialOrder);

//        OrderShipmentRequest request = new OrderShipmentRequest();
//        request.setOrderId(1);

//        useCase.run(request);

//        assertThat(orderRepository.getSavedOrder().getStatus(), is(OrderStatus.SHIPPED));
//        assertThat(shipmentService.getShippedOrder(), is(initialOrder));
//    }

//    [Fact](expected = OrderCannotBeShippedException.class)
//    public void createdOrdersCannotBeShipped() throws Exception {
//    Order initialOrder = new Order();
//    initialOrder.setId(1);
//    initialOrder.setStatus(OrderStatus.CREATED);
//    orderRepository.addOrder(initialOrder);

//    OrderShipmentRequest request = new OrderShipmentRequest();
//    request.setOrderId(1);

//    useCase.run(request);

//    assertThat(orderRepository.getSavedOrder(), is(nullValue()));
//    assertThat(shipmentService.getShippedOrder(), is(nullValue()));
//    }

//    [Fact](expected = OrderCannotBeShippedException.class)
//    public void rejectedOrdersCannotBeShipped() throws Exception {
//    Order initialOrder = new Order();
//    initialOrder.setId(1);
//    initialOrder.setStatus(OrderStatus.REJECTED);
//    orderRepository.addOrder(initialOrder);

//    OrderShipmentRequest request = new OrderShipmentRequest();
//    request.setOrderId(1);

//    useCase.run(request);

//    assertThat(orderRepository.getSavedOrder(), is(nullValue()));
//    assertThat(shipmentService.getShippedOrder(), is(nullValue()));
//    }

//    [Fact](expected = OrderCannotBeShippedTwiceException.class)
//    public void shippedOrdersCannotBeShippedAgain() throws Exception {
//    Order initialOrder = new Order();
//    initialOrder.setId(1);
//    initialOrder.setStatus(OrderStatus.SHIPPED);
//    orderRepository.addOrder(initialOrder);

//    OrderShipmentRequest request = new OrderShipmentRequest();
//    request.setOrderId(1);

//    useCase.run(request);

//    assertThat(orderRepository.getSavedOrder(), is(nullValue()));
//    assertThat(shipmentService.getShippedOrder(), is(nullValue()));
//    }
//    }
//}