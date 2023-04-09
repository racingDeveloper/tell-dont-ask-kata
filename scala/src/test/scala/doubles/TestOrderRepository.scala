package doubles

import ordershipping.domain.Order
import ordershipping.repository.OrderRepository

import scala.collection.mutable

class TestOrderRepository extends OrderRepository {
  private var insertedOrder: Order = _
  private val orders: mutable.MutableList[Order] = mutable.MutableList.empty

  override def save(order: Order): Unit =
    insertedOrder = order

  override def getById(orderId: Int): Option[Order] =
    orders.find(o => o.id == orderId)

  def addOrder(order: Order): Unit = orders += order
  def savedOrder(): Order = insertedOrder
}
