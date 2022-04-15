package ordershipping.service

import ordershipping.domain.Order

trait ShipmentService {
  def ship(order: Order): Unit
}
