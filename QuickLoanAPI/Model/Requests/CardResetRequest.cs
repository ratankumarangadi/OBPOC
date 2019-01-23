using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.Requests
{
    public class CardResetRequest
    {
        public string MemberId { get; set; }
        public double Amount { get; set; }
    }
}
