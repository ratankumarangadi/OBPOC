using QuickLoanAPI.Model.DbEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntiry
{
    public class Loans
    {
        public int Id { get; set; }

        public int LoanNumber { get; set; }

        public int AccountsId { get; set; }
        Accounts Accounts { get; set; }

        public decimal EmiDue { get; set; }

        public string EmiDueDate { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }
    }
}
