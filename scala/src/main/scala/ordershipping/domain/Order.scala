package ordershipping.domain

import ordershipping.domain.OrderStatus.OrderStatus

import scala.collection.mutable

class Order(
    var total: Double = 0,
    var currency: String = "",
    var items: mutable.MutableList[OrderItem] = mutable.MutableList.empty,
    var tax: Double = 0,
    var status: OrderStatus,
    var id: Int
)
