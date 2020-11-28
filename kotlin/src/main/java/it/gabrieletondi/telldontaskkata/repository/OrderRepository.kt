package it.gabrieletondi.telldontaskkata.repository

import it.gabrieletondi.telldontaskkata.domain.Order

interface OrderRepository {
    fun save(order: Order)
    fun getById(orderId: Int): Order?
}