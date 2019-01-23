using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.Requests.LoginResponse
{

   
    public class AccTransactions
    {
        public string Description { get; set; }
        public string PostDate { get; set; }
        public string Type { get; set; }
        public string OtherAccountNumber { get; set; }
        public string PostedDate { get; set; }
        public string Completed { get; set; }
        public string TransactionId { get; set; }

    }

    public class Status
    {
        public string LoginStatus = null;
        public string LoginStatusMessage = null;
    }
    public class LoginResponse
    {
        public string AccountNumber { get; set; }
        public int AccountId { get; set; }
        public List<AccTransactions> Statement { get; set; }
        public Status status  { get; set; }
    }


}
