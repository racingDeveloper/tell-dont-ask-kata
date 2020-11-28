package it.gabrieletondi.telldontaskkata.useCase

import it.gabrieletondi.telldontaskkata.domain.Order
import it.gabrieletondi.telldontaskkata.domain.OrderItem
import it.gabrieletondi.telldontaskkata.domain.OrderStatus
import it.gabrieletondi.telldontaskkata.repository.OrderRepository
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog
import java.math.BigDecimal
import java.math.RoundingMode
import java.util.*

class OrderCreationUseCase(private val orderRepository: OrderRepository, private val productCatalog: ProductCatalog) {
    fun run(request: SellItemsRequest) {
        val order = Order()
        order.status = OrderStatus.CREATED
        order.items = ArrayList()
        order.currency = "EUR"
        order.total = BigDecimal("0.00")
        order.tax = BigDecimal("0.00")
        for (itemRequest in request.requests!!) {
            val product = productCatalog.getByName(itemRequest.productName!!)
            if (product == null) {
                throw UnknownProductException()
            } else {
                val unitaryTax = product.price!!.divide(BigDecimal.valueOf(100)).multiply(product.category!!.taxPercentage).setScale(2, RoundingMode.HALF_UP)
                val unitaryTaxedAmount = product.price!!.add(unitaryTax).setScale(2, RoundingMode.HALF_UP)
                val taxedAmount = unitaryTaxedAmount.multiply(BigDecimal.valueOf(itemRequest.quantity.toLong())).setScale(2, RoundingMode.HALF_UP)
                val taxAmount = unitaryTax.multiply(BigDecimal.valueOf(itemRequest.quantity.toLong()))
                val orderItem = OrderItem()
                orderItem.product = product
                orderItem.quantity = itemRequest.quantity
                orderItem.tax = taxAmount
                orderItem.taxedAmount = taxedAmount
                (order.items as ArrayList<OrderItem>).add(orderItem)
                order.total = order.total!!.add(taxedAmount)
                order.tax = order.tax!!.add(taxAmount)
            }
        }
        orderRepository.save(order)
    }
}