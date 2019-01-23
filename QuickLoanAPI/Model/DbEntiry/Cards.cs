using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Cards
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public int AccountsId { get; set; }
        Accounts Accounts { get; set; }

        public string Type { get; set; }

        public string PaymentDueDate { get; set; }

        public decimal PaymentDue { get; set; }
    }
}
