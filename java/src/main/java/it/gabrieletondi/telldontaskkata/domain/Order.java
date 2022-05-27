package it.gabrieletondi.telldontaskkata.domain;

import it.gabrieletondi.telldontaskkata.useCase.ApprovedOrderCannotBeRejectedException;
import it.gabrieletondi.telldontaskkata.useCase.OrderApprovalRequest;
import it.gabrieletondi.telldontaskkata.useCase.OrderShipmentRequest;
import it.gabrieletondi.telldontaskkata.useCase.RejectedOrderCannotBeApprovedException;
import it.gabrieletondi.telldontaskkata.useCase.ShippedOrdersCannotBeChangedException;
import java.math.BigDecimal;
import java.util.List;

public class Order {
    private BigDecimal total;
    private String currency;
    private List<OrderItem> items;
    private BigDecimal tax;
    private OrderStatus status;
    private int id;

    public BigDecimal getTotal() {
        return total;
    }

    public void setTotal(BigDecimal total) {
        this.total = total;
    }

    public String getCurrency() {
        return currency;
    }

    public void setCurrency(String currency) {
        this.currency = currency;
    }

    public List<OrderItem> getItems() {
        return items;
    }

    public void setItems(List<OrderItem> items) {
        this.items = items;
    }

    public BigDecimal getTax() {
        return tax;
    }

    public void setTax(BigDecimal tax) {
        this.tax = tax;
    }

    public OrderStatus getStatus() {
        return status;
    }

    public void setStatus(OrderStatus status) {
        this.status = status;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public void handleRequest(OrderApprovalRequest request) {
        if (status.equals(OrderStatus.SHIPPED)) {
            throw new ShippedOrdersCannotBeChangedException();
        }
        if (request.isApproved() && status.equals(OrderStatus.REJECTED)) {
            throw new RejectedOrderCannotBeApprovedException();
        }
        if (!request.isApproved() && status.equals(OrderStatus.APPROVED)) {
            throw new ApprovedOrderCannotBeRejectedException();
        }
        setStatus(request.isApproved() ? OrderStatus.APPROVED : OrderStatus.REJECTED);
    }

    public void handleRequest(OrderShipmentRequest request) {
//        TODO
    }
}
