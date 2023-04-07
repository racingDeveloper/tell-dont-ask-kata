<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Domain;

use Doctrine\Common\Collections\ArrayCollection;

class Order
{
    private int $id;

    private OrderStatus $status;

    private float $tax = 0;

    private float $total = 0;

    private ArrayCollection $items;

    private string $currency;

    public function __construct()
    {
        $this->items = new ArrayCollection();
    }

    /**
     * @return int
     */
    public function getId(): int
    {
        return $this->id;
    }

    /**
     * @param int $id
     * @return Order
     */
    public function setId(int $id): Order
    {
        $this->id = $id;
        return $this;
    }


    /**
     * @return OrderStatus
     */
    public function getStatus(): OrderStatus
    {
        return $this->status;
    }

    /**
     * @param OrderStatus $orderStatus
     * @return Order
     */
    public function setStatus(OrderStatus $orderStatus): Order
    {
        $this->status = $orderStatus;
        return $this;
    }

    /**
     * @return float
     */
    public function getTax(): float
    {
        return $this->tax;
    }

    /**
     * @param float $tax
     * @return Order
     */
    public function setTax(float $tax): Order
    {
        $this->tax = $tax;
        return $this;
    }

    /**
     * @return float
     */
    public function getTotal(): float
    {
        return $this->total;
    }

    /**
     * @param float $total
     * @return Order
     */
    public function setTotal(float $total): Order
    {
        $this->total = $total;
        return $this;
    }

    /**
     * @return ArrayCollection
     */
    public function getItems(): ArrayCollection
    {
        return $this->items;
    }

    /**
     * @param ArrayCollection $items
     * @return Order
     */
    public function setItems(ArrayCollection $items): Order
    {
        $this->items = $items;
        return $this;
    }

    /**
     * @return string
     */
    public function getCurrency(): string
    {
        return $this->currency;
    }

    /**
     * @param string $currency
     * @return Order
     */
    public function setCurrency(string $currency): Order
    {
        $this->currency = $currency;
        return $this;
    }

}