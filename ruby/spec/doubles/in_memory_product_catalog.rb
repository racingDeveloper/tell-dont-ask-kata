# frozen_string_literal: true

class InMemoryProductCatalog
  def initialize(products)
    @products = products
  end

  def get_by_name(name)
    @products.find { |p| p.name == name }
  end
end
