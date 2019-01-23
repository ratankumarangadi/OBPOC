using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickLoanAPI.Model.DbEntity
{
    public class Permissions
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public int UserId { get; set; }

    }
}
