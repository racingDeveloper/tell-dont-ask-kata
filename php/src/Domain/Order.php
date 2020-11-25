<?php

namespace Archel\TellDontAsk\Domain;

/**
 * Class Order
 * @package Archel\TellDontAsk\Domain
 */
class Order
{
    /**
     * @var float
     */
    private $total;

    /**
     * @var string
     */
    private $currency;

    /**
     * @var array
     */
    private $items = [];

    /**
     * @var float
     */
    private $tax;

    /**
     * @var OrderStatus
     */
    private $status;

    /**
     * @var int
     */
    private $id;

    /**
     * @return float
     */
    public function getTotal() : float
    {
        return $this->total;
    }

    /**
     * @param float $total
     */
    public function setTotal(float $total)
    {
        $this->total = $total;
    }

    /**
     * @return string
     */
    public function getCurrency() : string
    {
        return $this->currency;
    }

    /**
     * @param string $currency
     */
    public function setCurrency(string $currency) : void
    {
        $this->currency = $currency;
    }

    /**
     * @return array
     */
    public function getItems() : array
    {
        return $this->items;
    }

    /**
     * @param OrderItem[] ...$items
     */
    public function setItems(OrderItem... $items) : void
    {
        $this->items = $items;
    }

    /**
     * @param OrderItem $item
     */
    public function addItem(OrderItem $item) : void
    {
        $this->items[] = $item;
    }

    /**
     * @return float
     */
    public function getTax() : float
    {
        return $this->tax;
    }

    /**
     * @param float $tax
     */
    public function setTax(float $tax) : void
    {
        $this->tax = $tax;
    }

    /**
     * @return OrderStatus
     */
    public function getStatus() : OrderStatus
    {
        return $this->status;
    }

    /**
     * @param OrderStatus $orderStatus
     */
    public function setStatus(OrderStatus $orderStatus) : void
    {
        $this->status = $orderStatus;
    }

    /**
     * @return int
     */
    public function getId() : int
    {
        return $this->id;
    }

    /**
     * @param int $id
     */
    public function setId(int $id) : void
    {
        $this->id = $id;
    }
}