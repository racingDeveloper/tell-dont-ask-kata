# frozen_string_literal: true

class TestOrderRepository
  def initialize
    @orders = []
    @inserted_order = nil
  end

  def saved_order
    @inserted_order
  end

  def save(order)
    @inserted_order = order
  end

  def get_by_id(order_id)
    @orders.first { |o| o.id == order_id }
  end

  def add_order(order)
    @orders << order
  end
end
