using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class BankNames
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public int BankId { get; set; }
        public string NationalIdentifier { get; set; }
    }
}
