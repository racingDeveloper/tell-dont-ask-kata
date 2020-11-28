package it.gabrieletondi.telldontaskkata.useCase

import it.gabrieletondi.telldontaskkata.domain.OrderStatus
import it.gabrieletondi.telldontaskkata.repository.OrderRepository

class OrderApprovalUseCase(private val orderRepository: OrderRepository) {
    fun run(request: OrderApprovalRequest) {
        val order = orderRepository.getById(request.orderId)
        if (order!!.status == OrderStatus.SHIPPED) {
            throw ShippedOrdersCannotBeChangedException()
        }
        if (request.isApproved && order.status == OrderStatus.REJECTED) {
            throw RejectedOrderCannotBeApprovedException()
        }
        if (!request.isApproved && order.status == OrderStatus.APPROVED) {
            throw ApprovedOrderCannotBeRejectedException()
        }
        order.status = if (request.isApproved) OrderStatus.APPROVED else OrderStatus.REJECTED
        orderRepository.save(order)
    }
}