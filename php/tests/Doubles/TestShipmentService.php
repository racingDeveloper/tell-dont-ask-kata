<?php

namespace Tests\Pitchart\TellDontAskKata\Doubles;

use Pitchart\TellDontAskKata\Domain\Order;
use Pitchart\TellDontAskKata\Services\ShipmentService;

class TestShipmentService implements ShipmentService
{

    private ?Order $shippedOrder = null;

    public function ship(Order $order)
    {
        $this->shippedOrder = $order;
    }

    public function getShippedOrder(): ?Order
    {
        return $this->shippedOrder;
    }
}