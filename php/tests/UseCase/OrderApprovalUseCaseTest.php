<?php

namespace Tests\Pitchart\TellDontAskKata\UseCase;

use PHPUnit\Framework\Assert;
use PHPUnit\Framework\TestCase;
use Pitchart\TellDontAskKata\Domain\Order;
use Pitchart\TellDontAskKata\Domain\OrderStatus;
use Pitchart\TellDontAskKata\UseCase\ApprovedOrderCannotBeRejectedException;
use Pitchart\TellDontAskKata\UseCase\OrderApprovalRequest;
use Pitchart\TellDontAskKata\UseCase\OrderApprovalUseCase;
use Pitchart\TellDontAskKata\UseCase\RejectedOrderCannotBeApprovedException;
use Pitchart\TellDontAskKata\UseCase\ShippedOrdersCannotBeChangedException;
use Tests\Pitchart\TellDontAskKata\Doubles\InMemoryOrderRepository;

class OrderApprovalUseCaseTest extends TestCase
{
    private InMemoryOrderRepository $orderRepository;

    private OrderApprovalUseCase $useCase;

    protected function setUp(): void
    {
        parent::setUp();
        $this->orderRepository = new InMemoryOrderRepository();
        $this->useCase = new OrderApprovalUseCase($this->orderRepository);
    }

    public function test_approve_existing_order()
    {
        $initialOrder = (new Order)->setStatus(OrderStatus::Created)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderApprovalRequest())->setId(1)->setApproved(true);

        $this->useCase->run($request);

        $savedOrder = $this->orderRepository->getSavedOrder();
        Assert::assertEquals(OrderStatus::Approved, $savedOrder->getStatus());
    }


    public function RejectedExistingOrder(): void
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Created)->setId(1);

        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderApprovalRequest())->setId(1)->setApproved(false);

        $this->useCase->run($request);

        $savedOrder = $this->orderRepository->getSavedOrder();
        Assert::assertEquals(OrderStatus::Rejected, $savedOrder->getStatus());
    }

    public function test_can_not_approve_rejected_order(): void
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Rejected)->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderApprovalRequest())->setId(1)->setApproved(true);

        $this->expectException(RejectedOrderCannotBeApprovedException::class);

        $this->useCase->run($request);

        Assert::assertNull($this->orderRepository->getSavedOrder());
    }


    public function test_can_not_reject_approved_order(): void
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Approved)->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderApprovalRequest())->setId(1)->setApproved(false);

        $this->expectException(ApprovedOrderCannotBeRejectedException::class);

        $this->useCase->run($request);

        Assert::assertNull($this->orderRepository->getSavedOrder());
    }


    public function test_shipped_orders_cannot_be_rejected(): void
    {
        $initialOrder = (new Order())->setStatus(OrderStatus::Shipped)->setId(1);
        $this->orderRepository->addOrder($initialOrder);

        $request = (new OrderApprovalRequest())->setId(1)->setApproved(false);

        $this->expectException(ShippedOrdersCannotBeChangedException::class);

        $this->useCase->run($request);

        Assert::assertNull($this->orderRepository->getSavedOrder());
    }


}
