package ordershipping.domain

class OrderItem(
    val product: ordershipping.domain.Product,
    var quantity: Int,
    var taxedAmount: Double,
    var tax: Double
)
