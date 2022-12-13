package ordershipping.usecase

import ordershipping.domain.OrderStatus
import ordershipping.repository.OrderRepository
import ordershipping.service.ShipmentService

class OrderShipmentUseCase(
                            private val orderRepository: OrderRepository,
                            private val shipmentService: ShipmentService
                          ) {
  def run(request: OrderShipmentRequest): Unit = {
    orderRepository
      .getById(request.orderId)
      .foreach(order => {
        if (
          order.status == OrderStatus.Created
            || order.status == OrderStatus.Rejected
        ) throw OrderCannotBeShippedException()

        if (order.status == OrderStatus.Shipped)
          throw OrderCannotBeShippedTwiceException()

        shipmentService.ship(order)

        order.status = OrderStatus.Shipped
        orderRepository.save(order)
      })
  }
}
