<?php

namespace Archel\TellDontAsk\Repository;

use Archel\TellDontAsk\Domain\Product;

/**
 * Interface ProductCatalog
 * @package Archel\TellDontAsk\Repository
 */
interface ProductCatalog
{
    public function getByName(string $name) : ?Product;
}
