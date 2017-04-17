package it.gabrieletondi.telldontaskkata.doubles;

import it.gabrieletondi.telldontaskkata.domain.Order;
import it.gabrieletondi.telldontaskkata.repository.OrderRepository;

public class TestOrderRepository implements OrderRepository {
    private Order insertedOrder;

    public Order insertedOrder() {
        return insertedOrder;
    }

    public void save(Order order) {
        this.insertedOrder = order;
    }
}
