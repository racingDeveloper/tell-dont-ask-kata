<?php

namespace Archel\TellDontAskTest\Doubles;

use Archel\TellDontAsk\Domain\Product;
use Archel\TellDontAsk\Repository\ProductCatalog;

/**
 * Class InMemoryProductCatalog
 * @package Archel\TellDontAskTest\Doubles
 */
class InMemoryProductCatalog implements ProductCatalog
{
    /**
     * @var array
     */
    private $products = [];

    /**
     * InMemoryProductCatalog constructor.
     * @param Product[] ...$products
     */
    public function __construct(Product... $products)
    {
        $this->products = $products;
    }

    /**
     * @param string $name
     * @return Product|null
     */
    public function getByName(string $name) : ?Product
    {
        $product = array_filter($this->products, function ($product) use ($name) {
            return $product->getName() === $name;
        });

        return !empty($product) ? current($product) : null;
    }
}