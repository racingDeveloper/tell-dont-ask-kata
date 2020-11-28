package it.gabrieletondi.telldontaskkata.doubles

import it.gabrieletondi.telldontaskkata.domain.Order
import it.gabrieletondi.telldontaskkata.repository.OrderRepository
import java.util.*

class TestOrderRepository : OrderRepository {
    var savedOrder: Order? = null
        private set
    private val orders: MutableList<Order> = ArrayList()
    override fun save(order: Order) {
        savedOrder = order
    }

    override fun getById(orderId: Int): Order {
        return orders.stream().filter { o: Order -> o.id == orderId }.findFirst().get()
    }

    fun addOrder(order: Order) {
        orders.add(order)
    }
}