<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

class OrderApprovalRequest
{
    private int $id = 0;

    private bool $approved = false;

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
     * @return OrderApprovalRequest
     */
    public function setId(int $id): OrderApprovalRequest
    {
        $this->id = $id;
        return $this;
    }

    /**
     * @return bool
     */
    public function isApproved(): bool
    {
        return $this->approved;
    }

    /**
     * @param bool $approved
     * @return OrderApprovalRequest
     */
    public function setApproved(bool $approved): OrderApprovalRequest
    {
        $this->approved = $approved;
        return $this;
    }
}