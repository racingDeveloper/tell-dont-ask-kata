<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Domain;

class Category
{
    private string $name;

    private float $taxPercentage;

    /**
     * @return float
     */
    public function getTaxPercentage(): float
    {
        return $this->taxPercentage;
    }

    /**
     * @param float $taxPercentage
     * @return Category
     */
    public function setTaxPercentage(float $taxPercentage): Category
    {
        $this->taxPercentage = $taxPercentage;
        return $this;
    }

    /**
     * @return string
     */
    public function getName(): string
    {
        return $this->name;
    }

    /**
     * @param string $name
     * @return Category
     */
    public function setName(string $name): Category
    {
        $this->name = $name;
        return $this;
    }
}