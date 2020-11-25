package it.gabrieletondi.telldontaskkata.domain;

import lombok.Data;

import java.math.BigDecimal;
import java.util.List;

@Data
public class Order {
    private BigDecimal total;
    private String currency;
    private List<OrderItem> items;
    private BigDecimal tax;
    private OrderStatus status;
    private int id;
}
