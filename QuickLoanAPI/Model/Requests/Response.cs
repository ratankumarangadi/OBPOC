using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.Requests
{

    public class Share
    {
        public double balance { get; set; }
        public string description { get; set; }
        public int id { get; set; }
    }

    public class Loan
    {
        public double balance { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public decimal emiDue { get; set; }
        public string emiDueDate { get; set; }
    }

    public class Card
    {
        public string CardNumber { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }
        public decimal PaymentDue { get; set; }
        public string PaymentDueDate { get; set; }
    }

        public class TransactionList
        {
        public int TransactionId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int id { get; set; }
        public string PostDate { get; set; }
        public decimal Amount { get; set; }
        
        }

        public class Portfolio
    {
          
            public string account { get; set; }
            public string Primary { get; set; }
            public string Joint { get; set; }
           // public decimal loanEligibility { get; set; }
            public List<Share> shares { get; set; }
            public double shares_total { get; set; }
            public List<Loan> loans { get; set; }
            public List<TransactionList> transactionList;
            public Card card { get; set; }
            public double loans_total { get; set; }
            public string message { get; set; }
          //  public string status { get; set; }
        }

        public class RootObject
       {
        public int MemberId { get; set; }
        public string LastLogin { get; set; }
        public string MemberName { get; set; }
        public decimal loanEligibility { get; set; }
        public List<Portfolio> AccountView { get; set; }
       }
    
}
