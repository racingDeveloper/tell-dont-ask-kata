
namespace TellDontAsk.UseCase
{
    public class OrderApprovalRequest {
        private int orderId;
        private bool approved;

        public void setOrderId(int orderId) {
            this.orderId = orderId;
        }

        public int getOrderId() {
            return orderId;
        }

        public void setApproved(bool approved) {
            this.approved = approved;
        }

        public bool isApproved() {
            return approved;
        }
    }
}
