package usecase

import doubles.TestOrderRepository
import ordershipping.domain.{Order, OrderStatus}
import ordershipping.usecase._
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

  "order approval use case" should "approve existing order" in {
    val initialOrder = new Order(status = OrderStatus.Created, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = true)

    useCase.run(request)

    orderRepository.savedOrder.status shouldBe OrderStatus.Approved
  }

  "order approval use case" should "reject existing order" in {
    val initialOrder = new Order(status = OrderStatus.Created, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = false)

    useCase.run(request)

    orderRepository.savedOrder.status shouldBe OrderStatus.Rejected
  }

  "order approval use case" should "not approve a rejected order" in {
    val initialOrder = new Order(status = OrderStatus.Rejected, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = true)

    assertThrows[RejectedOrderCannotBeApprovedException](useCase.run(request))
    orderRepository.savedOrder shouldBe null
  }

  "order approval use case" should "not reject an approved order" in {
    val initialOrder = new Order(status = OrderStatus.Approved, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = false)

    assertThrows[ApprovedOrderCannotBeRejectedException](useCase.run(request))
    orderRepository.savedOrder shouldBe null
  }

  "order approval use case" should "not reject a shipped order" in {
    val initialOrder = new Order(status = OrderStatus.Shipped, id = 1)
    orderRepository.addOrder(initialOrder)
    val request = OrderApprovalRequest(orderId = 1, approved = false)

    assertThrows[ShippedOrdersCannotBeChangedException](useCase.run(request))
    orderRepository.savedOrder shouldBe null
  }
}
