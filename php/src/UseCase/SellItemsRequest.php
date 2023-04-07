<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

use Doctrine\Common\Collections\ArrayCollection;

class SellItemsRequest
{
    private ArrayCollection $items;

    /**
     * @return ArrayCollection
     */
    public function getItems(): ArrayCollection
    {
        return $this->items;
    }

    /**
     * @param ArrayCollection $items
     * @return SellItemsRequest
     */
    public function setItems(ArrayCollection $items): SellItemsRequest
    {
        $this->items = $items;
        return $this;
    }

    public function __construct()
    {
        $this->items = new ArrayCollection();
    }


}