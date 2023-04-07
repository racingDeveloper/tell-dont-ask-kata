<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

class OrderShipmentRequest
{
    private int $id = 0;

    public function __construct()
    {
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
     * @return OrderShipmentRequest
     */
    public function setId(int $id): OrderShipmentRequest
    {
        $this->id = $id;
        return $this;
    }


}