package it.gabrieletondi.telldontaskkata.domain;

import lombok.Data;

import java.math.BigDecimal;

@Data
public class OrderItem {
    private Product product;
    private int quantity;
    private BigDecimal taxedAmount;
    private BigDecimal tax;
}
