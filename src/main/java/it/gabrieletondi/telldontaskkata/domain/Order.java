package it.gabrieletondi.telldontaskkata.domain;

import lombok.AllArgsConstructor;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import static it.gabrieletondi.telldontaskkata.domain.OrderStatus.CREATED;

@NoArgsConstructor
public class Order {
    public Order(BigDecimal total, String currency, List<OrderItem> items, BigDecimal tax, OrderStatus status) {
        this.total = total;
        this.currency = currency;
        this.items = items;
        this.tax = tax;
        this.status = status;
    }

    private BigDecimal total;
    private String currency;
    private List<OrderItem> items=new ArrayList<>();
    private BigDecimal tax;
    private OrderStatus status;
    private int id;

    public BigDecimal getTotal() {
        return total;
    }


    public void setTotal(BigDecimal total) {
        this.total = total;
    }
    public void addTotal(BigDecimal amount){
        setTotal(this.total.add(amount));
    }

    public void addTax(BigDecimal tax){
        setTax(this.tax.add(tax));
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

    public void addOrderItem(OrderItem orderItem){
        items.add(orderItem);
    }

    public Boolean isOrderStatus(OrderStatus orderStatus){
        return this.getStatus().equals(orderStatus);
    }
}
