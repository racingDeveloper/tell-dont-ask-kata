<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Domain;

class OrderItem
{
    private int $quantity = 0;

    private float $tax;

    private float $taxedAmount;

    private Product $product;

    /**
     * @param float $tax
     * @return OrderItem
     */
    public function setTax(float $tax): OrderItem
    {
        $this->tax = $tax;
        return $this;
    }

    /**
     * @param float $taxedAmount
     * @return OrderItem
     */
    public function setTaxedAmount(float $taxedAmount): OrderItem
    {
        $this->taxedAmount = $taxedAmount;
        return $this;
    }

    /**
     * @return Product
     */
    public function getProduct(): Product
    {
        return $this->product;
    }

    /**
     * @return int
     */
    public function getQuantity(): int
    {
        return $this->quantity;
    }

    /**
     * @return float
     */
    public function getTax(): float
    {
        return $this->tax;
    }

    /**
     * @return float
     */
    public function getTaxedAmount(): float
    {
        return $this->taxedAmount;
    }

    public function setProduct(Product $product): self
    {
        $this->product = $product;
        return $this;
    }

    public function setQuantity(int $quantity): self
    {
        $this->quantity = $quantity;
        return $this;
    }
}