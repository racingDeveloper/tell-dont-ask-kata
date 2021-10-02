using System.Collections.Generic;

namespace TellDontAskKata.Main.UseCase
{
    public class SellItemsRequest
    {
        public IList<SellItemRequest> Requests { get; set; }
    }
}
