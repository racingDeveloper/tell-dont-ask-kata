package it.gabrieletondi.telldontaskkata.useCase;

import lombok.Data;

import java.util.List;

@Data
public class SellItemsRequest {
    private List<SellItemRequest> requests;
}
