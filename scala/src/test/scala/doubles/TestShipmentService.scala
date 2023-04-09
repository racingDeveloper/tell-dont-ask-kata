package doubles

import ordershipping.domain.Order
import ordershipping.service.ShipmentService

class TestShipmentService extends ShipmentService {
  private var orderToShip: Order = _

  override def ship(order: Order): Unit = orderToShip = order

  def shippedOrder(): Order = orderToShip
}
