<?php

namespace Archel\TellDontAsk\UseCase;

/**
 * Class OrderApprovalRequest
 * @package Archel\TellDontAsk\UseCase
 */
class OrderApprovalRequest
{
    /**
     * @var int
     */
    private $orderId;

    /**
     * @var bool
     */
    private $approved;

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

    /**
     * @param bool $approved
     */
    public function setApproved(bool $approved) : void
    {
        $this->approved = $approved;
    }

    /**
     * @return bool
     */
    public function isApproved() : bool
    {
        return $this->approved;
    }
}