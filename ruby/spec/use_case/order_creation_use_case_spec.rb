# frozen_string_literal: true

require_relative '../doubles/test_order_repository'
require_relative '../doubles/in_memory_product_catalog'

require 'use_case/order_creation_use_case'
require 'use_case/sell_item_request'
require 'use_case/sell_items_request'

require 'domain/product'
require 'domain/category'

RSpec.describe OrderCreationUseCase do
  let(:order_repository) { TestOrderRepository.new }
  let(:food) do
    Category.new.tap do |c|
      c.name = 'food'
      c.tax_percentage = 10.0
    end
  end

  let(:product_catalog) do
    InMemoryProductCatalog.new(
      [
        Product.new.tap do |product|
          product.name = 'salad'
          product.price = 3.56
          product.category = food
        end,
        Product.new.tap do |product|
          product.name = 'tomato'
          product.price = 4.65
          product.category = food
        end
      ]
    )
  end

  let(:use_case) { OrderCreationUseCase.new(order_repository, product_catalog) }

  it 'sells multiple items' do
    salad_request = SellItemRequest.new
    salad_request.product_name = 'salad'
    salad_request.quantity = 2

    tomato_request = SellItemRequest.new
    tomato_request.product_name = 'tomato'
    tomato_request.quantity = 3

    request = SellItemsRequest.new
    request.requests << salad_request
    request.requests << tomato_request

    use_case.run(request)

    inserted_order = order_repository.saved_order

    expect(inserted_order.status).to eq(OrderStatus::CREATED)
    expect(inserted_order.total).to eq(23.20)
    expect(inserted_order.tax).to eq(2.13)
    expect(inserted_order.currency).to eq('EUR')
    expect(inserted_order.items.count).to eq(2)

    expect(inserted_order.items[0].product.name).to eq('salad')
    expect(inserted_order.items[0].product.price).to eq(3.56)
    expect(inserted_order.items[0].quantity).to eq(2)
    expect(inserted_order.items[0].taxed_amount).to eq(7.84)
    expect(inserted_order.items[0].tax).to eq(0.72)

    expect(inserted_order.items[1].product.name).to eq('tomato')
    expect(inserted_order.items[1].product.price).to eq(4.65)
    expect(inserted_order.items[1].quantity).to eq(3)
    expect(inserted_order.items[1].taxed_amount).to eq(15.36)
    expect(inserted_order.items[1].tax).to eq(1.41)
  end

  it 'cannot sell an unknown product' do
    request = SellItemsRequest.new
    request.requests = []

    unknown_product_request = SellItemRequest.new
    unknown_product_request.product_name = 'unknown product'
    request.requests << unknown_product_request

    expect { use_case.run(request) }.to raise_error(described_class::UnknownProductError)
  end
end
