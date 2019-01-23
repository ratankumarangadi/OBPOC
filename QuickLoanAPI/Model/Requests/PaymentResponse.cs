using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.Requests
{
    public class PaymentResponse
    {
        public bool Status { get; set; }
        public string Description { get; set; }
    }
}
