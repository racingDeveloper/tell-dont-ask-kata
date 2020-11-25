package it.gabrieletondi.telldontaskkata.useCase;

import lombok.Data;

@Data
public class SellItemRequest {
    private int quantity;
    private String productName;
}
