package doubles

import ordershipping.domain.Order
import ordershipping.repository.OrderRepository

import scala.collection.mutable

class TestOrderRepository extends OrderRepository {
  var insertedOrder: Order = _
  val orders: mutable.MutableList[Order] = mutable.MutableList.empty

  override def save(order: Order): Unit =
    insertedOrder = order

  override def getById(orderId: Int): Option[Order] =
    orders.find(o => o.id == orderId)

  def addOrder(order: Order): Unit = orders += order
}
