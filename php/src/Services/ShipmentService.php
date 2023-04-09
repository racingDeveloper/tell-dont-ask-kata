<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Services;

use Pitchart\TellDontAskKata\Domain\Order;

interface ShipmentService
{
    public function ship(Order $order);
}