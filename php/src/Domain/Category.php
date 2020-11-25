<?php

namespace Archel\TellDontAsk\Domain;

/**
 * Class Category
 * @package Archel\TellDontAsk\Domain
 */
class Category
{
    /**
     * @var string
     */
    private $name;

    /**
     * @var float
     */
    private $taxPercentage;

    /**
     * @return string
     */
    public function getName() : string
    {
        return $this->name;
    }

    /**
     * @param string $name
     */
    public function setName(string $name) : void
    {
        $this->name = $name;
    }

    /**
     * @return float
     */
    public function getTaxPercentage() : float
    {
        return $this->taxPercentage;
    }

    /**
     * @param float $taxPercentage
     */
    public function setTaxPercentage(float $taxPercentage) : void
    {
        $this->taxPercentage = $taxPercentage;
    }
}