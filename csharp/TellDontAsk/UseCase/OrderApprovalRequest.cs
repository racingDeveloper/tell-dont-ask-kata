using System;

namespace TellDontAsk.UseCase
{
    public class OrderApprovalRequest
    {
        private int orderId;
        private Boolean approved;

        public void setOrderId(int orderId)
        {
            this.orderId = orderId;
        }

        public int getOrderId()
        {
            return orderId;
        }

        public void setApproved(Boolean approved)
        {
            this.approved = approved;
        }

        public Boolean isApproved()
        {
            return approved;
        }
    }
}
