using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Members
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string LastLogin { get; set; }
        public decimal LoanEligibility { get; set; }
        public bool IsEditable { get; set; }
        public bool InUse { get; set; }
    }
}
