package ordershipping.domain

object OrderStatus extends Enumeration {
  type OrderStatus = Value
  val Approved, Rejected, Shipped, Created = Value
}
