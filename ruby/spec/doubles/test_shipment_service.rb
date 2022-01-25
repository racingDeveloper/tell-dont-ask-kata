# frozen_string_literal: true

class TestShipmentService
  attr_reader :shipped_order

  def initialize
    @shipped_order = nil
  end

  def ship(order)
    @shipped_order = order
  end
end
