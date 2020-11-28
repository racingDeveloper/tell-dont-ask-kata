package it.gabrieletondi.telldontaskkata.repository

import it.gabrieletondi.telldontaskkata.domain.Product

interface ProductCatalog {
    fun getByName(name: String): Product?
}