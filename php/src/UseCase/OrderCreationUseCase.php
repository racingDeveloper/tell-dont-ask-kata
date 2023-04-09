<?php
declare(strict_types=1);

namespace Pitchart\TellDontAskKata\UseCase;

use Pitchart\TellDontAskKata\Domain\Order;
use Pitchart\TellDontAskKata\Domain\OrderItem;
use Pitchart\TellDontAskKata\Domain\OrderStatus;
use Pitchart\TellDontAskKata\Repository\OrderRepository;
use Pitchart\TellDontAskKata\Repository\ProductCatalog;

class OrderCreationUseCase
{
    private OrderRepository $repository;

    private ProductCatalog $catalog;

    /**
     * @param OrderRepository $repository
     * @param ProductCatalog $catalog
     */
    public function __construct(OrderRepository $repository, ProductCatalog $catalog)
    {
        $this->repository = $repository;
        $this->catalog = $catalog;
    }

    /**
     * @throws UnknownProductException
     */
    public function run(SellItemsRequest $request): void
    {
        $order = (new Order())
            ->setStatus(OrderStatus::Created)
            ->setCurrency("EUR");

        /** @var SellItemRequest $itemRequest */
        foreach ($request->getItems() as $itemRequest) {
            $product = $this->catalog->getByName($itemRequest->getProductName());

            if ($product == null) {
                throw new UnknownProductException();
            }

            $unitaryTax = self::round(($product->getPrice() / 100) * $product->getCategory()->getTaxPercentage());
            $unitaryTaxedAmount = self::round($product->getPrice() + $unitaryTax);
            $taxedAmount = self::round($unitaryTaxedAmount * $itemRequest->getQuantity());
            $taxAmount = self::round($unitaryTax * $itemRequest->getQuantity());

            $orderItem = (new OrderItem())
                ->setProduct($product)
                ->setQuantity($itemRequest->getQuantity())
                ->setTax($taxAmount)
                ->setTaxedAmount($taxedAmount);

            $order->getItems()->add($orderItem);
            $order->setTotal($order->getTotal() + $taxedAmount);
            $order->setTax($order->getTax() + $taxAmount);

            $this->repository->save($order);
        }

    }

    private static function round(float $amount): float
    {
        return round($amount, 2);
    }
}