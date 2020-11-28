package it.gabrieletondi.telldontaskkata.doubles

import it.gabrieletondi.telldontaskkata.domain.Order
import it.gabrieletondi.telldontaskkata.service.ShipmentService

class TestShipmentService : ShipmentService {
    var shippedOrder: Order? = null
        private set

    override fun ship(order: Order) {
        shippedOrder = order
    }
}