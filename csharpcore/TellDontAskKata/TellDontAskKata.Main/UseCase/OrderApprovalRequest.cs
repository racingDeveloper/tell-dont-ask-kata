using System;
using System.Collections.Generic;
using System.Text;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderApprovalRequest
    {
        public int OrderId { get; set; }
        public bool Approved { get; set; }
    }
}
