package it.gabrieletondi.telldontaskkata.useCase;

import lombok.Data;

@Data
public class OrderApprovalRequest {
    private int orderId;
    private boolean approved;
}
