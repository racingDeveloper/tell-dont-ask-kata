<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\Domain;

enum OrderStatus
{

    case Created;
    case Approved;
    case Shipped;
    case Rejected;
}