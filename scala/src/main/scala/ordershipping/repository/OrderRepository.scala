package ordershipping.repository

import ordershipping.domain.Order

trait OrderRepository {
  def save(order: Order): Unit
  def getById(orderId: Int): Option[Order]
}
