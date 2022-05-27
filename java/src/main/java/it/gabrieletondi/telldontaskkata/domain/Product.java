package it.gabrieletondi.telldontaskkata.domain;

import static java.math.BigDecimal.valueOf;
import static java.math.RoundingMode.HALF_UP;

import java.math.BigDecimal;

public class Product {
    private String name;
    private BigDecimal price;
    private Category category;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public BigDecimal getPrice() {
        return price;
    }

    public void setPrice(BigDecimal price) {
        this.price = price;
    }

    public Category getCategory() {
        return category;
    }

    public void setCategory(Category category) {
        this.category = category;
    }

    public BigDecimal calculateTax() {
        return price.divide(valueOf(100)).multiply(category.getTaxPercentage()).setScale(2, HALF_UP);
    }

    public BigDecimal calculatePriceWithTax() {
        return price.add(calculateTax()).setScale(2, HALF_UP);
    }
}
