package it.gabrieletondi.telldontaskkata.domain

import java.math.BigDecimal

class OrderItem {
    var product: Product? = null
    var quantity = 0
    var taxedAmount: BigDecimal? = null
    var tax: BigDecimal? = null
}