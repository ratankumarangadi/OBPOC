using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class AccountDetails
    {
        public int Id { get; set; }

        public int AccountsId { get; set; }
        Accounts Accounts { get; set; }

        public int MembersId { get; set; }
        Members Members { get; set; }

        public int AccountTypesId { get; set; }
        AccountTypes AccountTypes { get; set; }
    }
}
