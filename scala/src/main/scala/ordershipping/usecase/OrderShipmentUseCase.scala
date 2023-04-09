package ordershipping.usecase

import ordershipping.domain.OrderStatus
import ordershipping.repository.OrderRepository
import ordershipping.service.ShipmentService

class OrderShipmentUseCase(
    val orderRepository: OrderRepository,
    val shipmentService: ShipmentService
) {
  def run(request: OrderShipmentRequest): Unit = {
    orderRepository
      .getById(request.orderId)
      .foreach(order => {
        if (
          order.status == OrderStatus.Created ||
          order.status == OrderStatus.Rejected
        ) throw new OrderCannotBeShippedException

        if (order.status == OrderStatus.Shipped)
          throw new OrderCannotBeShippedTwiceException

        shipmentService.ship(order)
        order.status = OrderStatus.Shipped
        orderRepository.save(order)
      })
  }
}
