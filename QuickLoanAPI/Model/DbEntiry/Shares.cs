using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Shares
    {
        public int Id { get; set; }

        public int ShareNumber { get; set; }

        public int AccountsId { get; set; }
        Accounts Accounts { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }
    }
}
