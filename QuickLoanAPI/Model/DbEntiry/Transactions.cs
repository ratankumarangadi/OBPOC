using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Transactions
    {
        public int Id { get; set; }

        public int AccountsId { get; set; }
        Accounts Accounts { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Postdate { get; set; }

        public decimal Amount { get; set; }

        public int RecordId { get; set; }

        public int OtherAccountId { get; set; }

        public string Posted { get; set; }

        public string Completed { get; set; }
    }
}
