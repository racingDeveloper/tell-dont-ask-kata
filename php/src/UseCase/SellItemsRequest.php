<?php

namespace Archel\TellDontAsk\UseCase;

/**
 * Class SellItemsRequest
 * @package Archel\TellDontAsk\UseCase
 */
class SellItemsRequest
{
    /**
     * @var array
     */
    private $requests;

    /**
     * @param SellItemRequest[] ...$requests
     */
    public function setRequests(SellItemRequest... $requests) : void
    {
        $this->requests = $requests;
    }

    /**
     * @return array
     */
    public function getRequests() : array
    {
        return $this->requests;
    }
}