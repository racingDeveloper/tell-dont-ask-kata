package doubles

import ordershipping.domain.Order
import ordershipping.repository.OrderRepository

import scala.collection.mutable

class TestOrderRepository extends OrderRepository {
  val orders: mutable.MutableList[Order] = mutable.MutableList.empty
  var savedOrder: Order = _

  override def save(order: Order): Unit =
    savedOrder = order

  override def getById(orderId: Int): Option[Order] =
    orders.find(o => o.id == orderId)

  def addOrder(order: Order): Unit = orders += order
}
