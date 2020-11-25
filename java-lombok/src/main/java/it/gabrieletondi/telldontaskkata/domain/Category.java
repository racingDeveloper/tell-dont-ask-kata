package it.gabrieletondi.telldontaskkata.domain;

import lombok.Data;

import java.math.BigDecimal;

@Data
public class Category {
    private String name;
    private BigDecimal taxPercentage;
}
