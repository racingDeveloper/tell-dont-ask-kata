<?php

namespace Archel\TellDontAskTest\Doubles;

use Archel\TellDontAsk\Domain\Order;
use Archel\TellDontAsk\Service\ShipmentService;

/**
 * Class TestShipmentService
 * @package Archel\TellDontAskTest\Doubles
 */
class TestShipmentService implements ShipmentService
{
    private $shippedOrder = null;

    /**
     * @return Order|null
     */
    public function getShippedOrder() : ?Order
    {
        return $this->shippedOrder;
    }

    /**
     * @param Order $order
     */
    public function ship(Order $order): void
    {
        $this->shippedOrder = $order;
    }
}