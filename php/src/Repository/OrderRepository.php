<?php

namespace Archel\TellDontAsk\Repository;

use Archel\TellDontAsk\Domain\Order;

/**
 * Class OrderRepository
 * @package Archel\TellDontAsk\Repository
 */
interface OrderRepository
{
    /**
     * @param Order $order
     */
    public function save(Order $order) : void;

    /**
     * @param int $orderId
     * @return Order
     */
    public function getById(int $orderId) : ?Order;
}