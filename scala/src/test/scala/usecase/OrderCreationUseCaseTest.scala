package usecase

import doubles.{InMemoryProductCatalog, TestOrderRepository}
import ordershipping.domain.{Category, OrderStatus, Product}
import ordershipping.usecase.{
  OrderCreationUseCase,
  SellItemRequest,
  SellItemsRequest,
  UnknownProductException
}
import org.scalatest.BeforeAndAfterEach
import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers

class OrderCreationUseCaseTest
    extends AnyFlatSpec
    with Matchers
    with BeforeAndAfterEach {
  private val food = new Category(name = "food", taxPercentage = 10)
  private val productCatalog = new InMemoryProductCatalog(
    List(
      new Product(name = "salad", price = 3.56, category = food),
      new Product(name = "tomato", price = 4.65, category = food)
    )
  )
  private var orderRepository: TestOrderRepository = _
  private var useCase: OrderCreationUseCase = _

  override def beforeEach(): Unit = {
    orderRepository = new TestOrderRepository()
    useCase = new OrderCreationUseCase(
      orderRepository = orderRepository,
      productCatalog = productCatalog
    )
  }

  "order creation use case" should "sell multiple items" in {
    val saladRequest = SellItemRequest(productName = "salad", quantity = 2)
    val tomatoRequest = SellItemRequest(productName = "tomato", quantity = 3)

    val request = SellItemsRequest(
      List(saladRequest, tomatoRequest)
    )

    useCase.run(request)

    val insertedOrder = orderRepository.insertedOrder
    insertedOrder.status shouldBe OrderStatus.Created
    insertedOrder.total shouldBe 23.20
    insertedOrder.tax shouldBe 2.13
    insertedOrder.currency shouldBe "EUR"
    insertedOrder.items.length shouldBe 2
    insertedOrder.items.head.product.name shouldBe "salad"
    insertedOrder.items.head.product.price shouldBe 3.56
    insertedOrder.items.head.quantity shouldBe 2
    insertedOrder.items.head.taxedAmount shouldBe 7.84
    insertedOrder.items.head.tax shouldBe 0.72

    insertedOrder.items(1).product.name shouldBe "tomato"
    insertedOrder.items(1).product.price shouldBe 4.65
    insertedOrder.items(1).quantity shouldBe 3
    insertedOrder.items(1).taxedAmount shouldBe 15.36
    insertedOrder.items(1).tax shouldBe 1.41
  }

  "order creation use case" should "unknown product" in {
    val request = SellItemsRequest(
      List(SellItemRequest(productName = "unknown product", quantity = 0))
    )
    assertThrows[UnknownProductException] {
      useCase.run(request)
    }
  }
}
