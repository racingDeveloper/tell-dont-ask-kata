<?php

namespace Archel\TellDontAskTest\UseCase;

use Archel\TellDontAsk\Domain\Order;
use Archel\TellDontAsk\Domain\OrderStatus;
use Archel\TellDontAsk\UseCase\OrderApprovalRequest;
use Archel\TellDontAsk\UseCase\OrderApprovalUseCase;
use Archel\TellDontAsk\UseCase\RejectedOrderCannotBeApprovedException;
use Archel\TellDontAsk\UseCase\ShippedOrdersCannotBeChangedException;
use Archel\TellDontAskTest\Doubles\TestOrderRepository;
use PHPUnit\Framework\TestCase;

/**
 * Class OrderApprovalUseCaseTest
 * @package Archel\TellDontAskTest\UseCase
 */
class OrderApprovalUseCaseTest extends TestCase
{
    /**
     * @var TestOrderRepository
     */
    private $orderRepository;

    /**
     * @var OrderApprovalUseCase
     */
    private $useCase;

    /**
     * OrderApprovalUseCaseTest constructor.
     */
    public function setUp()
    {
        $this->orderRepository = new TestOrderRepository();
        $this->useCase = new OrderApprovalUseCase($this->orderRepository);
    }

    /**
     * @test
     */
    public function approvedExistingOrder() : void
    {
        $initialOrder = new Order();
        $initialOrder->setStatus(OrderStatus::created());
        $initialOrder->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderApprovalRequest();
        $request->setOrderId(1);
        $request->setApproved(true);

        $this->useCase->run($request);

        $savedOrder = $this->orderRepository->getSavedOrder();

        $this->assertEquals(OrderStatus::APPROVED, $savedOrder->getStatus()->getType());
    }

    /**
     * @test
     */
    public function rejectExistingOrder() : void
    {
        $initialOrder = new Order();
        $initialOrder->setStatus(OrderStatus::created());
        $initialOrder->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderApprovalRequest();
        $request->setOrderId(1);
        $request->setApproved(false);

        $this->useCase->run($request);

        $savedOrder = $this->orderRepository->getSavedOrder();

        $this->assertEquals(OrderStatus::REJECTED, $savedOrder->getStatus()->getType());
    }

    /**
     * @test
     */
    public function cannotApproveRejectedOrder() : void
    {
        $this->expectException(RejectedOrderCannotBeApprovedException::class);
        $initialOrder = new Order();
        $initialOrder->setStatus(OrderStatus::rejected());
        $initialOrder->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderApprovalRequest();
        $request->setOrderId(1);
        $request->setApproved(true);

        $this->useCase->run($request);

        $this->assertEmpty($this->orderRepository->getSavedOrder());
    }

    /**
     * @test
     */
    public function shippedOrdersCannotBeRejected() : void
    {
        $this->expectException(ShippedOrdersCannotBeChangedException::class);
        $initialOrder = new Order();
        $initialOrder->setStatus(OrderStatus::shipped());
        $initialOrder->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = new OrderApprovalRequest();
        $request->setOrderId(1);
        $request->setApproved(false);
        
        $this->useCase->run($request);

        $this->assertEmpty($this->orderRepository->getSavedOrder());
    }
}
