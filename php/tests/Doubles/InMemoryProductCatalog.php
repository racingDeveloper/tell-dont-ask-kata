<?php

namespace Tests\Pitchart\TellDontAskKata\Doubles;

use Doctrine\Common\Collections\ArrayCollection;
use Pitchart\TellDontAskKata\Domain\Product;
use Pitchart\TellDontAskKata\Repository\ProductCatalog;

class InMemoryProductCatalog implements ProductCatalog
{
    private ArrayCollection $products;

    /**
     * @param ArrayCollection $products
     */
    public function __construct(ArrayCollection $products)
    {
        $this->products = $products;
    }

    public function getByName(string $name): ?Product
    {
        return $this->products->findFirst(static function (int $key, Product $product) use ($name) {
            return $product->getName() === $name;
        });
    }
}