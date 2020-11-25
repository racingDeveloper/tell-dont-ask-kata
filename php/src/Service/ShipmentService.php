<?php

namespace Archel\TellDontAsk\Service;

use Archel\TellDontAsk\Domain\Order;

/**
 * Class ShipmentService
 * @package Archel\TellDontAsk\Service
 */
interface ShipmentService
{
    public function ship(Order $order) : void;
}