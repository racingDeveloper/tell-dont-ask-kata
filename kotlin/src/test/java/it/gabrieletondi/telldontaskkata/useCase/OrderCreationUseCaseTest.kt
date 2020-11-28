package it.gabrieletondi.telldontaskkata.useCase

import it.gabrieletondi.telldontaskkata.domain.Category
import it.gabrieletondi.telldontaskkata.domain.Order
import it.gabrieletondi.telldontaskkata.domain.OrderStatus
import it.gabrieletondi.telldontaskkata.domain.Product
import it.gabrieletondi.telldontaskkata.doubles.InMemoryProductCatalog
import it.gabrieletondi.telldontaskkata.doubles.TestOrderRepository
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog
import org.hamcrest.Matchers
import org.hamcrest.Matchers.equalTo
import org.junit.Assert.assertThat
import org.junit.Test
import java.math.BigDecimal
import java.util.*

class OrderCreationUseCaseTest {
    private val orderRepository: TestOrderRepository = TestOrderRepository()
    private val food: Category = object : Category() {
        init {
            name = "food"
            taxPercentage = BigDecimal("10")
        }
    }
    private val productCatalog: ProductCatalog = InMemoryProductCatalog(
        listOf(
            object : Product() {
                init {
                    name = "salad"
                    price = BigDecimal("3.56")
                    category = food
                }
            },
            object : Product() {
                init {
                    name = "tomato"
                    price = BigDecimal("4.65")
                    category = food
                }
            }
        )
    )
    private val useCase = OrderCreationUseCase(orderRepository, productCatalog)

    @Test
    @Throws(Exception::class)
    fun sellMultipleItems() {
        val saladRequest = SellItemRequest()
        saladRequest.productName = "salad"
        saladRequest.quantity = 2
        val tomatoRequest = SellItemRequest()
        tomatoRequest.productName = "tomato"
        tomatoRequest.quantity = 3
        val request = SellItemsRequest()
        request.requests = ArrayList()
        (request.requests as ArrayList<SellItemRequest>).add(saladRequest)
        (request.requests as ArrayList<SellItemRequest>).add(tomatoRequest)
        useCase.run(request)
        val insertedOrder: Order = orderRepository.savedOrder!!
        assertThat(insertedOrder.status, equalTo(OrderStatus.CREATED))
        assertThat(insertedOrder.total, equalTo(BigDecimal("23.20")))
        assertThat(insertedOrder.tax, equalTo(BigDecimal("2.13")))
        assertThat(insertedOrder.currency, equalTo("EUR"))
        assertThat(insertedOrder.items, Matchers.hasSize(2))
        assertThat(insertedOrder.items?.get(0)?.product?.name, equalTo("salad"))
        assertThat(insertedOrder.items?.get(0)?.product?.price, equalTo(BigDecimal("3.56")))
        assertThat(insertedOrder.items?.get(0)?.quantity, equalTo(2))
        assertThat(insertedOrder.items?.get(0)?.taxedAmount, equalTo(BigDecimal("7.84")))
        assertThat(insertedOrder.items?.get(0)?.tax, equalTo(BigDecimal("0.72")))
        assertThat(insertedOrder.items?.get(1)?.product?.name, equalTo("tomato"))
        assertThat(insertedOrder.items?.get(1)?.product?.price, equalTo(BigDecimal("4.65")))
        assertThat(insertedOrder.items?.get(1)?.quantity, equalTo(3))
        assertThat(insertedOrder.items?.get(1)?.taxedAmount, equalTo(BigDecimal("15.36")))
        assertThat(insertedOrder.items?.get(1)?.tax, equalTo(BigDecimal("1.41")))
    }

    @Test(expected = UnknownProductException::class)
    @Throws(Exception::class)
    fun unknownProduct() {
        val request = SellItemsRequest()
        request.requests = ArrayList()
        val unknownProductRequest = SellItemRequest()
        unknownProductRequest.productName = "unknown product"
        (request.requests as ArrayList<SellItemRequest>).add(unknownProductRequest)
        useCase.run(request)
    }
}