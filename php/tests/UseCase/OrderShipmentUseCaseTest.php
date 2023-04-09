<?php

namespace Tests\Pitchart\TellDontAskKata\UseCase;

use PHPUnit\Framework\Assert;
use PHPUnit\Framework\TestCase;
use Pitchart\TellDontAskKata\Domain\Order;
use Pitchart\TellDontAskKata\Domain\OrderStatus;
use Pitchart\TellDontAskKata\UseCase\OrderCannotBeShippedException;
use Pitchart\TellDontAskKata\UseCase\OrderCannotBeShippedTwiceException;
use Pitchart\TellDontAskKata\UseCase\OrderShipmentRequest;
use Pitchart\TellDontAskKata\UseCase\OrderShipmentUseCase;
use Tests\Pitchart\TellDontAskKata\Doubles\InMemoryOrderRepository;
use Tests\Pitchart\TellDontAskKata\Doubles\TestShipmentService;

class OrderShipmentUseCaseTest extends TestCase
{
    private InMemoryOrderRepository $orderRepository;

    private OrderShipmentUseCase $useCase;
    private TestShipmentService $shipmentService;

    protected function setUp(): void
    {
        parent::setUp();
        $this->orderRepository = new InMemoryOrderRepository();
        $this->shipmentService = new TestShipmentService();

        $this->useCase = new OrderShipmentUseCase($this->orderRepository, $this->shipmentService);
    }

    public function test_ship_approved_order(): void
    {
        $initialOrder = (new Order)->setStatus(OrderStatus::Approved)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderShipmentRequest())->setId(1);

        $this->useCase->run($request);

        $savedOrder = $this->orderRepository->getSavedOrder();
        Assert::assertEquals(OrderStatus::Shipped, $savedOrder->getStatus());
    }


    public function test_created_orders_can_not_be_shipped()
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Created)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderShipmentRequest())->setId(1);

        $this->expectException(OrderCannotBeShippedException::class);

        $this->useCase->run($request);

        Assert:: assertNull($this->orderRepository->getSavedOrder());
        Assert:: assertNull($this->shipmentService->getShippedOrder());
    }


    public function test_rejected_orders_can_not_be_shipped()
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Rejected)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderShipmentRequest())->setId(1);

        $this->expectException(OrderCannotBeShippedException::class);

        $this->useCase->run($request);

        Assert:: assertNull($this->orderRepository->getSavedOrder());
        Assert:: assertNull($this->shipmentService->getShippedOrder());
    }


    public function test_shipped_orders_can_not_be_shipped_again()
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Shipped)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderShipmentRequest())->setId(1);

        $this->expectException(OrderCannotBeShippedTwiceException::class);

        $this->useCase->run($request);

        Assert:: assertNull($this->orderRepository->getSavedOrder());
        Assert:: assertNull($this->shipmentService->getShippedOrder());
    }

}
