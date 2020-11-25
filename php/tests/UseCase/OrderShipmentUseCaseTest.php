<?php

namespace Archel\TellDontAskTest\UseCase;

use Archel\TellDontAsk\Domain\Order;
use Archel\TellDontAsk\Domain\OrderStatus;
use Archel\TellDontAsk\Repository\OrderRepository;
use Archel\TellDontAsk\Service\ShipmentService;
use Archel\TellDontAsk\UseCase\OrderCannotBeShippedException;
use Archel\TellDontAsk\UseCase\OrderCannotBeShippedTwiceException;
use Archel\TellDontAsk\UseCase\OrderShipmentRequest;
use Archel\TellDontAsk\UseCase\OrderShipmentUseCase;
use Archel\TellDontAsk\UseCase\ShippedOrdersCannotBeChangedException;
use Archel\TellDontAskTest\Doubles\TestOrderRepository;
use Archel\TellDontAskTest\Doubles\TestShipmentService;
use PHPUnit\Framework\TestCase;

/**
 * Class OrderShipmentUseCaseTest
 * @package Archel\TellDontAskTest\UseCase
 */
class OrderShipmentUseCaseTest extends TestCase
{
    /**
     * @var TestOrderRepository
     */
    private $orderRepository;

    /**
     * @var TestShipmentService
     */
    private $shipmentService;

    /**
     * @var OrderShipmentUseCase
     */
    private $useCase;

    public function setUp()
    {
        $this->orderRepository = new TestOrderRepository();
        $this->shipmentService = new TestShipmentService();
        $this->useCase = new OrderShipmentUseCase($this->orderRepository, $this->shipmentService);
    }

    /**
     * @test
     */
    public function shipApprovedOrder() : void
    {
        $initialOrder = new Order();
        $initialOrder->setId(1);
        $initialOrder->setStatus(OrderStatus::approved());
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderShipmentRequest();
        $request->setOrderId(1);

        $this->useCase->run($request);

        $this->assertEquals(OrderStatus::SHIPPED, $this->orderRepository->getSavedOrder()->getStatus()->getType());
        $this->assertEquals($this->shipmentService->getShippedOrder()->getId(), $initialOrder->getId());
    }

    /**
     * @test
     */
    public function createdOrdersCannotBeShipped() : void
    {
        $this->expectException(OrderCannotBeShippedException::class);

        $initialOrder = new Order();
        $initialOrder->setId(1);
        $initialOrder->setStatus(OrderStatus::created());
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderShipmentRequest();
        $request->setOrderId(1);

        $this->useCase->run($request);

        $this->assertEmpty($this->orderRepository->getSavedOrder());
        $this->assertEmpty($this->shipmentService->getShippedOrder());
    }

    /**
     * @test
     */
    public function rejectedOrdersCannotBeShipped() : void
    {
        $this->expectException(OrderCannotBeShippedException::class);

        $initialOrder = new Order();
        $initialOrder->setId(1);
        $initialOrder->setStatus(OrderStatus::rejected());
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderShipmentRequest();
        $request->setOrderId(1);

        $this->useCase->run($request);

        $this->assertEmpty($this->orderRepository->getSavedOrder());
        $this->assertEmpty($this->shipmentService->getShippedOrder());
    }

    /**
     * @test
     */
    public function shippedOrdersCannotBeShippedAgain() : void
    {
        $this->expectException(OrderCannotBeShippedTwiceException::class);

        $initialOrder = new Order();
        $initialOrder->setId(1);
        $initialOrder->setStatus(OrderStatus::shipped());
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderShipmentRequest();
        $request->setOrderId(1);

        $this->useCase->run($request);

        $this->assertEmpty($this->orderRepository->getSavedOrder());
        $this->assertEmpty($this->shipmentService->getShippedOrder());
    }

}
