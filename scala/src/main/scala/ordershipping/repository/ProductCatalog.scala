package ordershipping.repository

trait ProductCatalog {
  def getByName(name: String): Option[ordershipping.domain.Product]
}
