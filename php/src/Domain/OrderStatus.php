<?php

namespace Archel\TellDontAsk\Domain;

/**
 * Class OrderStatus
 * @package Archel\TellDontAsk\Domain
 */
class OrderStatus
{
    /**
     * @var string
     */
    private $type;

    const APPROVED = 'APPROVED';
    const REJECTED = 'REJECTED';
    const SHIPPED = 'SHIPPED';
    const CREATED = 'CREATED';

    /**
     * OrderStatus constructor.
     * @param string $type
     */
    private function __construct(string $type)
    {
        $this->type = $type;
    }

    /**
     * @return OrderStatus
     */
    public static function approved() : self
    {
        return new static(self::APPROVED);
    }

    /**
     * @return OrderStatus
     */
    public static function rejected() : self
    {
        return new static(self::REJECTED);
    }

    /**
     * @return OrderStatus
     */
    public static function shipped() : self
    {
        return new static(self::SHIPPED);
    }

    /**
     * @return OrderStatus
     */
    public static function created() : self
    {
        return new static(self::CREATED);
    }

    /**
     * @return string
     */
    public function getType() : string
    {
        return $this->type;
    }
}