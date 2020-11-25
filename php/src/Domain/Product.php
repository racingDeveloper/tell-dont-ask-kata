<?php

namespace Archel\TellDontAsk\Domain;

/**
 * Class Product
 * @package Archel\TellDontAsk\Domain
 */
class Product
{
    /**
     * @var string
     */
    private $name;

    /**
     * @var float
     */
    private $price;

    /**
     * @var Category
     */
    private $category;

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
    public function getPrice() : float
    {
        return $this->price;
    }

    /**
     * @param float $price
     */
    public function setPrice(float $price) : void
    {
        $this->price = $price;
    }

    /**
     * @return Category
     */
    public function getCategory() : Category
    {
        return $this->category;
    }

    /**
     * @param Category $category
     */
    public function setCategory(Category $category) : void
    {
        $this->category = $category;
    }
}