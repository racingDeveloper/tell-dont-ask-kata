# frozen_string_literal: true

require 'use_case/order_shipment_use_case'
require 'use_case/order_shipment_request'

require 'domain/order'
require 'domain/order_status'

require_relative '../doubles/test_shipment_service'

RSpec.describe OrderShipmentUseCase do
  let(:order_repository) do
    r = TestOrderRepository.new
    r.add_order(initial_order)
    r
  end
  let(:shipment_service) { TestShipmentService.new }
  let(:use_case) { OrderShipmentUseCase.new(order_repository, shipment_service) }
  let(:request) do
    r = OrderShipmentRequest.new
    r.order_id = initial_order.id
    r
  end

  let(:initial_order) do
    order = Order.new
    order.id = 1
    order
  end

  it 'ships approved orders' do
    initial_order.status = OrderStatus::APPROVED

    use_case.run(request)

    expect(order_repository.saved_order.status).to eq(OrderStatus::SHIPPED)
    expect(shipment_service.shipped_order).to eq(initial_order)
  end

  it 'cannot ship created orders' do
    initial_order.status = OrderStatus::CREATED

    expect { use_case.run(request) }.to raise_error(described_class::OrderCannotBeShippedError)

    expect(order_repository.saved_order).to be_nil
    expect(shipment_service.shipped_order).to be_nil
  end

  it 'cannot ship rejected orders' do
    initial_order.status = OrderStatus::REJECTED

    expect { use_case.run(request) }.to raise_error(described_class::OrderCannotBeShippedError)

    expect(order_repository.saved_order).to be_nil
    expect(shipment_service.shipped_order).to be_nil
  end

  it 'cannot ship shipped orders again' do
    initial_order.status = OrderStatus::SHIPPED

    expect { use_case.run(request) }.to raise_error(described_class::OrderCannotBeShippedTwiceError)

    expect(order_repository.saved_order).to be_nil
    expect(shipment_service.shipped_order).to be_nil
  end
end
