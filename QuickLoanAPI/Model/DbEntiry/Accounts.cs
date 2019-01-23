using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Accounts
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string IBAN { get; set; }
        public string swift_bic { get; set; }
        public int bankId { get; set; }
    }
}
