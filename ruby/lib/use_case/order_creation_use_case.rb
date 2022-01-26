# frozen_string_literal: true

require_relative '../domain/order'
require_relative '../domain/order_item'
require_relative '../domain/order_status'
require_relative '../domain/product'

class OrderCreationUseCase
  class UnknownProductError < StandardError; end

  def initialize(order_repository, product_catalog)
    @order_repository = order_repository
    @product_catalog = product_catalog
  end

  # @param request [SellItemsRequest]
  def run(request)
    order = Order.new
    order.status = OrderStatus::CREATED
    order.items = []
    order.currency = 'EUR'
    order.total = 0.0
    order.tax = 0.0

    request.requests.each do |item_request|
      product = @product_catalog.get_by_name(item_request.product_name)

      raise UnknownProductError if product.nil?

      unitary_tax = ((product.price / 100.0) * product.category.tax_percentage).ceil(2)
      unitary_taxed_amount = (product.price + unitary_tax).ceil(2)
      taxed_amount = (unitary_taxed_amount * item_request.quantity).ceil(2)
      tax_amount = (unitary_tax * item_request.quantity).ceil(2)

      order_item = OrderItem.new
      order_item.product = product
      order_item.quantity = item_request.quantity
      order_item.tax = tax_amount
      order_item.taxed_amount = taxed_amount

      order.items << order_item
      order.total += taxed_amount
      order.tax += tax_amount
    end

    @order_repository.save(order)
  end
end
