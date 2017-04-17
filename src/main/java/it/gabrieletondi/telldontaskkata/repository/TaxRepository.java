package it.gabrieletondi.telldontaskkata.repository;

import java.math.BigDecimal;

public interface TaxRepository {
    BigDecimal getTaxPercentageByCategory(String category);
}
