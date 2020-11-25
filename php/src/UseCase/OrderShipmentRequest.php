<?php

namespace Archel\TellDontAsk\UseCase;

/**
 * Class OrderShipmentRequest
 * @package Archel\TellDontAsk\UseCase
 */
class OrderShipmentRequest
{
    /**
     * @var int
     */
    private $orderId;

    /**
     * @param int $orderId
     */
    public function setOrderId(int $orderId) : void
    {
        $this->orderId = $orderId;
    }

    /**
     * @return int
     */
    public function getOrderId() : int
    {
        return $this->orderId;
    }
}