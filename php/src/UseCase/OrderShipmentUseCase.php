<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

use Pitchart\TellDontAskKata\Domain\OrderStatus;
use Pitchart\TellDontAskKata\Repository\OrderRepository;
use Pitchart\TellDontAskKata\Services\ShipmentService;

class OrderShipmentUseCase
{

    private OrderRepository $repository;
    private ShipmentService $shipmentService;

    /**
     * @param OrderRepository $repository
     * @param ShipmentService $shipmentService
     */
    public function __construct(OrderRepository $repository, ShipmentService $shipmentService)
    {
        $this->repository = $repository;
        $this->shipmentService = $shipmentService;
    }

    /**
     * @throws OrderCannotBeShippedTwiceException
     * @throws OrderCannotBeShippedException
     */
    public function run(OrderShipmentRequest $request): void
    {
        $order = $this->repository->GetById($request->getId());

        if ($order->getStatus() == OrderStatus::Created || $order->getStatus() == OrderStatus::Rejected) {
            throw new OrderCannotBeShippedException();
        }

        if ($order->getStatus() == OrderStatus::Shipped) {
            throw new OrderCannotBeShippedTwiceException();
        }

        $this->shipmentService->ship($order);

        $order->setStatus(OrderStatus::Shipped);
        $this->repository->save($order);
    }
}