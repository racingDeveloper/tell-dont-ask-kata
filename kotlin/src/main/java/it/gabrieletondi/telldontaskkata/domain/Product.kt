package it.gabrieletondi.telldontaskkata.domain

import java.math.BigDecimal

open class Product {
    var name: String? = null
    var price: BigDecimal? = null
    var category: Category? = null
}