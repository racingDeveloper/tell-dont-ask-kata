<?php

namespace Archel\TellDontAsk\UseCase;

/**
 * Class SellItemRequest
 * @package Archel\TellDontAsk\UseCase
 */
class SellItemRequest
{
    /**
     * @var int
     */
    private $quantity;

    /**
     * @var string
     */
    private $productName;

    /**
     * @param int $quantity
     */
    public function setQuantity(int $quantity) : void
    {
        $this->quantity = $quantity;
    }

    /**
     * @return int
     */
    public function getQuantity() : int
    {
        return $this->quantity;
    }

    /**
     * @param string $productName
     */
    public function setProductName(string $productName) : void
    {
        $this->productName = $productName;
    }

    /**
     * @return string
     */
    public function getProductName() : string
    {
        return $this->productName;
    }
}