package it.gabrieletondi.telldontaskkata.domain

import java.math.BigDecimal

class Order {
    var total: BigDecimal? = null
    var currency: String? = null
    var items: List<OrderItem>? = null
    var tax: BigDecimal? = null
    var status: OrderStatus? = null
    var id = 0
}