# frozen_string_literal: true

require 'use_case/order_approval_use_case'
require 'use_case/order_approval_request'
require 'domain/order'
require_relative '../doubles/test_order_repository'

RSpec.describe OrderApprovalUseCase do
  let(:order_repository) { TestOrderRepository.new }
  let(:use_case) { described_class.new(order_repository) }

  let(:initial_order) do
    Order.new.tap do |order|
      order.id = 1
    end
  end

  let(:request) { OrderApprovalRequest.new }

  before do
    order_repository.add_order(initial_order)
    request.order_id = initial_order.id
  end

  it 'approved existing order' do
    request.approved = true

    use_case.run(request)
    saved_order = order_repository.saved_order
    expect(saved_order.status).to eq(OrderStatus::APPROVED)
  end

  it 'rejected existing order' do
    initial_order.status = OrderStatus::CREATED

    request.approved = false

    use_case.run(request)

    saved_order = order_repository.saved_order

    expect(saved_order.status).to eq(OrderStatus::REJECTED)
  end

  it 'cannot approve rejected order' do
    initial_order.status = OrderStatus::REJECTED

    request.approved = true

    expect { use_case.run(request) }.to raise_error(described_class::RejectedOrderCannotBeApprovedError)
  end

  it 'cannot reject approved order' do
    initial_order.status = OrderStatus::APPROVED

    request.approved = false

    expect { use_case.run(request) }.to raise_error(described_class::ApprovedOrderCannotBeRejectedError)
  end

  it 'cannot approve shipepd orders' do
    initial_order.status = OrderStatus::SHIPPED

    request.approved = true

    expect { use_case.run(request) }.to raise_error(described_class::ShippedOrdersCannotBeChangedError)
  end

  it 'cannot reject shipped orders' do
    initial_order.status = OrderStatus::SHIPPED

    request.approved = true

    expect { use_case.run(request) }.to raise_error(described_class::ShippedOrdersCannotBeChangedError)
  end
end
