package it.gabrieletondi.telldontaskkata.service

import it.gabrieletondi.telldontaskkata.domain.Order

interface ShipmentService {
    fun ship(order: Order)
}