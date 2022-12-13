package doubles

import ordershipping.domain.Order
import ordershipping.service.ShipmentService

class TestShipmentService extends ShipmentService {
  var shippedOrder: Order = null
  override def ship(order: Order): Unit = shippedOrder = order
}
