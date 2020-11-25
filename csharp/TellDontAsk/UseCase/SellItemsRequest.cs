using System.Collections.Generic;

namespace TellDontAsk.UseCase
{
    public class SellItemsRequest
    {
        private List<SellItemRequest> requests;

        public void setRequests(List<SellItemRequest> requests)
        {
            this.requests = requests;
        }

        public List<SellItemRequest> getRequests()
        {
            return requests;
        }
    }
}
