package usecase

import doubles.TestOrderRepository
import ordershipping.domain.{Order, OrderStatus}
import ordershipping.usecase.{OrderApprovalRequest, OrderApprovalUseCase}
import org.scalatest.BeforeAndAfterEach
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class OrderApprovalUseCaseTest
    extends AnyFlatSpec
    with Matchers
    with BeforeAndAfterEach {
  private var orderRepository: TestOrderRepository = _
  private var useCase: OrderApprovalUseCase = _

  override def beforeEach(): Unit = {
    orderRepository = new TestOrderRepository
    useCase = new OrderApprovalUseCase(orderRepository)
  }

  "order approval use case" should "approved existing order" in {
    val initialOrder = new Order(status = OrderStatus.Created, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = true)

    useCase.run(request)

    val savedOrder = orderRepository.insertedOrder

    savedOrder.status shouldBe OrderStatus.Approved
  }

  // TODO add other Test cases here
}
