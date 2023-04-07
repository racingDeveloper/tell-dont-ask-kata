<?php

namespace Tests\Pitchart\TellDontAskKata\Doubles;

use Doctrine\Common\Collections\ArrayCollection;
use Pitchart\TellDontAskKata\Domain\Order;
use Pitchart\TellDontAskKata\Repository\OrderRepository;

class InMemoryOrderRepository implements OrderRepository
{

    private ArrayCollection $orders;
    private ?Order $insertedOrder = null;


    public function __construct()
    {
        $this->orders = new ArrayCollection();
    }

    public function getSavedOrder(): ?Order
    {
        return $this->insertedOrder;
    }

    public function save(Order $order): void
    {
        $this->orders->add($order);
        $this->insertedOrder = $order;
    }

    public function addOrder(Order $order): void
    {
        $this->save($order);
    }

    public function getById($orderId): Order
    {
        return $this->orders->findFirst(fn(int $key, Order $order) => $order->getId() == $orderId);
    }
}