<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Repository;

use Pitchart\TellDontAskKata\Domain\Product;

interface ProductCatalog
{

    public function getByName(string $name): ?Product;
}