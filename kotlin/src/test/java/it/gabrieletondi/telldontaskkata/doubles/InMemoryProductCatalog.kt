package it.gabrieletondi.telldontaskkata.doubles

import it.gabrieletondi.telldontaskkata.domain.Product
import it.gabrieletondi.telldontaskkata.repository.ProductCatalog

class InMemoryProductCatalog(private val products: List<Product>) : ProductCatalog {
    override fun getByName(name: String): Product? {
        return products.stream().filter { p: Product -> p.name == name }.findFirst().orElse(null)
    }
}