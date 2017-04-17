package it.gabrieletondi.telldontaskkata;

public class TestOrderRepository implements OrderRepository {
    private Order insertedOrder;

    public Order insertedOrder() {
        return insertedOrder;
    }

    public void save(Order order) {
        this.insertedOrder = order;
    }
}
