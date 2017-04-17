package it.gabrieletondi.telldontaskkata.doubles;

import it.gabrieletondi.telldontaskkata.repository.TaxRepository;

import java.math.BigDecimal;
import java.util.HashMap;

public class InMemoryTaxRespository implements TaxRepository {
    private final HashMap<String, BigDecimal> productCategoryToTaxPercentage;

    public InMemoryTaxRespository(HashMap<String, BigDecimal> productCategoryToTaxPercentage) {
        this.productCategoryToTaxPercentage = productCategoryToTaxPercentage;
    }

    @Override
    public BigDecimal getTaxPercentageByCategory(String category) {
        return productCategoryToTaxPercentage.get(category);
    }
}
