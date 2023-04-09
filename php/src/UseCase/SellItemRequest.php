<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

class SellItemRequest
{

    private string $productName;

    private int $quantity;

    public function __construct()
    {
    }

    /**
     * @param int $quantity
     * @return SellItemRequest
     */
    public function setQuantity(int $quantity): SellItemRequest
    {
        $this->quantity = $quantity;
        return $this;
    }

    /**
     * @return string
     */
    public function getProductName(): string
    {
        return $this->productName;
    }

    /**
     * @return int
     */
    public function getQuantity(): int
    {
        return $this->quantity;
    }

    public function setProductName(string $productName): self
    {
        $this->productName = $productName;
        return $this;
    }
}