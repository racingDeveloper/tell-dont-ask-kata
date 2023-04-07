<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Repository;

use Pitchart\TellDontAskKata\Domain\Order;

interface OrderRepository
{

    public function save(Order $order): void;

    public function getById($orderId): Order;
}